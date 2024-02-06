using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class LootBox : Interect
{
    public List<Item> LootObject = new List<Item>();

    public List<Item> ObejctToSpawn = new List<Item>();
    public GameObject Loot;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(1, 3);
        for (int i = 0; i < rand; i++)
        {
            ObejctToSpawn.Add(LootObject[Random.Range(0, LootObject.Count)]);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interect(Vector3Int pos,Tilemap Ground)
    {
        base.interect(pos, Ground);
        Start();
        Vector3 position = Ground.GetCellCenterWorld(pos);
        for (int i = 0; i < ObejctToSpawn.Count; i++)
        {
            int amt = Random.Range(1,2);

            for (int j = 0; j < amt; j++)
            {
                GameObject loot = Instantiate(Loot, position + Random.insideUnitSphere*2, Quaternion.identity);
                loot.GetComponent<Loot>().Initialize(ObejctToSpawn[i]);
            }

        }

        Ground.SetTile(pos, null);

    }
}
