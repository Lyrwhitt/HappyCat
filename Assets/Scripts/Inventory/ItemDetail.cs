using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetail : MonoBehaviour
{
    public Image itemImg;
    public TextMeshProUGUI quantity;
    public TextMeshProUGUI description;
    public Button useItemBtn;
    public Button confirmBtn;

    private ICommand useItemCommand;

    private void Start()
    {
        useItemBtn.onClick.AddListener(UseItemBtnClicked);
        confirmBtn.onClick.AddListener(ConfirmBtnClicked);
    }

    private void UseItemBtnClicked()
    {
        useItemCommand.Execute();
    }

    private void ConfirmBtnClicked()
    {
        /*
        ClearItemDetail();
        this.gameObject.SetActive(false);
        */
        //Destroy(this.gameObject);
        UIManager.Instance.CloseUI();
    }

    private void ClearItemDetail()
    {
        itemImg.sprite = null;
        quantity.text = string.Empty;
        description.text = string.Empty;
    }

    public void SetItemDetail(Item item)
    {
        itemImg.sprite = item.itemData.itemImg;
        quantity.text = item.quantity.ToString();
        description.text = item.itemData.itemDescription;
        //useItemCommand = item.useItemCommand;
    }
}
