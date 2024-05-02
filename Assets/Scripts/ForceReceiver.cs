using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private CharacterController controller;

    public float drag = 0.3f;
    public float gravity = -9.8f;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // 타겟까지 감속도달
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }
    public void ResetForceReceiver()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }

    public void ChangeDragAndGravity(float drag, float gravity)
    {
        this.drag = drag;
        this.gravity = gravity;
    }
}
