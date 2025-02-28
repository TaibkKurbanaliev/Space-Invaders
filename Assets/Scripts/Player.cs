using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _gunPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Instantiate(_bullet, _gunPoint.position, _gunPoint.rotation);
    }
}
