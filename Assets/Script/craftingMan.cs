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
    string csvFile;
    public List<Item> CraftedItem = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        csvFile = Path.Combine(Application.streamingAssetsPath, "craftingList.csv");
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
        using (StreamReader reader = new StreamReader(csvFile))
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
                        if(field!="")
                        {

                        Debug.Log(IS[i].transform.childCount + "asd");
                        }
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
            if (item.itemName.ToLower() == stuff.ToLower())
            {
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
        bool couldCraft = false;
        foreach (var item in IS)
        {
            if(item.transform.childCount>0)
            {

                InventoryItem inventoryItem = item.transform.GetChild(0).GetComponent<InventoryItem>();
                if (inventoryItem.count == 1)
                {
                    Destroy(item.transform.GetChild(0).gameObject);
                }
                if (inventoryItem.count>1)
                {
                    inventoryItem.count--;
                    couldCraft = true;
                    inventoryItem.RefreshCount();
                }
            
            }
        }
        StartCoroutine(dealy());
    }

    IEnumerator dealy()
    {
        yield return new WaitForEndOfFrame();
        crafting();
    }
}



