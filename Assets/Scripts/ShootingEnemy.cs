using System.Collections;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private float _maxDelay = 4f;
    [SerializeField] private float _minDelay = 2f;

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
            yield return new WaitForSeconds(Random.Range(_minDelay,_maxDelay));
            Shoot();
        }
    }
}
