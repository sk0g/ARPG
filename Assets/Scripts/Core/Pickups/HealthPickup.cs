using UnityEngine;

namespace Core.Pickups
{
    [RequireComponent(typeof(Collider))]
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] int healAmount = 10;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var health = other.GetComponent<Health>();
                if (health.CanHeal)
                {
                    health.Heal(healAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}