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

    [SerializeField] [Range(2f, 20f)] float minimumSpawnDistance = 10f;

    GameObject _player;

    void Awake()
    {
        if (swordsman == null) { print("Swordsman prefab is null!"); }

        if (plane == null) { print("Plane is null!"); }

        _player = GameObject.FindWithTag("Player");
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

            sm.transform.SetPositionAndRotation(GetValidSpawnPoint(), sm.transform.rotation);
        }
    }

    Vector3 GetValidSpawnPoint()
    {
        for (var i = 0; i < 10; i++)
        {
            var newTransform = new Vector3(RandomFloat(), gameObject.transform.position.y, RandomFloat());
            if (SpawnPointValid(newTransform)) { return newTransform; }
        }

        print("Giving up and returning anyway");
        return new Vector3(RandomFloat(), gameObject.transform.position.y, RandomFloat());
    }

    float RandomFloat() => (Random.value * 2 * unitSpawnRadius) - unitSpawnRadius;

    bool SpawnPointValid(Vector3 point) => DistanceToPlayerOk(point) && PointIsAboveStage(point);

    bool PointIsAboveStage(Vector3 point) => Physics.Raycast(point, Vector3.down, 2f);

    bool DistanceToPlayerOk(Vector3 point) =>
        Vector3.Distance(_player.transform.position, point) >= minimumSpawnDistance;
}
}