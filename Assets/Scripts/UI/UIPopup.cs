using TMPro;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public TextMeshProUGUI popupContent;

    public void SetPopupContent(string content)
    {
        popupContent.text = content;
    }
}
