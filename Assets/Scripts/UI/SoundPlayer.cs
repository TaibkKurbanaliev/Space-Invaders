using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _selectSound;
    [SerializeField] private AudioClip _clickSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnSelect()
    {
        _audioSource.PlayOneShot(_selectSound);
    }

    public void OnClick()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}
