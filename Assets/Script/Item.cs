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
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool Stackable = true;
        
    [Header("Both")]
    public Sprite image;
 
    public void Change(ActionType item)
    {
        actiontype = item;
    }

    public enum ItemType
    {
        BuildingBlocks,
        Tools,
        Crafting
    }

    public enum ActionType
    {
        Dig,
        Chop,
        Mine
    }
}
