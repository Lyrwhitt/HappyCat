using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private InventoryController inventoryController;

    public List<InventoryCell> cells = new List<InventoryCell>();

    public Button closeBtn;

    public void InitializeInventoryView(SortedDictionary<int, Item> items, InventoryController controller)
    {
        closeBtn.onClick.AddListener(OnCloseBtnClicked);

        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].SetCell(i);
        }

        ClearInventory();
        UpdateAllInventoryItem(items);
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

    public void UpdateAllInventoryItem(SortedDictionary<int, Item> items)
    {
        foreach (KeyValuePair<int, Item> kvp in items)
        {
            cells[kvp.Key].SetItem(kvp.Value);
        }
    }

    public void OnAddItem(int cellNum, Item item)
    {
        cells[cellNum].SetItem(item);
    }

    public void OnRemoveItem(int cellNum)
    {
        cells[cellNum].ClearCell();
    }

    public void OnSwapItem(int origin, int swap)
    {
        if (cells[swap].item == null)
        {
            cells[swap].SetItem(cells[origin].item);
            cells[origin].ClearCell();
        }
        else
        {
            Item changeItem = cells[origin].item;
            cells[origin].SetItem(cells[swap].item);
            cells[swap].SetItem(changeItem);
        }
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
