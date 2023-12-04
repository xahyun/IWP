using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldWand : MonoBehaviour, ICollectable
{
   // public static event HandleWandCollected OnWandCollected;
    //public delegate void HandleWandCollected(ItemData itemData);
    //public ItemData wandData;
    public void Collect()
    {
        Debug.Log("wand collected");
        Destroy(gameObject);
        //OnWandCollected?.Invoke(wandData);
    }

}
