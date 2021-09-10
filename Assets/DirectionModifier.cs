using System.Collections.Generic;
using UnityEngine;

public class DirectionModifier : MonoBehaviour
{
    [Range(5f, 25f)] [SerializeField] float rayCheckDistance = 5.0f;
    Ray forwardRay;
    List<Ray> Rays = new();

    void Awake()
    {
        forwardRay = new Ray(transform.position, transform.forward);
    }

    void FixedUpdate()
    {
        UpdateForwardRay();

        if (!CanMoveForward()) { transform.rotation = Quaternion.Euler(0, FindBestLookAngle(), 0); }
    }

    void OnDrawGizmos()
    {
        foreach (var r in Rays)
        {
            print("being called!");
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(r.origin, r.direction * 5.0f);
        }
    }

    void UpdateForwardRay()
    {
        forwardRay.origin = transform.position;
        forwardRay.direction = transform.forward;
    }

    bool CanMoveForward() => Physics.Raycast(forwardRay, out RaycastHit hit, rayCheckDistance);


    public float FindBestLookAngle()
    {
        List<Ray> ray = CreateSideRays();

        return 0;
    }

    public List<Ray> CreateSideRays()
    {
        int rayCount = 18;
        float totalAngle = 180;

        List<Ray> castRays = new();

        for (int i = 0; i < rayCount; i++)
        {
            Ray r = new Ray();
            r.origin = transform.position;
            r.direction = Quaternion.Euler(0, (i * totalAngle / rayCount) - (totalAngle / 2), 0) * transform.forward;
            castRays.Add(r);
        }

        return castRays;
    }
}