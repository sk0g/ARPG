using System.Numerics;

namespace Interfaces
{
    public interface IStrafer
    {
        void MoveWhileLookingAt(Vector3 movementDirection, Vector3 lookAtDirection);
    }
}