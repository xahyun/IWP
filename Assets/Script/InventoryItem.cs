using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TextMeshProUGUI CountText;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        CountText.text = count.ToString();
        bool TextActive = count > 1;
        CountText.gameObject.SetActive(TextActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        if (transform.parent.name == "itemGot")
        {
            craftingMan.ins.DeleteAllCrafting();
        }
        if (transform.parent.transform.parent.name == "Crafting")
        {
            transform.SetParent(transform.root);
            craftingMan.ins.crafting();
            return;
        }
        transform.SetParent(transform.root);
     
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        //
        if (parentAfterDrag.TryGetComponent<inventorySlot>(out inventorySlot IS))
        {
            if(IS.craftableSlot)
            {
                craftingMan.ins.crafting();
            }
        }
    }
}
