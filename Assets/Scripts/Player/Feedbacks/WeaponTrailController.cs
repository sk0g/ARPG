using JetBrains.Annotations;
using UnityEngine;

public class WeaponTrailController : MonoBehaviour
{
    TrailRenderer _trailRenderer;

    void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        DisableEmitter();
    }

    [UsedImplicitly]
    void StartingAttack() => StartEmitting();

    [UsedImplicitly]
    void EndingAttack() => DisableEmitter();

    void DisableEmitter() => _trailRenderer.emitting = false;

    void StartEmitting()
    {
        _trailRenderer.emitting = true;
        _trailRenderer.Clear();
    }
}