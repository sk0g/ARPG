using JetBrains.Annotations;
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
    
    [UsedImplicitly]
    void SetTrigger(string triggerName)
    {
        _anim.SetTrigger(triggerName);
    }
    
    void UpdateAnimatorSpeedValue() =>
        _anim.SetFloat(Speed, Mathf.Clamp01(_cc.velocity.magnitude / maxWalkSpeed));

    /*
     * Override existing animation suite with new animations
     * i.e. Walk => Axe 2Hand walk
     */
    public void SetAnimations(AnimatorOverrideController overrideController)
    {
        if (overrideController == null)
        {
            Debug.LogError("Animation Override is null");
            return;
        }

        _anim.runtimeAnimatorController = overrideController;
    }
}
}