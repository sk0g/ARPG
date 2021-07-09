using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] float maxWalkSpeed = 5f;

        CharacterController _cc;
        Animator _anim;

        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            _cc = GetComponent<CharacterController>();
            _anim = GetComponentInChildren<Animator>();
        }

        void FixedUpdate()
        {
            UpdateAnimatorSpeedValue();
        }

        void UpdateAnimatorSpeedValue() =>
            _anim.SetFloat(Speed, Mathf.Clamp01(_cc.velocity.magnitude / maxWalkSpeed));

        void SetTrigger(string triggerName) => _anim.SetTrigger(triggerName);
    }
}