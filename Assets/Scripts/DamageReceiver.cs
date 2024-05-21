using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public enum DamageType
{
    Stagger, Airborne
}

public class DamageReceiver : MonoBehaviour
{
    private Health health;
    private ForceReceiver forceReceiver;
    private Animator animator;
    private GroundDetection groundDetection;

    private float staggerTime = 0f;
    public bool isStagger => staggerTime > 0f;
    public bool isAirborne = false;

    private float dragOrigin;
    private float gravityOrigin;

    public Action onStagger;
    public Action onAirborne;
    public Action onStand;
    public Action onDown;

    private Coroutine coroutine;

    private void Awake()
    {
        health = GetComponent<Health>();
        forceReceiver = GetComponent<ForceReceiver>();
        animator = GetComponentInChildren<Animator>();
        groundDetection = GetComponent<GroundDetection>();

        dragOrigin = forceReceiver.drag;
        gravityOrigin = forceReceiver.gravity;
    }

    private void Update()
    {
        if (isStagger)
        {
            staggerTime -= Time.deltaTime;

            animator.SetFloat("StaggerTime", staggerTime);
        }
    }

    public void Damage(float damage, Vector3 force)
    {
        health.TakeDamage(damage);
        forceReceiver.AddForce(force);
    }

    public void Stagger(float time)
    {
        if (isAirborne)
            return;

        onStagger?.Invoke();
        staggerTime = time;
    }

    public void Airborne(float drag, float gravity, float downTime)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(AirborneCorountine(drag, gravity, downTime));
    }

    private IEnumerator AirborneCorountine(float drag, float gravity, float downTime)
    {
        if (isAirborne)
            yield break;

        onAirborne?.Invoke();
        WaitUntil waitUntil = new WaitUntil(() => !groundDetection.isGrounded);
        yield return waitUntil;

        isAirborne = true;
        forceReceiver.ChangeDragAndGravity(drag, gravity);

        waitUntil = new WaitUntil(() => groundDetection.isGrounded);
        yield return waitUntil;

        isAirborne = false;
        forceReceiver.ChangeDragAndGravity(dragOrigin, gravityOrigin);

        onDown?.Invoke();
        WaitForSeconds waitForSeconds = new WaitForSeconds(downTime);
        yield return waitForSeconds;

        onStand?.Invoke();
    }

    /*
    private IEnumerator AirborneCorountine(float drag, float gravity, float downTime)
    {
        if (isAirborne)
            yield break;

        animator.SetTrigger("Airborne");

        WaitUntil waitUntil = new WaitUntil(() => !groundDetection.isGrounded);
        yield return waitUntil;


        Debug.Log("airborne");

        isAirborne = true;
        forceReceiver.ChangeDragAndGravity(drag, gravity);

        waitUntil = new WaitUntil(() => groundDetection.isGrounded);
        yield return waitUntil;

        WaitForSeconds waitForSeconds = new WaitForSeconds(downTime);
        yield return waitForSeconds;

        animator.SetTrigger("Stand");

        Debug.Log("airborneEnd");

        isAirborne = false;
        forceReceiver.ChangeDragAndGravity(dragOrigin, gravityOrigin);
    }
    */
}
