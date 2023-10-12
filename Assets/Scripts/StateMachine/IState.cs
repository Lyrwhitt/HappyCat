using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void EnterState();
    public void ExitState();
    public void HandleInput();
    public void UpdateState();
    public void FixedUpdateState();
}
