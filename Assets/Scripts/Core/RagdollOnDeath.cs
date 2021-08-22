using System;
using System.Collections.Generic;
using System.Linq;
using Player.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
[RequireComponent(typeof(Health))]
public class RagdollOnDeath : MonoBehaviour
{
    [SerializeField] List<Component> componentsToDestroy = new();
    [SerializeField] List<GameObject> gameObjectsToRagdoll = new();
    [SerializeField] Boolean shouldFreeWeaponOnDeath = true;

    readonly List<Tuple<Rigidbody, Collider>> _validatedRagdollComponents = new();

    [Header("Main ragdoll")] [SerializeField]
    float ragdollForceStrength = 10f;

    [SerializeField] float ragdollForceRadius = 10f;

    [Header("Secondary ragdoll twitches")] [SerializeField] [Range(0f, 1f)]
    float chanceForLimbsToJerkOnRagdoll = .3f;

    [SerializeField] float limbJerkForceDivisor = 20f;

    void Awake()
    {
        ReadAndValidateRequiredComponents();

        SetAnimated();
        GetComponent<Health>().onDeath.AddListener(HandleDeath);
    }

    void ReadAndValidateRequiredComponents()
    {
        gameObjectsToRagdoll.ForEach(go =>
        {
            var rb = go.GetComponent<Rigidbody>();
            var c = go.GetComponent<Collider>();

            if (rb == null || c == null) { print($"Double check {go.name} has ragdolling set up right!"); }
            else { _validatedRagdollComponents.Add(Tuple.Create(rb, c)); }
        });
    }

    void SetAnimated()
    {
        _validatedRagdollComponents.ForEach(components =>
        {
            var (rb, c) = components;

            rb.gameObject.tag = "BodyPart";
            rb.gameObject.AddComponent<DetachableBodyPart>();

            rb.isKinematic = true;
            c.isTrigger = true;

            // leaving these here for now in-case of physics implosion
            //rb.detectCollisions = false;
            //c.enabled = false;
        });
    }

    void HandleDeath(float overkillMultiplier)
    {
        SetRagdolling();
        TossAwayFromPlayer(overkillMultiplier);
        if (shouldFreeWeaponOnDeath) { FreeWeapon(); }

        // destroy any components specified in the list, including itself
        Destroy(this, .1f);
        componentsToDestroy.ForEach(Destroy);
    }

    void FreeWeapon()
    {
        GetComponentsInChildren<Weapon>()
            .Where(w => w.gameObject.GetComponent<Rigidbody>() != null)
            .ToList()
            .ForEach(weapon =>
            {
                weapon.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                weapon.SetColliderState(false);
            });
    }

    void SetRagdolling()
    {
        _validatedRagdollComponents.ForEach(components =>
        {
            var (rb, c) = components;

            // leaving these here for now in-case of physics implosion
            //c.enabled = true;
            c.isTrigger = false;

            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.mass *= 3; // make the ragdolled bodies less floppy
        });
    }

    void TossAwayFromPlayer(float overkillMultiplier)
    {
        var playerTransform = GameObject.FindWithTag("Player").transform.position;

        // shove around a few limbs and shit
        _validatedRagdollComponents.ForEach(components =>
        {
            var (rb, _) = components;

            if (Random.value <= chanceForLimbsToJerkOnRagdoll)
            {
                rb.AddForce(
                    new Vector3(
                        ragdollForceStrength / limbJerkForceDivisor * overkillMultiplier * Random.value,
                        ragdollForceStrength / limbJerkForceDivisor * overkillMultiplier * Random.value,
                        ragdollForceStrength / limbJerkForceDivisor * overkillMultiplier * Random.value
                    ),
                    ForceMode.Impulse);
            }
            else
            {
                rb.AddExplosionForce(
                    ragdollForceStrength * overkillMultiplier,
                    playerTransform,
                    ragdollForceRadius,
                    .5f,
                    ForceMode.Impulse);
            }
        });
    }
}
}