using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CrossFade : SceneTransition
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public override IEnumerator AnimationIn()
    {
        var tweener = _canvasGroup.DOFade(1f, 1f);
        yield return tweener.WaitForCompletion();
    }

    public override IEnumerator AnimationOut()
    {
        var tweener = _canvasGroup.DOFade(0f, 1f);
        yield return tweener.WaitForCompletion();
    }
}
