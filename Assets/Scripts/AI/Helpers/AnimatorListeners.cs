using UnityEngine;

namespace AI.Helpers
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorListeners : MonoBehaviour
    {
        Animator _animator;

        void Awake() => _animator = GetComponent<Animator>();

        public void SetTrigger(string triggerName) => _animator.SetTrigger(triggerName);

        public void SetFloat(string paramName, float newValue) => _animator.SetFloat(paramName, newValue);

        public void StartingAttack()
        {
        }

        public void EndingAttack()
        {
        }
    }
}