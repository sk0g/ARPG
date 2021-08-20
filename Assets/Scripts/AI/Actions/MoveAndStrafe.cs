using Interfaces;
using UnityEngine;

namespace AI.Actions
{
[RequireComponent(typeof(CharacterController))]
public class MoveAndStrafe : MonoBehaviour, IDirectionalMover
{
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float walkAndStrafeSpeed = 2f;

    [Tooltip("Degrees per second the character can turn")] [SerializeField]
    float rotationSpeed = 150f;

    CharacterController _cc;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    public void MoveInDirection(Vector3 movementDirection) =>
        _cc.SimpleMove(movementDirection.normalized * runSpeed);


    public void StrafeInDirection(Vector3 movementDirection) =>
        _cc.SimpleMove(movementDirection.normalized * walkAndStrafeSpeed);


    // slowly faces the direction to be looked at
    public void LookAtDirection(Vector3 direction) =>
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(direction),
            Time.fixedDeltaTime * rotationSpeed);
}
}