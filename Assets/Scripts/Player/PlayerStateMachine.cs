using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player;

    [Header("State")]
    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    public PlayerFallState fallState;
    public PlayerNormalAttackState normalAttackState;
    public PlayerDashState dashState;
    public PlayerAirborneState airborneState;
    public PlayerStaggerState staggerState;
    public PlayerStandState standState;
    public PlayerDownState downState;
    public PlayerPostDelayState postDelayState;

    [Header("Skill State")]
    public UppercutState uppercutState;
    public GatlingPunchState gatlingPunchState;

    [Header("Movement")]
    public float animationBlend = 0f;
    public Vector2 movementInput;
    public float movementSpeed;
    public float rotationDamping;
    public float movementSpeedModifier = 1f;

    [Header("Jump")]
    public float jumpForce;

    [Header("Attack")]
    public bool isAttacking = false;
    public int comboIndex;
    public bool isCancelable = true;

    [Header("PostDelay")]
    public float postDelay = 0f;
    public bool isPostDelay = false;

    public Transform mainCameraTransform;

    public PlayerStateMachine(Player player)
    {
        this.player = player;

        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);
        jumpState = new PlayerJumpState(this);
        fallState = new PlayerFallState(this);
        normalAttackState = new PlayerNormalAttackState(this);
        dashState = new PlayerDashState(this);

        staggerState = new PlayerStaggerState(this);
        airborneState = new PlayerAirborneState(this);
        standState = new PlayerStandState(this);
        downState = new PlayerDownState(this);

        uppercutState = new UppercutState(this);
        gatlingPunchState = new GatlingPunchState(this);

        postDelayState = new PlayerPostDelayState(this);

        mainCameraTransform = Camera.main.transform;

        movementSpeed = player.data.groundedData.baseSpeed;
        rotationDamping = player.data.groundedData.baseRotationDamping;
    }
}
