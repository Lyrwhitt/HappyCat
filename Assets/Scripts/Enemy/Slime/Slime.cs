using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [field: Header("Animation")]
    [field: SerializeField] public SlimeAnimationData animationData;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public CharacterController controller;

    [HideInInspector]
    public ForceReceiver forceReceiver;

    private void Awake()
    {
        animationData.Initialize();

        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        forceReceiver = GetComponent<ForceReceiver>();
    }

    private void Update()
    {
        controller.Move(forceReceiver.Movement * Time.deltaTime);
    }
}
