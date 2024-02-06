using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private TileBase highlightTile;
    [SerializeField] private Tilemap Ground;
    [SerializeField] private Tilemap Top;

    [SerializeField] private GameObject lootPrefab;

    private Vector3Int playerPos;
    private Vector3Int highlightTilePos;
    private bool highlighted;

    Item prevItem;
    private void Update()
    {
        Item item = InventoryManager.instance.GetSelectedItem(false);
        playerPos = Ground.WorldToCell(transform.position);
        if(item!=null)
        {
            HighlightTile(item);
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(highlighted)
            {
            
                if(item.type == Item.ItemType.Tools|| item.type == Item.ItemType.Wand)
                {
                    Vector3Int mouseGridPos = GetMouseOnGridPos();
                    RuleTileWithData RTWD = Ground.GetTile<RuleTileWithData>(mouseGridPos);
                    if(RTWD)
                    {
                        Destroy(highlightTilePos);
                    }
                }
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
          
            if (highlighted)
            {
                if (item.type == Item.ItemType.BuildingBlocks ||  (item.type == Item.ItemType.Wand))
                {
                    Build(highlightTilePos, item);
                    
                }
                else
                {
                    Interect();
                }
            }
            else
            {
                ItemInterect(item);
            }
        }
    }

    protected virtual void Interect()
    {
        Vector3Int mouseGridPos = GetMouseOnGridPos();
        RuleTileWithData RTWD=  Ground.GetTile<RuleTileWithData>(mouseGridPos);
        RTWD.interect.interect();
    }

    private Vector3Int GetMouseOnGridPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseCellPos = Ground.WorldToCell(mousePos);
        mouseCellPos.z = 0;

        return mouseCellPos;

    }

    private void HighlightTile(Item currentItem)
    {
        Vector3Int mouseGridPos = GetMouseOnGridPos();

        if (highlightTilePos != mouseGridPos || prevItem != currentItem)
        {
            Top.SetTile(highlightTilePos, null);

            if(InRange(playerPos, mouseGridPos, currentItem.range))
            {
                if (CheckCondition(Ground.GetTile<RuleTileWithData>(mouseGridPos),currentItem))
                {
                    Top.SetTile(mouseGridPos, highlightTile);

                    highlighted = true;
                }
                else
                {
                    highlighted = false;
                }
            }

            highlightTilePos = mouseGridPos;
            prevItem = currentItem;
        }
    }
    private bool InRange(Vector3Int positionA, Vector3Int PositionB, Vector2 range)
    {
        Vector3Int distance = positionA - PositionB;

        if(Mathf.Abs(distance.x)>=range.x || Mathf.Abs(distance.y)>=range.y)
        {
            return false;
        }
        return true;
    }
    private bool CheckCondition(RuleTileWithData tile, Item currentItem)
    {
   
        if (currentItem.type == Item.ItemType.BuildingBlocks)
        {
            if (!tile)
            {
                return true;
            }

        }
        else if (currentItem.type == Item.ItemType.Tools)
        {
            if (tile)
            {
                if (currentItem.itemName == "Wand")
                {
                    currentItem.actiontype = tile.item.actiontype;
                }
                if (tile.item.actiontype == currentItem.actiontype)
                {
                    return true;
                }
            }
        }
        else if (currentItem.itemName == "Wand")
        {
            if (tile)
            {
                if (tile.item==null)
                {
                    return true;
                }
                currentItem.actiontype = tile.item.actiontype;
                if (tile.item.actiontype == currentItem.actiontype)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
     
        return false;
    }

    void ItemInterect(Item currentItem)
    {
        if(currentItem!=null)
        {
            currentItem.interect.interect(currentItem);
        }
    }

    private void Build(Vector3Int position, Item itemToBuild)
    {
        if(itemToBuild.tile ==null)
        {
            return;
        }
        Top.SetTile(position, null);
        highlighted = false;
        audioManeger.ins.PlayAudio(6);
        InventoryManager.instance.GetSelectedItem(true);
        Ground.SetTile(position, itemToBuild.tile);
        if (itemToBuild.itemName == "Wand")
        {
            itemToBuild.tile = null;
        }
    }

    private void Destroy(Vector3Int position)
    {
        Top.SetTile(position, null);
        highlighted = false;

        RuleTileWithData tile = Ground.GetTile<RuleTileWithData>(position);
        Ground.SetTile(position, null);
        audioManeger.ins.PlayAudio(1);
        Vector3 pos = Ground.GetCellCenterWorld(position);
        GameObject loot = Instantiate(lootPrefab, pos, Quaternion.identity);
        loot.GetComponent<Loot>().Initialize(tile.dropItem);
    }
}
