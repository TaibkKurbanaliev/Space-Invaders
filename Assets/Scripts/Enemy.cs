using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float _speed;
    [SerializeField] private int _killPoints = 100;
    [SerializeField] private float _stepDownOffset = 1f;
    [SerializeField] private LayerMask _leftWall;
    [SerializeField] private LayerMask _rightWall;

    public static event Action<int, Vector3> Died;

    private static event Action<Vector2> _edgeReached;
    private Vector2 _directionX;
    private Rigidbody2D _rb;

    private bool _isReached = false;
    private float _currentPosY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentPosY = transform.position.y;
        _directionX = transform.right;
    }

    private void OnEnable()
    {
        _edgeReached += OnEdgeReached;
    }

    private void OnDisable()
    {
        _edgeReached -= OnEdgeReached;
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
        if ((1 << collision.gameObject.layer & _leftWall) != 0)
        {
            _edgeReached?.Invoke(transform.right);
        }
        else if ((1 << collision.gameObject.layer & _rightWall) != 0)
        {
            _edgeReached?.Invoke(-transform.right);
        }
    }

    public void Move(Vector2 direction)
    {
        Vector2 position = transform.position;
        _rb.MovePosition(position + direction * _speed * Time.fixedDeltaTime);
    }

    private void OnEdgeReached(Vector2 direction)
    {
        _directionX = direction;
        _isReached = true;
    }

    public void TakeDamage()
    {
        Died?.Invoke(_killPoints, transform.position);
        Destroy(gameObject);
    }
}
