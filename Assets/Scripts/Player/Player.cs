using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStateMachine stateMachine;

    [field: Header("References")]
    [field: SerializeField] public PlayerSO data;

    [field: Header("Animation")]
    [field: SerializeField] public PlayerAnimationData animationData;

    //public Rigidbody rigidbody;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public PlayerInput input;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public ForceReceiver forceReceiver;


    private void Awake()
    {
        animationData.Initialize();

        //rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        forceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.idleState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
}
