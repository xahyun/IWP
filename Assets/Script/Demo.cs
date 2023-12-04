using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] ItemsToPickUp;

    public void PickUpiItem(int id)
    {
        bool result = inventoryManager.AddItem(ItemsToPickUp[id]);
        if (result == true)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item not added");
        }
    }

    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received item" + receivedItem);
        }
        else
        {
            Debug.Log("no item received");
        }
    }

    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("used item" + receivedItem);
        }
        else
        {
            Debug.Log("no item received");
        }
    }
}
