using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Health))]
    public class RagdollOnDeath : MonoBehaviour
    {
        [SerializeField] List<Component> componentsToDestroy = new();
        [SerializeField] Component rootNode;

        [Header("Main ragdoll")] [SerializeField]
        float ragdollForceStrength = 10f;

        [SerializeField] float ragdollForceRadius = 10f;

        [Header("Secondary ragdoll twitches")] [SerializeField] [Range(0f, 1f)]
        float chanceForLimbsToJerkOnRagdoll = .3f;

        [SerializeField] float limbJerkForceDivisor = 20f;

        void Awake()
        {
            SetAnimated();
            GetComponent<Health>().onDeath.AddListener(CleanUp);
        }

        void SetAnimated()
        {
            // this might be able to loop through RBs as well
            foreach (var c in rootNode.GetComponentsInChildren<Collider>(true))
            {
                c.attachedRigidbody.isKinematic = true;
                c.attachedRigidbody.detectCollisions = false;
            }
        }

        void CleanUp(float overkillMultiplier)
        {
            SetRagdolling(overkillMultiplier);

            // destroy any components specified in the list, including itself
            Destroy(this, .1f);
            componentsToDestroy.ForEach(Destroy);
        }

        void SetRagdolling(float overkillMultiplier)
        {
            foreach (var rb in GetComponentsInChildren<Rigidbody>().Where(rb => rb != null))
            {
                rb.isKinematic = false;
                rb.detectCollisions = true;
                rb.mass *= 3; // make the ragdolled bodies less floppy
            }

            var playerTransform = GameObject.FindWithTag("Player").transform.position;

            // shove around a few limbs and shit
            foreach (var rb in GetComponentsInChildren<Rigidbody>().Where(rb => rb != null))
            {
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
            }
        }
    }
}