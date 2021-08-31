using System;
using Core;
using UnityEngine;

namespace Environment
{
[RequireComponent(typeof(Collider))]
public class KillOnTriggerEnter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print($"{other.name} entered kill-plane");
        other.GetComponent<Health>()?.TakeDamage(Single.PositiveInfinity);
    }
}
}