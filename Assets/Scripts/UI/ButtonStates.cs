using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonStates : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnClick()
    {
        _animator.SetTrigger("Press");
    }
}
