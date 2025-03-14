using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExplosionEffectsPool : MonoBehaviour
{
    [SerializeField] private GameObject _particleSystem; 
    [SerializeField] private int _numberOfEffects;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private Player _player;

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
        _player.TakeDamaged += OnPlayerTakeDamaged;
        Enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _player.TakeDamaged -= OnPlayerTakeDamaged;
        Enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied(int _, Vector3 position)
    {
        Explose(position);
    }

    private void Explose(Vector3 position)
    {
        var system = _particlesPool.FirstOrDefault(system => system.GetComponent<ParticleSystem>().isStopped);
        system.gameObject.transform.position = position;
        system.GetComponent<ParticleSystem>().Play();
        _audioSource.PlayOneShot(_explosionSound);
    }

    private void OnPlayerTakeDamaged(int _health)
    {
        if (_health == 0)
        {
            Explose(_player.transform.position);
        }
    }
}
