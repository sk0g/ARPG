using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Cleanup : MonoBehaviour
    {
        [SerializeField] List<Component> componentsToDestroy = new();

        public void CleanUp()
        {
            if (componentsToDestroy.Count == 0) return;

            // destroy any components specified in the list, including itself
            componentsToDestroy.ForEach(Destroy);
            Destroy(this);
        }
    }
}