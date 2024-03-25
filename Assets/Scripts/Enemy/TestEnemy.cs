using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TestEnemy : MonoBehaviour
{
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public ForceReceiver forceReceiver;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        forceReceiver = GetComponent<ForceReceiver>();
    }

    private void Update()
    {
        controller.Move(forceReceiver.Movement * Time.deltaTime);
    }
}
