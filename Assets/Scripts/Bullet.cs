using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _target;
    [SerializeField] private LayerMask _destroyer;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + transform.up * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _target) != 0)
        {
            if (collision.gameObject.TryGetComponent(out IDamagable target))
            {
                target.TakeDamage();
                Destroy(gameObject);
            }
        }

        if ((1 << collision.gameObject.layer & _destroyer) != 0)
        {
            Destroy(gameObject);
        }
    }
}
