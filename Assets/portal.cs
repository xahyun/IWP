using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class portal : Interect
{

    public override void interect()
    {
        base.interect();
        pm.transform.position = Vector3.zero;
        audioManeger.ins.PlayAudio(3);
    }
}
