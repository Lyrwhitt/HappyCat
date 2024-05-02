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

    private void Awake()
    {
        health = GetComponent<Health>();
        forceReceiver = GetComponent<ForceReceiver>();
        animator = GetComponentInChildren<Animator>();
        groundDetection = GetComponent<GroundDetection>();
    }

    private void Update()
    {
        if (isStagger)
        {
            Debug.Log("stagger");
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

    public void Airborne()
    {
        StartCoroutine(AirborneCorountine());
    }

    private IEnumerator AirborneCorountine()
    {
        if (isAirborne)
            yield break;

        animator.SetTrigger("Airborne");

        WaitUntil waitUntil = new WaitUntil(() => !groundDetection.isGrounded);

        yield return waitUntil;
        isAirborne = true;

        waitUntil = new WaitUntil(() => groundDetection.isGrounded);

        yield return waitUntil;
        isAirborne = false;
    }
}
