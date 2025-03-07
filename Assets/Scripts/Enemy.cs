using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _killPoints = 100;
    [SerializeField] private float _stepDownOffset = 1f;
    [SerializeField] private LayerMask _wallMask;

    public static event Action<int, Vector3> Died;

    private static event Action _edgeReached;
    private Vector2 _directionX = Vector2.right;
    private Rigidbody2D _rb;

    private bool _isReached = false;
    private float _currentPosY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentPosY = transform.position.y;
    }

    private void OnEnable()
    {
        _edgeReached += OnEdgeReached;
    }

    private void OnDisable()
    {
        _edgeReached -= OnEdgeReached;
    }

    private void OnDestroy()
    {
        Died?.Invoke(_killPoints, transform.position);
    }

    private void Update()
    {
        if (_isReached)
        {
            if (transform.position.y > _currentPosY - _stepDownOffset)
            { 
                Move(Vector2.down); 
            }
            else
            {
                _currentPosY = transform.position.y;
                _isReached = false;
            }
        }
        else
        {
            Move(_directionX);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _wallMask) != 0)
        {
            _edgeReached?.Invoke();
        }
    }

    public void Move(Vector2 direction)
    {
        Vector2 position = transform.position;
        _rb.MovePosition(position + direction * _speed * Time.fixedDeltaTime);
    }

    private void OnEdgeReached()
    {
        _isReached = true;
        _directionX *= -1;
    }

}
