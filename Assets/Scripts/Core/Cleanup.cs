using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cleanup : MonoBehaviour
{
    [SerializeField] List<Component> componentsToDestroy = new List<Component>();
    public UnityEvent onDeath;

    public void CleanUp()
    {
        if (componentsToDestroy.Count != 0)
        {
            // destroy any components specifciued iun the list
            foreach (Component c in componentsToDestroy) { Destroy(c);}
            Destroy(this);
        }
    }
}