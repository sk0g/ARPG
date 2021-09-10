using System.Linq;
using Actions;
using Core.Managers;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
[RequireComponent(typeof(Dash), typeof(IDirectionalMover))]
public class PlayerInputHandler : MonoBehaviour
{
    Attack _attack1, _attack2;

    Vector3 _currentMovement;
    Dash _dasher;
    IDirectionalMover _directionalMover;
    PlayerInputActions _inputActions;
    bool _shouldAttack, _shouldAttack2;
    bool _shouldDash;

    bool anyAttackIsAttacking => _attack1.isAttacking || _attack2.isAttacking;

    bool canDash => !anyAttackIsAttacking && _dasher.canDash;

    bool canAttack1 => !_dasher.isDashing && _attack1.canAttack && !anyAttackIsAttacking;

    bool canAttack2 => !_dasher.isDashing && _attack2.canAttack && !anyAttackIsAttacking;

    // due to how the animator's speed value is set in PlayerAnimationController.UpdateAnimatorSpeedValue(),
    // it is important to move even if the movement input is Vector3.zero
    bool canMove => !(_dasher.isDashing || anyAttackIsAttacking);

    void Awake()
    {
        _directionalMover = GetComponent<IDirectionalMover>();
        _dasher = GetComponent<Dash>();
        _attack1 = GetComponents<Attack>().First(a => a.animationName == "Attack1");
        _attack2 = GetComponents<Attack>().First(a => a.animationName == "Attack2");
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.PlayerCanAct) { return; }

        if (_shouldDash && canDash) { DoDash(); }
        else if (_shouldAttack && canAttack1) { DoAttack(); }
        else if (_shouldAttack2 && canAttack2) { DoAttack2(); }
        else if (canMove) { DoMove(); }
    }

    void DoDash()
    {
        if (_currentMovement != Vector3.zero) { _directionalMover.LookAtDirection(_currentMovement); }

        StartCoroutine(_dasher.StartDash());
        _shouldDash = false;
    }

    void DoAttack()
    {
        if (_currentMovement != Vector3.zero) { _directionalMover.LookAtDirection(_currentMovement); }

        _attack1.StartAttack();
        _shouldAttack = false;
    }

    void DoAttack2()
    {
        if (_currentMovement != Vector3.zero) { _directionalMover.LookAtDirection(_currentMovement); }

        _attack2.StartAttack();
        _shouldAttack2 = false;
    }

    void DoMove() => _directionalMover.MoveInDirection(_currentMovement);

    #region Input Reader

    [UsedImplicitly]
    public void ReadMovement(InputAction.CallbackContext ctx)
    {
        var movementVec2 = ctx.ReadValue<Vector2>();
        _currentMovement = Quaternion.Euler(0, 45, 0) * new Vector3(movementVec2.x, 0, movementVec2.y);
    }

    [UsedImplicitly]
    public void ReadDash(InputAction.CallbackContext ctx)
    {
        if (ctx.started) { _shouldDash = true; }

        if (ctx.canceled && _shouldDash) { _shouldDash = false; }
    }

    [UsedImplicitly]
    public void ReadAttack1(InputAction.CallbackContext ctx)
    {
        if (ctx.started) { _shouldAttack = true; }

        if (ctx.canceled && _shouldAttack) { _shouldAttack = false; }
    }

    [UsedImplicitly]
    public void ReadAttack2(InputAction.CallbackContext ctx)
    {
        if (ctx.started) { _shouldAttack2 = true; }

        if (ctx.canceled && _shouldAttack2) { _shouldAttack2 = false; }
    }

    [UsedImplicitly]
    public void ReadPause(InputAction.CallbackContext ctx)
    {
        if (ctx.started) { GameManager.Instance.TogglePause(); }
    }

    #endregion
}
}