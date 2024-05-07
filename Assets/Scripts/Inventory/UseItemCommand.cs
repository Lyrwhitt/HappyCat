using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEffect : ICommand
{
    public void Execute()
    {
        Debug.Log("아무 효과가 없는 아이템 입니다.");
    }
}
