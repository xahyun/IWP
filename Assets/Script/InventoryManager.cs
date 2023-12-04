using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public inventorySlot[] inventorySlots;
    public GameObject InventoryItemPrefab;
    public int maxStackItem = 10;

    int selectedSlot = -1;

    public void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int Number);
            if (isNumber && Number > 0 && Number < 10)
            {
                ChangeSelectedSlot(Number - 1);
            }
        }

    }
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].DeSelect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    public bool AddItem(Item item)
    {
        //check if any slot has the same item with count lower thn the max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlot slot = inventorySlots[i];
            if(slot.transform.childCount<=0)
            {
                continue;
            }
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxStackItem &&
                itemInSlot.item.Stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        //find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, inventorySlot slot)
    {
        GameObject newItemGO = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem(bool use )
    {
        inventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null )
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if(itemInSlot.count<=0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }    
            }
            return item;
        }
        return null;
    }
}
