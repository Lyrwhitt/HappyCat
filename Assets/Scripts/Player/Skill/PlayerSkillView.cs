using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillView : MonoBehaviour
{
    [HideInInspector]
    public List<SkillCell> skillCells = new List<SkillCell>();

    public SkillCell btnQ;
    public SkillCell btnE;
    public SkillCell btnR;
    public SkillCell btnT;
    public SkillCell btnF;
    public SkillCell btnG;

    private void Awake()
    {
        skillCells.Add(btnQ);
        skillCells.Add(btnE);
        skillCells.Add(btnR);
        skillCells.Add(btnT);
        skillCells.Add(btnF);
        skillCells.Add(btnG);
    }
}
