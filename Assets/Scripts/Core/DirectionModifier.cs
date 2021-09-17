using System.Collections.Generic;
using UnityEngine;

public class DirectionModifier : MonoBehaviour
{
    [Range(5f, 25f)] [SerializeField] float rayCheckDistance = 5.0f;
    [Range(0.1f, 5f)] [SerializeField] float rayThiccness = 1.0f;
    [Range(45, 360f)] [SerializeField] float totalLookAngle = 360f;
    [Range(1, 6)] [SerializeField] int rayCount = 1;

    Ray forwardRay;

    int rayCountStepSize = 16;

    public int RayCount
    {
        get => rayCount * rayCountStepSize;
        set => rayCount = value;
    }

    void Awake()
    {
        forwardRay = new Ray(transform.position, transform.forward);
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
        List<Ray> ray = CreateSideRays();

        int i = 0;
        foreach (Ray r in ray)
        {
            i++;
            if (CanMoveForward(r))
            {
                float angle = Vector3.Angle(Vector3.forward, r.direction);
                // Debug.DrawRay(r.origin, r.direction*3.0f);
                transform.rotation *= Quaternion.Euler(0, angle, 0);
            }
        }

        return 0;
    }

    List<Ray> CreateSideRays()
    {
        List<Ray> castRays = new();

        for (int i = 0; i < RayCount; i++)
        {
            Ray r = new Ray();
            r.origin = transform.position;
            r.direction = Quaternion.Euler(0, (i * totalLookAngle / RayCount) - (totalLookAngle / 2), 0)
                          * transform.forward;
            castRays.Add(r);
        }

        return castRays;
    }
}