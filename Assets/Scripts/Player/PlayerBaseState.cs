using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;
    protected DamageReceiver damageReceiver;
    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.player.data.groundedData;
        damageReceiver = stateMachine.player.GetComponent<DamageReceiver>();
    }

    public virtual void EnterState()
    {
        AddInputActionsCallbacks();
        AddEvents();
    }

    public virtual void ExitState()
    {
        RemoveInputActionsCallbacks();
        RemoveEvents();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void UpdateState()
    {
        Move();
    }

    public virtual void FixedUpdateState()
    {

    }


    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.player.input;
        input.playerActions.Movement.canceled += OnMovementCanceled;
        input.playerActions.Run.started += OnRunStarted;
        input.playerActions.Jump.started += OnJumpStarted;

        input.playerActions.Attack.performed += OnAttackPerformed;
        input.playerActions.Attack.canceled += OnAttackCanceled;

        input.playerActions.Dash.started += OnDashStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.player.input;
        input.playerActions.Movement.canceled -= OnMovementCanceled;
        input.playerActions.Run.started -= OnRunStarted;
        input.playerActions.Jump.started -= OnJumpStarted;

        input.playerActions.Attack.performed -= OnAttackPerformed;
        input.playerActions.Attack.canceled -= OnAttackCanceled;

        input.playerActions.Dash.started -= OnDashStarted;
    }

    protected virtual void AddEvents()
    {
        damageReceiver.onAirborne += OnAirborne;
        damageReceiver.onDown += OnDown;
        damageReceiver.onStagger += OnStagger;
        damageReceiver.onStand += OnStand;
    }

    protected virtual void RemoveEvents()
    {
        damageReceiver.onAirborne -= OnAirborne;
        damageReceiver.onDown -= OnDown;
        damageReceiver.onStagger -= OnStagger;
        damageReceiver.onStand -= OnStand;
    }

    protected void OnAirborne()
    {
        stateMachine.ChangeState(stateMachine.airborneState);
    }

    protected void OnStagger()
    {
        stateMachine.ChangeState(stateMachine.staggerState);
    }

    protected void OnStand()
    {
        stateMachine.ChangeState(stateMachine.standState);
    }

    protected void OnDown()
    {
        stateMachine.ChangeState(stateMachine.downState);
    }

    /*
    protected virtual void OnBtnQStarted(InputAction.CallbackContext obj)
    {
    }
    */

    protected virtual void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        stateMachine.isAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        stateMachine.isAttacking = false;
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }
    protected virtual void OnDashStarted(InputAction.CallbackContext context)
    {
        if (!stateMachine.player.groundDetection.isGrounded)
            return;

        if (CooldownManager.Instance.SkillUsable("Dash", 2.0f))
            stateMachine.ChangeState(stateMachine.dashState);
    }

    // 인풋 안받는 무브
    protected void ForceMove()
    {
        stateMachine.player.controller.Move(stateMachine.player.forceReceiver.Movement * Time.deltaTime);
    }

    private void ReadMovementInput()
    {
        stateMachine.movementInput = stateMachine.player.input.playerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);
        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovemenetSpeed();

        stateMachine.player.controller.Move(((movementDirection * movementSpeed) + stateMachine.player.forceReceiver.Movement) * Time.deltaTime);
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = stateMachine.player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.rotationDamping * Time.deltaTime);
        }
    }

    protected Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.mainCameraTransform.forward;
        Vector3 right = stateMachine.mainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.movementInput.y + right * stateMachine.movementInput.x;
    }

    private float GetMovemenetSpeed()
    {
        float movementSpeed = stateMachine.movementSpeed * stateMachine.movementSpeedModifier;
        return movementSpeed;
    }

    protected void SetRotationForward()
    {
        Vector3 forward = stateMachine.mainCameraTransform.forward;
        forward.y = 0;
        stateMachine.player.transform.rotation = Quaternion.LookRotation(forward);
    }

    protected void SetPostDelay(float delayTime)
    {
        stateMachine.postDelay = delayTime;
        stateMachine.isPostDelay = true;

        stateMachine.ChangeState(stateMachine.postDelayState);
    }

    protected void SetAnimationBool(int animationHash, bool value)
    {
        if(value)
            stateMachine.player.animator.SetBool(animationHash, true);
        else
            stateMachine.player.animator.SetBool(animationHash, false);
    }

    protected void SetAnimationFloat(int animationHash, float value)
    {
        stateMachine.player.animator.SetFloat(animationHash, value);
    }

    protected void SetAnimationTrigger(int animationHash)
    {
        stateMachine.player.animator.SetTrigger(animationHash);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
