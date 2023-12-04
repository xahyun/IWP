using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWand : MonoBehaviour, ICollectable
{
    //public static event HandleWandCollected OnWandCollected;
    //public delegate void HandleWandCollected(ItemData itemData);
    //public ItemData wandData;
    public void Collect()
    {
        Debug.Log("Red wand collected");
        Destroy(gameObject);
       // OnWandCollected?.Invoke(wandData);
    }
}
