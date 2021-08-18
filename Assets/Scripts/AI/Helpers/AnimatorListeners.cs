using JetBrains.Annotations;
using UnityEngine;

namespace AI.Helpers
{
[RequireComponent(typeof(Animator))]
public class AnimatorListeners : MonoBehaviour
{
    Animator _animator;

    void Awake() => _animator = GetComponent<Animator>();

    [UsedImplicitly]
    public void SetTrigger(string triggerName) => _animator.SetTrigger(triggerName);

    [UsedImplicitly]
    public void SetFloat(string paramName, float newValue) => _animator.SetFloat(paramName, newValue);
}
}