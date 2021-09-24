using System.Collections;
using System.Collections.Generic;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Actions
{
public class Attack : MonoBehaviour
{
    [SerializeField] float cooldownTime = .3f;
    [SerializeField] float damageAmount = 10f;
    [SerializeField] string animationTriggerName = "Attack1";
    [SerializeField] bool canCrit;
    [SerializeField] WeaponSheath _weaponSheath;
    bool _canAttackAgain = true; // not attacking, and has served the cooldown time?

    List<GameObject> _objectsDamagedThisAttack = new();

    bool _shouldEmitWeaponTrailEvents; // assigned to in Awake

    public string animationName => animationTriggerName;

    public bool canAttack => !isAttacking && _canAttackAgain;

    public bool isAttacking { get; private set; }

    bool weaponIsDamaging => _weaponSheath.WeaponIsTrigger();

    void Awake()
    {
        _weaponSheath = GetComponent<WeaponSheath>();
        _shouldEmitWeaponTrailEvents = GetComponentInChildren<WeaponTrailController>() != null;
    }

    public void OnCollisionEnter(Collision other) => MaybeDamage(other.collider);

    public void OnTriggerEnter(Collider other) => MaybeDamage(other);

    public void StartAttack()
    {
        //TODO: set to less expensive or more direct call 
        gameObject.SendMessage("SetTrigger", animationTriggerName);
        if (_shouldEmitWeaponTrailEvents) { gameObject.BroadcastMessage("StartingAttack"); }

        isAttacking = true;
        _objectsDamagedThisAttack.Clear();
    }

    [UsedImplicitly]
    public void StartAttackSwing()
    {
        if (isAttacking) { _weaponSheath.SetColliderState(true); }
    }

    [UsedImplicitly]
    public void EndAttackSwing()
    {
        if (isAttacking) { _weaponSheath.SetColliderState(false); }
    }

    public void Interrupt()
    {
        if (canAttack) { return; }

        EndAttackSwing();
        StartCoroutine(EndAttack());
    }

    [UsedImplicitly]
    public IEnumerator EndAttack()
    {
        isAttacking = false;
        if (_shouldEmitWeaponTrailEvents) { gameObject.BroadcastMessage("EndingAttack"); }

        yield return new WaitForSeconds(cooldownTime);

        _canAttackAgain = true;
    }

    void MaybeDamage(Collider other)
    {
        // big brain way of detecting whether two game objects have a parent-child relationship 
        // NOTE: big brain way is broken. Enemy root is actually the spawner, but it still works... For now.
        if (other.gameObject.transform.root == gameObject.transform.root) { return; }

        if (!(weaponIsDamaging && isAttacking)) { return; } // random collision, instead of an attack driven one

        var damageable = other.GetComponent<IDamageable>();
        if (damageable == null || _objectsDamagedThisAttack.Contains(other.gameObject)) { return; }

        var hitDamage = HitIsCrit(other.gameObject) ? damageAmount * 2 : damageAmount;

        damageable.TakeDamage(hitDamage);
        _objectsDamagedThisAttack.Add(other.gameObject);

        BroadcastMessage("PlayFeedbackNamed",
                         "AttackedEnemyFeedback",
                         SendMessageOptions.DontRequireReceiver);
    }

    bool HitIsCrit(GameObject other)
    {
        if (!canCrit) { return false; }

        var hitIsCrit = other.transform.InverseTransformPoint(transform.position).z <= -.5f;

        if (hitIsCrit) { BroadcastMessage("PlayFeedbackNamed", "CritFeedback"); }

        return hitIsCrit;
    }

    [UsedImplicitly]
    void Hit() { } // Animation Event Sink for imported pre-built animation events  
}
}