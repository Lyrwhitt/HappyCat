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

public class InventoryCommand : ICommand
{
    private InventoryView inventoryView;

    public InventoryCommand(InventoryView inventoryView)
    {
        this.inventoryView = inventoryView;
    }
    
    public void Execute()
    {
        inventoryView.OpenInventory();
    }
}