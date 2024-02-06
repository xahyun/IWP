using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class LootBox : Interect
{
    public List<Item> LootObject = new List<Item>();

    public GameObject Loot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interect(Vector3Int pos,Tilemap Ground)
    {
        base.interect(pos, Ground);

        Vector3 position = Ground.GetCellCenterWorld(pos);
        for (int i = 0; i < LootObject.Count; i++)
        {
            int amt = Random.Range(0,5);

            for (int j = 0; j < amt; j++)
            {
                GameObject loot = Instantiate(Loot, position + Random.insideUnitSphere*2, Quaternion.identity);
                loot.GetComponent<Loot>().Initialize(LootObject[i]);
            }

        }

        Ground.SetTile(pos, null);

    }
}
