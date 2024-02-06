using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject GO;
    [SerializeField] GameObject player;
    Vector3 Respawnpos=Vector3.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void respawnPlayer()
    {
        player.SetActive(true);
        player.transform.position = Respawnpos;
        Debug.Log(player.transform.position);
        HealthHeartBar.hb.Reset();
        GO.SetActive(false);
        InventoryManager.instance.ClearInvAndReduceWand();
        XP.xp.Reset();
    }
}
