using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionEffectsPool : MonoBehaviour
{
    [SerializeField] private int _numberOfEffects;
    [SerializeField] private ParticleSystem _particleSystem; 

    private List<ParticleSystem> _particlesPool;

    private void Awake()
    {
        var particleSystem = Instantiate(_particleSystem);
        particleSystem.Stop();
        _particlesPool.Add(particleSystem);
    }

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied(int _, Vector3 position)
    {
        var system = _particlesPool.FirstOrDefault(system => system.isStopped);
        system.gameObject.transform.position = position;
        system.Play();
    }
}
