using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    public PlayerSkillController skillController;

    [field: Header("References")]
    [field: SerializeField] public PlayerSO data;

    [field: Header("Animation")]
    [field: SerializeField] public PlayerAnimationData animationData;

    //public Rigidbody rigidbody;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public AnimationEventReceiver animationEventReceiver;
    [HideInInspector]
    public PlayerInput input;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public ForceReceiver forceReceiver;
    [HideInInspector]
    public Camera playerCamera;
    [HideInInspector]
    public CinemachineBrain playerCameraBrain;

    public Test testGizmo;

    private void Awake()
    {
        animationData.Initialize();

        //rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        animationEventReceiver = GetComponentInChildren<AnimationEventReceiver>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        forceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new PlayerStateMachine(this);
        skillController = GetComponent<PlayerSkillController>();

        playerCamera = Camera.main;
        playerCameraBrain = playerCamera.GetComponent<CinemachineBrain>();
    }

    private void Start()
    {
        GameManager.Instance.ChangeCursorLockMode(CursorLockMode.Locked);
        stateMachine.ChangeState(stateMachine.idleState);
    }

    
    private void OnDrawGizmos()
    {
        if (testGizmo == null)
            return;

        if (!testGizmo.testGizmo)
            return;

        // 디버그 모드에서 사각형 영역을 시각화합니다.
        Gizmos.color = Color.red;

        Gizmos.matrix = Matrix4x4.TRS(testGizmo.testGizmoCenter, testGizmo.testGizmoRotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, testGizmo.testGizmoSize);
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
