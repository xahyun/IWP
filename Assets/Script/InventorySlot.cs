using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI LabelText;
    public TextMeshProUGUI StackSizeText;

    public void ClearSlot()
    {
        Icon.enabled = false;
        LabelText.enabled = false;
        StackSizeText.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if (item==null)
        {
            ClearSlot();
            return;
        }
        Icon.enabled = true;
        LabelText.enabled = true;
        StackSizeText.enabled = true;

        Icon.sprite = item.itemData.Icon;
        LabelText.text = item.itemData.DisplayName;
        StackSizeText.text = item.StackSize.ToString();
    }
}
