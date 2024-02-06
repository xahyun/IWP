using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interect : ScriptableObject
{
    protected PlayerMovement pm;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }
    public virtual void interect()
    {
        Start();
    }
    public virtual void interect(Item currentItem)
    {
        Start();
    }
}
