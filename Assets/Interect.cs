using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Interect : ScriptableObject
{
    protected PlayerMovement pm;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    public virtual void interect(Item currentItem)
    {
        Start();
    }
    public virtual void interect(Vector3Int pos,Tilemap Ground)
    {
        Start();
    }
}
