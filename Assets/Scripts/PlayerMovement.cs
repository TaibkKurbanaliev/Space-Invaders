using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        Move();
    }

    private void Move()
    {
        var input = Input.GetAxis("Horizontal");

        if (input == 0f)
            return;

        Vector2 currentPos = transform.position;
        _rb.MovePosition(currentPos + Vector2.right * input * _speed * Time.fixedDeltaTime);
    }
}
