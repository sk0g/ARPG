using UnityEngine;

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
        print($"Dropping blood at {position}");
        var blood = Instantiate(bloodPickup, transform);

        blood.transform.SetPositionAndRotation(
            new Vector3(position.x, -1e-3f, position.z),
            blood.transform.rotation);
    }
}