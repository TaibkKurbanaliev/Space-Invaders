using System.Collections;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private float _delay = 1f;


    private void Start()
    {
        StartCoroutine(MakeShoots());
    }

    private void Shoot()
    {
        Instantiate(_bullet, _gunPoint.position, _gunPoint.rotation);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator MakeShoots()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(_delay);
        }
    }
}
