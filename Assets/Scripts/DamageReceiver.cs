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
        }
    }

    public void Damage(float damage, Vector3 force)
    {
        health.TakeDamage(damage);
        forceReceiver.AddForce(force);
    }

    public void Stagger(float time)
    {
        animator.SetTrigger("Stagger");
        staggerTime = time;
    }

    public void Airborne(float drag, float gravity)
    {
        StartCoroutine(AirborneCorountine(drag, gravity));
    }

    private IEnumerator AirborneCorountine(float drag, float gravity)
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


        Debug.Log("airborneEnd");

        isAirborne = false;
        forceReceiver.ChangeDragAndGravity(dragOrigin, gravityOrigin);
    }
}
