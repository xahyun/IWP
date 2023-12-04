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
       if(transform.childCount == 0)
       {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
      
       }
    }


}
