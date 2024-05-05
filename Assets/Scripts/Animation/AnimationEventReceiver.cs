using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    private int actionID = 0;
    public Action animationEvent;

    public void SetAnimationEvent(int id)
    {
        actionID = id;
    }

    public void OnCalledEvent(int value)
    {
        if (actionID != value)
            return;
        
        animationEvent?.Invoke();
    }
}
