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

    [Header("Skill State")]
    public UppercutState uppercutState;

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

        uppercutState = new UppercutState(this);

        mainCameraTransform = Camera.main.transform;

        movementSpeed = player.data.groundedData.baseSpeed;
        rotationDamping = player.data.groundedData.baseRotationDamping;
    }
}
