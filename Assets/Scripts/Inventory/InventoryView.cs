using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public List<InventoryCell> cells = new List<InventoryCell>();

    public Button closeBtn;

    private InventoryModel inventoryModel;

    public void InitializeInventoryView(InventoryModel model)
    {
        inventoryModel = model;

        closeBtn.onClick.AddListener(OnCloseBtnClicked);

        ClearInventory();
        UpdateInventory();
    }

    private void OnCloseBtnClicked()
    {
        OpenInventory();
    }

    public void ClearInventory()
    {
        for(int i = 0; i < cells.Count; i++)
        {
            cells[i].ClearCell();
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < inventoryModel.items.Count; i++)
        {
            cells[i].SetItem(inventoryModel.items[i]);
        }
    }

    public void OnRemoveItem(int cellNum)
    {
        cells[cellNum].ClearCell();
    }

    public void OpenInventory()
    {
        if (!this.gameObject.activeSelf)
        {
            GameManager.Instance.ChangeCursorLockMode(CursorLockMode.None);
            this.gameObject.SetActive(true);
        }
        else
        {
            GameManager.Instance.ChangeCursorLockMode(CursorLockMode.Locked);
            this.gameObject.SetActive(false);
        }
    }
}
