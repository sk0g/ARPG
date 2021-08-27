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
    bool _canAttackAgain = true; // not attacking, and has served the cooldown time?

    bool _isAttacking; // currently involved in an animation event?

    List<GameObject> _objectsDamagedThisAttack = new();

    bool _shouldEmitWeaponTrailEvents; // assigned to in Awake

    public string AnimationName => animationTriggerName;

    public bool CanAttack => !_isAttacking && _canAttackAgain;

    public bool IsAttacking => _isAttacking;

    bool WeaponIsDamaging => currentWeapon.WeaponCollider.isTrigger;

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

    public void OnCollisionEnter(Collision other) => MaybeDamage(other.collider);

    public void OnTriggerEnter(Collider other) => MaybeDamage(other);

    public void StartAttack()
    {
        //TODO: set to less expensive or more direct call 
        gameObject.SendMessage("SetTrigger", animationTriggerName);
        if (_shouldEmitWeaponTrailEvents) { gameObject.BroadcastMessage("StartingAttack"); }

        _isAttacking = true;
        _objectsDamagedThisAttack.Clear();
    }

    [UsedImplicitly]
    public void StartAttackSwing() => currentWeapon.SetColliderState(true);

    [UsedImplicitly]
    public void EndAttackSwing() => currentWeapon.SetColliderState(false);

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

    void MaybeDamage(Collider other)
    {
        // big brain way of detecting whether two game objects have a parent-child relationship 
        // NOTE: big brain way is broken. Enemy root is actually the spawner, but it still works... For now.
        if (other.gameObject.transform.root == gameObject.transform.root) { return; }

        if (!(WeaponIsDamaging && IsAttacking)) { return; } // random collision, instead of an attack driven one

        var damageable = other.GetComponent<IDamageable>();
        if (damageable == null || _objectsDamagedThisAttack.Contains(other.gameObject)) { return; }

        var hitDamage = HitIsCrit(other.gameObject) ? damageAmount * 2 : damageAmount;

        damageable.TakeDamage(hitDamage);
        _objectsDamagedThisAttack.Add(other.gameObject);
    }

    bool HitIsCrit(GameObject other)
    {
        if (!canCrit) { return false; }

        var dotProduct = Quaternion.Dot(transform.rotation, other.transform.rotation);

        // between .8 and 1 is considered a crit
        var hitIsCrit = dotProduct >= .8f;

        if (hitIsCrit) { BroadcastMessage("PlayFeedbackNamed", "CritFeedback"); }

        return hitIsCrit;
    }

    [UsedImplicitly]
    void Hit() { } // Animation Event Sink for imported pre-built animation events  
}
}