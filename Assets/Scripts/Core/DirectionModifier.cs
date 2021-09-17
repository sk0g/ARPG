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

    void Awake()
    {
        _totalRayCount = Mathf.RoundToInt(totalLookAngle / raySpacingAngle);
    }

    Vector3 ClosestOffsetToDirection(Vector3? desiredMovementOffset) =>
        RaysToTest(desiredMovementOffset).FirstOrDefault(CanMoveForward).direction;

    bool CanMoveForward(Ray ray) => !Physics.SphereCast(ray, rayThiccness, out RaycastHit hit, rayCheckDistance);

    IEnumerable<Ray> RaysToTest(Vector3? desiredMovementOffset)
    {
        var rayOrigin = transform.position;
        var tryingToMoveTo = desiredMovementOffset ?? transform.forward;

        for (var i = 0; i < _totalRayCount; i++)
        {
            // ReSharper disable once PossibleLossOfFraction
            var angleMultiplier = (int) Mathf.Floor((i + 1) / 2);

            if (i % 2 == 0) // even - left rotate
            {
                yield return new Ray(
                    rayOrigin, Quaternion.Euler(0, -raySpacingAngle * angleMultiplier, 0) * tryingToMoveTo);
            }
            else // odd - right rotate
            {
                yield return new Ray(
                    rayOrigin, Quaternion.Euler(0, raySpacingAngle * angleMultiplier, 0) * tryingToMoveTo);
            }
        }
    }
}
}