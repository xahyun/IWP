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
    public bool split;
    public bool ExtraGotDestroy;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parentBeforeDrag;
    [HideInInspector] public Item item;
    [HideInInspector] public double count = 1;
    public GameObject extra;

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
        if(Input.GetMouseButton(1) && count>1)
        {
            double pickUP = count / 2;
            pickUP = Math.Floor(pickUP);
            Debug.LogError(pickUP);
            count -= pickUP;
            RefreshCount();
            extra = Instantiate(gameObject, transform.position, Quaternion.identity) as GameObject;
            extra.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            InventoryItem II = extra.GetComponent<InventoryItem>();
            II.count = pickUP;
            II.RefreshCount();
            II.parentAfterDrag = transform.parent;

            II.image.raycastTarget = false;
            Debug.Log(extra.GetComponent<InventoryItem>().parentBeforeDrag);
            extra.transform.SetParent(transform.root);
            split = true;
        }
        else
        {
            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            string PrevParentName = transform.parent.name;
          
            if (transform.parent.transform.parent.name == "Crafting")
            {
                transform.SetParent(transform.root);
                craftingMan.ins.crafting();
                return;
            }
            transform.SetParent(transform.root);
            if (PrevParentName == "itemGot")
            {
                craftingMan.ins.DeleteAllCrafting();
            }

        }

    }

    private void Update()
    {
        if(split)
        {
            extra.transform.position = Input.mousePosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!split)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        if (!split)
        {
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
            if (parentAfterDrag.TryGetComponent<inventorySlot>(out inventorySlot IS))
            {
                if (IS.craftableSlot)
                {
                    craftingMan.ins.crafting();
                }
            }
        }
        else
        {
            if (ExtraGotDestroy)
            {
                split = false;
                return;
            }

            InventoryItem II = extra.GetComponent<InventoryItem>();

            II.image.raycastTarget = true;
            if (transform.parent == II.parentAfterDrag)
            {
                count += extra.GetComponent<InventoryItem>().count;
                RefreshCount();
                Destroy(extra);
                split = false;
                Debug.LogError("MoveBack");
                return;
            }

            extra.transform.SetParent(II.parentAfterDrag);
            if (II.parentAfterDrag.TryGetComponent<inventorySlot>(out inventorySlot IS))
            {

                if (IS.craftableSlot)
                {
                    craftingMan.ins.crafting();
                }
            }
        }

        split = false;
    }
}
