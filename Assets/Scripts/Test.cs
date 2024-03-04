using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Animator testAnim;

    private void Start()
    {
        testAnim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            testAnim.SetBool("Fire", true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            testAnim.SetBool("Run", true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            testAnim.SetBool("Fire", false);
            testAnim.SetBool("Run", false);
        }
    }
}
