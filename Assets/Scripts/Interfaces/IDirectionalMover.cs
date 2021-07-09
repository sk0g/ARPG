using UnityEngine;

namespace Interfaces
{
    public interface IDirectionalMover
    {
        void MoveInDirection(Vector3 movementDirection);
        void LookAtDirection(Vector3 offset);
    }
}