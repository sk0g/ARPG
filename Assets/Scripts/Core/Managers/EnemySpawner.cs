using System.Collections;
using System.Linq;
using UnityEngine;

namespace Core.Managers
{
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject plane;

    [Header("Swordsman")] [SerializeField] GameObject swordsman;
    [SerializeField] float swordsmenSpawnFrequency = 5f;
    [SerializeField] int initialSwordsmenToSpawn = 1;

    [Header("General")] [SerializeField] [Range(1f, 20f)]
    float unitSpawnRadius = 20f;

    void Awake()
    {
        if (swordsman == null) { print("Swordsman prefab is null!"); }

        if (plane == null) { print("Plane is null!"); }
    }

    void OnEnable()
    {
        SpawnSwordsman(initialSwordsmenToSpawn);

        StartCoroutine(SpawnSwordsmenPeriodically());
    }

    IEnumerator SpawnSwordsmenPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(swordsmenSpawnFrequency);
            SpawnSwordsman();
        }
        // ReSharper disable once IteratorNeverReturns
    }

    void SpawnSwordsman(int count = 1)
    {
        foreach (var _ in Enumerable.Range(0, count))
        {
            var sm = Instantiate(swordsman, gameObject.transform);

            var currentPosition = sm.transform.position;
            currentPosition.x += RandomFloat();
            currentPosition.z += RandomFloat();

            sm.transform.SetPositionAndRotation(currentPosition, sm.transform.rotation);
        }
    }

    float RandomFloat() => (Random.value * 2 * unitSpawnRadius) - unitSpawnRadius;
}
}