using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
[Serializable]
public class Status
{
    [SerializeField] bool value;
    [HideInInspector] public UnityEvent<bool> valueChanged;

    public bool active => value;

    public void SetTo(bool newValue)
    {
        value = newValue;
        valueChanged.Invoke(newValue);
    }
}

public class CharacterStatus : MonoBehaviour
{
    public Status invulnerable;
    public Status stunned;
}
}