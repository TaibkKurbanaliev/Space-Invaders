using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 3;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private AudioClip _shootSound;

    private AudioSource _audioSource;
    private InputSystem_Actions _actions;

    private void Awake()
    {
        _actions = new InputSystem_Actions();
        _actions.Player.Attack.performed += OnAttack;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Instantiate(_bullet, _gunPoint.position, _gunPoint.rotation);
        _audioSource.PlayOneShot(_shootSound);
    }

    public void TakeDamage()
    {
        _health--;

        if (_health == 0)
        {
            Destroy(gameObject);
        }
    }
}
