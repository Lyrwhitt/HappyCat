using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    public Action animationEvent;

    public void OnCalledEvent()
    {
        animationEvent?.Invoke();
    }
}
