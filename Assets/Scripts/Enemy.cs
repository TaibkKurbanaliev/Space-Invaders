using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private static event Action _edgeReached;
    private Vector2 direction = Vector2.right;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        Move();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _edgeReached?.Invoke();
    }

    public void Move()
    {
        Vector2 position = transform.position;
        _rb.MovePosition(position + direction * _speed * Time.fixedDeltaTime);
    }

    private void OnEdgeReached()
    {
        direction *= -1;
    }

}
