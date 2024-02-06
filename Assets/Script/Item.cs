using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public string itemName;
    public TileBase tile;
    public ItemType type;
    public ActionType actiontype;

    public int damage;
    public Vector2 range;

    [Header("Only UI")]
    public bool Stackable = true;
        
    [Header("Both")]
    public Sprite image;

    public Interect interect;
 
    public void Change(ActionType item)
    {
        actiontype = item;
    }

    public enum ItemType
    {
        BuildingBlocks,
        Tools,
        Food,
        XP,
        Wand,
        Crafting
    }

    public enum ActionType
    {
        Dig,
        Chop,
        Mine
    }
}
