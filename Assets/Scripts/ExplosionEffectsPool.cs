using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExplosionEffectsPool : MonoBehaviour
{
    [SerializeField] private GameObject _particleSystem; 
    [SerializeField] private int _numberOfEffects;
    [SerializeField] private AudioClip _explosionSound;

    private AudioSource _audioSource;
    private List<GameObject> _particlesPool = new();

    private void Start()
    {
        for (int i = 0; i < _numberOfEffects; i++)
        {
            var particleSystem = Instantiate(_particleSystem);
            particleSystem.GetComponent<ParticleSystem>().Stop();
            _particlesPool.Add(particleSystem);
        }
        
        _audioSource = GetComponent<AudioSource>();
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
        var system = _particlesPool.FirstOrDefault(system => system.GetComponent<ParticleSystem>().isStopped);
        system.gameObject.transform.position = position;
        system.GetComponent<ParticleSystem>().Play();
        _audioSource.PlayOneShot(_explosionSound);
    }
}
