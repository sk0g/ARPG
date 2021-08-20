using UnityEngine;

namespace Interfaces
{
public interface IStrafer
{
    void MoveWhileLookingAt(Vector3 movementDirection, Vector3 lookAtDirection);
}
}