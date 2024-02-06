using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class eatingFood : Interect
{
    // Start is called before the first frame update

    public override void interect(Item currentItem)
    {
        base.interect(currentItem);
        hungerBar.hb.AddFromHunger(currentItem.damage);
        InventoryManager.instance.GetSelectedItem(true);
        audioManeger.ins.PlayAudio(5);
    }
}
