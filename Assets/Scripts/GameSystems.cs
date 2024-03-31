using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenuCommand : ICommand
{
    private SkillMenu skillMenu;

    public SkillMenuCommand(SkillMenu skillMenu)
    {
        this.skillMenu = skillMenu;
    }

    public void Execute()
    {
        skillMenu.OpenSkillMenu();
    }
}