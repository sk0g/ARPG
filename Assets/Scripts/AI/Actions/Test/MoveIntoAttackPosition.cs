using TheKiwiCoder;
using UnityEngine;

public class MoveIntoAttackPosition : ActionNode
{
    [SerializeField] float attackRange;
    [SerializeField] float walkSpeed = 2.5f;
    [SerializeField] float rotationSpeed = 150f;


    GameObject _player;
    CharacterController _cc;

    protected override void OnStart()
    {
        _player = GameObject.FindWithTag("Player");
        _cc = context.gameObject.GetComponent<CharacterController>();

        context.animator.SetFloat("Speed", .5f);
    }

    protected override void OnStop()
    {
        context.animator.SetFloat("Speed", .0f);
    }

    protected override State OnUpdate()
    {
        if (DistanceToPlayer() <= attackRange) { return State.Success; }

        if (DistanceToPlayer() >= attackRange + 5) { return State.Failure; }

        MoveInDirection(OffsetToPlayer());
        LookAtDirection(OffsetToPlayer());

        return State.Running;
    }

    void MoveInDirection(Vector3 movementDirection) =>
        _cc.SimpleMove(movementDirection.normalized * walkSpeed);


    float DistanceToPlayer() => Vector3.Distance(context.transform.position, _player.transform.position);

    Vector3 OffsetToPlayer() => _player.transform.position - context.transform.position;

    void LookAtDirection(Vector3 direction) =>
        context.transform.rotation = Quaternion.RotateTowards(
            from: context.transform.rotation,
            to: Quaternion.LookRotation(direction),
            maxDegreesDelta: Time.fixedDeltaTime * rotationSpeed);
}