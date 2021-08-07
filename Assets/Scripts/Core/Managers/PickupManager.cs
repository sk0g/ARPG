using UnityEngine;

namespace Core.Managers
{
    public class PickupManager : MonoBehaviour
    {
        [SerializeField] GameObject bloodPickup;

        public static PickupManager Instance;

        void Awake()
        {
            Instance = this;
        }

        public void SpawnBloodPickup(Vector3 position)
        {
            var blood = Instantiate(bloodPickup, transform);

            // rotating the blood randomly may be a good idea :) 
            blood.transform.SetPositionAndRotation(
                new Vector3(position.x, -1e-3f, position.z),
                blood.transform.rotation);
        }
    }
}