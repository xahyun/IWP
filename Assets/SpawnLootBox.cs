using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnLootBox : MonoBehaviour
{
    [SerializeField] private TileBase highlightTile;
    [SerializeField] private Tilemap Ground;
    [SerializeField] private Tilemap Top;
    [SerializeField] TileBase chest;
    // Start is called before the first frame update
    void Start()
    {
        int chest = Random.Range(10, 20);
        for (int i = 0; i < chest; i++)
        {
            Vector2 randompos = (Vector2)transform.position + Random.insideUnitCircle * 50;
            Build(new Vector3Int((int)randompos.x,(int)randompos.y));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Build(Vector3Int position)
    {
        Ground.SetTile(position, chest);
    }
}
