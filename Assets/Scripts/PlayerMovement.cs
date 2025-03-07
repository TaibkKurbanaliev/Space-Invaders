using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private Rigidbody2D _rb;
    private InputSystem_Actions _actions;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    public void Update()
    {
        Move();
    }

    private void Move()
    {
        var input = _actions.Player.Move.ReadValue<Vector2>().x;

        if (input == 0f)
            return;

        Vector2 currentPos = transform.position;
        _rb.MovePosition(currentPos + Vector2.right * input * _speed * Time.fixedDeltaTime);
    }
}
