using System.Collections;
using System.Collections.Generic;
using Interfaces;
using JetBrains.Annotations;
using Player;
using Player.Weapons;
using UnityEngine;

namespace Actions
{
public class Attack : MonoBehaviour
{
    [SerializeField] float cooldownTime = .3f;
    [SerializeField] float damageAmount = 10f;
    [SerializeField] string animationTriggerName = "Attack1";
    [SerializeField] Weapon currentWeapon;
    [SerializeField] bool canCrit;

    bool _shouldEmitWeaponTrailEvents;

    bool _isAttacking;
    bool _canAttackAgain = true;

    public string AnimationName => animationTriggerName;

    public bool CanAttack => !_isAttacking && _canAttackAgain;

    public bool IsAttacking => _isAttacking;

    List<GameObject> _objectsDamagedThisAttack = new();

    void Awake()
    {
        // not bare-fist!
        if (currentWeapon != null && currentWeapon.WeaponAnimations != null)
        {
            // TODO: Define a player component's interface similar to BTree's context
            GetComponent<PlayerAnimationController>().SetAnimations(currentWeapon.WeaponAnimations);
        }

        _shouldEmitWeaponTrailEvents = GetComponentInChildren<WeaponTrailController>() != null;
    }

    public void StartAttack()
    {
        gameObject.SendMessage("SetTrigger", animationTriggerName);
        if (_shouldEmitWeaponTrailEvents) { gameObject.BroadcastMessage("StartingAttack"); }

        _isAttacking = true;
    }

    [UsedImplicitly]
    public void StartAttackSwing()
    {
        currentWeapon.WeaponCollider.isTrigger = true;
        _objectsDamagedThisAttack.Clear();
    }

    [UsedImplicitly]
    public void EndAttackSwing() => currentWeapon.WeaponCollider.isTrigger = false;

    public void Interrupt()
    {
        if (CanAttack) { return; }

        EndAttackSwing();
        StartCoroutine(EndAttack());
    }

    [UsedImplicitly]
    public IEnumerator EndAttack()
    {
        _isAttacking = false;
        if (_shouldEmitWeaponTrailEvents) { gameObject.BroadcastMessage("EndingAttack"); }

        yield return new WaitForSeconds(cooldownTime);

        _canAttackAgain = true;
    }

    public void PassOnTriggerEnter(Collider other)
    {
        if (currentWeapon.WeaponCollider.isTrigger) { OnTriggerEnter(other); }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject) { return; }

        var damageable = other.GetComponent<IDamageable>();

        if (damageable == null || _objectsDamagedThisAttack.Contains(other.gameObject)) { return; }

        var hitDamage = HitIsCrit(other) ? damageAmount * 2 : damageAmount;

        damageable.TakeDamage(hitDamage);

        _objectsDamagedThisAttack.Add(other.gameObject);
    }

    bool HitIsCrit(Collider other)
    {
        if (!canCrit) { return false; }

        var dotProduct = Quaternion.Dot(transform.rotation, other.transform.rotation);

        // between .8 and 1 is considered a crit
        var hitIsCrit = dotProduct >= .8f;

        if (hitIsCrit) { BroadcastMessage("PlayFeedbacks", "CritFeedback"); }

        return hitIsCrit;
    }

    //Animation Event Sink for imported pre-built animation events  
    [UsedImplicitly]
    void Hit() => print("hit event being called");
}
}