using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine;

    [HideInInspector] public PlayerSkillController skillController;
    [HideInInspector] public InventoryController inventoryController;

    [field: Header("References")]
    [field: SerializeField] public PlayerSO data;
    [field: SerializeField] public PlayerSkillSO[] skillDatas;

    [field: Header("Animation")]
    [field: SerializeField] public PlayerAnimationData animationData;

    //public Rigidbody rigidbody;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AnimationEventReceiver animationEventReceiver;
    [HideInInspector] public PlayerInput input;
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public ForceReceiver forceReceiver;
    [HideInInspector] public Camera playerCamera;
    [HideInInspector] public CinemachineBrain playerCameraBrain;
    [HideInInspector] public GroundDetection groundDetection;
    [HideInInspector] public PlayerStatus status;

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
        status = GetComponent<PlayerStatus>();

        skillController = GetComponent<PlayerSkillController>();
        inventoryController = GetComponent<InventoryController>();

        groundDetection = GetComponent<GroundDetection>();

        skillController.SetSkillController(this);

        stateMachine = new PlayerStateMachine(this);

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                ItemController itemController;
                itemController = other.GetComponent<ItemController>();

                Debug.Log(itemController.GetItem().itemData.itemName);

                inventoryController.AddInventoryItem(itemController.GetItem());
            }
        }
    }
}
