using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class portal : Interect
{

    public override void interect(Vector3Int pos,Tilemap Ground)
    {
        base.interect(pos, Ground);
        OtherWorld OW = FindObjectOfType<OtherWorld>();
        if(!OW.travel)
        {
            OW.travel = true;
            pm.transform.position = OW.transform.position;
        }
        else
        {
            OW.travel = false;
            GameObject home = GameObject.Find("HomeWorld");
            pm.transform.position = home.transform.position;
        }

        audioManeger.ins.PlayAudio(3);
    }
}
