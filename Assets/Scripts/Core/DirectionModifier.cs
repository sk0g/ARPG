using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
public class DirectionModifier : MonoBehaviour
{
    [Range(5f, 25f)] [SerializeField] float rayCheckDistance = 5.0f;
    [Range(0.1f, 5f)] [SerializeField] float rayThiccness = 1.0f;
    [Range(45, 360f)] [SerializeField] float totalLookAngle = 360f;
    [Range(1, 6)] [SerializeField] int raySpacingAngle = 16;

    int _totalRayCount;

    Ray forwardRay;

    void Awake()
    {
        forwardRay = new Ray(transform.position, transform.forward);
        _totalRayCount = 360 / raySpacingAngle;
    }

    void FixedUpdate()
    {
        UpdateForwardRay();

        if (!CanMoveForward(forwardRay)) { FindBestLookAngle(); }
    }

    void UpdateForwardRay()
    {
        forwardRay.origin = transform.position;
        forwardRay.direction = transform.forward;
    }

    bool CanMoveForward(Ray ray) => !Physics.SphereCast(ray, rayThiccness, out RaycastHit hit, rayCheckDistance);

    float FindBestLookAngle()
    {
        var r = RaysToTest().FirstOrDefault(CanMoveForward);

        float angle = Vector3.Angle(Vector3.forward, r.direction);
        // Debug.DrawRay(r.origin, r.direction * 3.0f);

        transform.Rotate(new Vector3(0, 1, 0), angle);
        return 0;
    }

    IEnumerable<Ray> RaysToTest()
    {
        var rayOrigin = transform.position;
        var forward = transform.forward;

        for (var i = 0; i < _totalRayCount; i++)
        {
            // ReSharper disable once PossibleLossOfFraction
            var angleMultiplier = (int) Mathf.Floor((i + 1) / 2);

            if (i % 2 == 0) // even - left rotate
            {
                yield return new Ray(rayOrigin, Quaternion.Euler(0, -raySpacingAngle * angleMultiplier, 0) * forward);
            }
            else // odd - right rotate
            {
                yield return new Ray(rayOrigin, Quaternion.Euler(0, raySpacingAngle * angleMultiplier, 0) * forward);
            }
        }
    }
}
}