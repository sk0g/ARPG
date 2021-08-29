using UnityEngine;

namespace Core.Pickups
{
[RequireComponent(typeof(Collider))]
public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; } // health pickups are for the player :) 

        var health = other.GetComponent<Health>();
        if (health == null || !health.canHeal) return;

        health.Heal(healAmount);
        Destroy(gameObject);
    }
}
}