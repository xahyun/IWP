using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class craftingMan : MonoBehaviour
{
    public static craftingMan ins;
    public GameObject InventoryItemPrefab;
    public string craftingListr;
    public TextAsset TA;
    public List<inventorySlot> IS = new List<inventorySlot>();
    public inventorySlot built;

    public List<Item> CraftedItem = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        craftingListr = "Assets/craftingList.csv";
       // crafting();
    }
    private void Awake()
    {
        ins = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void crafting()
    {
        if(built.transform.childCount>0)
        {
        Destroy(built.transform.GetChild(0).gameObject);

        }

        // Open the file with a StreamReader
        using (StreamReader reader = new StreamReader(craftingListr))
        {
            int j = 0;
            // Read the file line by line
            while (!reader.EndOfStream)
            {
                // Read a line from the file
                string line = reader.ReadLine();

                // Split the line into fields using a comma as the separator
                string[] fields = line.Split(',');
                if (fields[0] == "//")
                {
                    Debug.Log("SKIP");
                    continue;
                }
                string stuff = "";
                // Now you can use the fields array to access individual values
                int i = -1;

                //Debug.Log("List" + j + fields[10]);
                foreach (string field in fields)
                {
                    if (i == -1 || i == 9)
                    {
                        i++;
                        continue;
                    }
                    if ((field == "" && IS[i].transform.childCount > 0))
                    {
                        stuff = "";


                        break;
                    }
                    if (field != "" && IS[i].transform.childCount == 0)
                    {
                        stuff = "";


                        break;
                    }
                    if ((field == "" && IS[i].transform.childCount == 0) || (field.ToLower() == IS[i].GetComponentInChildren<InventoryItem>().item.itemName.ToLower()))
                    {
                        i++;
                        stuff = fields[10].ToString();
                        continue;
                    }

                }
                if (stuff != "")
                {
                    Crafted(stuff);
                    break;
                }

                j++;
            }
        }
    }

    void Crafted(string stuff)
    {
        foreach (var item in CraftedItem)
        {
            Debug.Log($"{stuff.ToLower()} :: {item.itemName.ToLower()}  ");
            if (item.itemName.ToLower() == stuff.ToLower())
            {
                Debug.Log($"{stuff.ToLower()} :: {item.itemName.ToLower()}  ");
                SpawnNewItem(item, built);
                break;
            }
        }
    }

    void SpawnNewItem(Item item, inventorySlot slot)
    {
        GameObject newItemGO = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public void DeleteAllCrafting()
    {
        foreach (var item in IS)
        {
            Debug.Log(item.transform.childCount);
            if(item.transform.childCount>0)
            {
                Destroy(item.transform.GetChild(0).gameObject);
            }
        }
    }
}



