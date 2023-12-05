using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] public Item item;
    [SerializeField] private TileBase highlightTile;
    [SerializeField] private Tilemap Ground;
    [SerializeField] private Tilemap Top;

    private Vector3Int playerPos;
    private Vector3Int highlightTilePos;
    private bool highlighted;

    private void Update()
    {
        playerPos = Ground.WorldToCell(transform.position);
        if(item!=null)
        {
            HighlightTile(item);
        }
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

        if (highlightTilePos != mouseGridPos)
        {
            Top.SetTile(highlightTilePos, null);

            if(InRange(playerPos, mouseGridPos, (Vector3Int)currentItem.range))
            {

                if(CheckCondition(Ground.GetTile<RuleTileWithData>(mouseGridPos),currentItem))
                {
                    Top.SetTile(mouseGridPos, highlightTile);
                    highlightTilePos = mouseGridPos;

                    highlighted = true;
                }
                else
                {
                    highlighted = false;
                }
            }

        }
    }
    private bool InRange(Vector3Int positionA, Vector3Int PositionB, Vector3Int range)
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
        if(currentItem.type == Item.ItemType.BuildingBlocks)
        {
            if(!tile)
            {
                return true;
            }
            else if (currentItem.type == Item.ItemType.Tools)
            {
                if(tile)
                {
                    if(tile.item.actiontype == currentItem.actiontype)
                    {
                        return true;
                    }
                }


            }
        }
        return false;
    }

}
