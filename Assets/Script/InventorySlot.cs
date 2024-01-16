using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color SelectedColor, NotSelectedColor;
    public bool craftableSlot = false;

    private void Awake()
    {
        DeSelect();
    }
    public void Select()
    {
        image.color = SelectedColor;
    }

    public void DeSelect()
    {
        image.color = NotSelectedColor;
    }
    public void OnDrop(PointerEventData eventData)
    {

        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        InventoryItem inventoryItemPrev = eventData.pointerDrag.GetComponent<InventoryItem>();
        bool splitted = false ;
        if(inventoryItem.split)
        {
            splitted = true;
            inventoryItem = inventoryItem.extra.GetComponent<InventoryItem>();
        }


        if (transform.childCount > 0)
        {
            InventoryItem inventoryItemFromSlot = transform.GetChild(0).GetComponent<InventoryItem>();
            if (inventoryItem.item.Stackable)
            {
                if (inventoryItem.item.itemName == inventoryItemFromSlot.item.itemName)
                {
                    if (splitted)
                    {
                        inventoryItemPrev.ExtraGotDestroy = true;
                    }
                    inventoryItemFromSlot.count += inventoryItem.count;
                    inventoryItemFromSlot.RefreshCount();
                    Destroy(inventoryItem.gameObject);
                }
            }
        }
        else
        {

            inventoryItem.parentAfterDrag = transform;
        }
        
    }


}
