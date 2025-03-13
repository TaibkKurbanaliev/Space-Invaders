using System.Collections;
using UnityEngine;

public abstract class SceneTransition : MonoBehaviour
{
    public abstract IEnumerator AnimationIn();
    public abstract IEnumerator AnimationOut();
}
