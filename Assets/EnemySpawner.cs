using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();

    List<GameObject> enemySpawned = new List<GameObject>();

    float spawnCD=10;
    PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        pm = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemySpawned.Count ==0 && Vector3.Distance(pm.transform.position,transform.position) > 20)
        {
            spawnCD -= Time.deltaTime;
            if(spawnCD<=0)
            {
                spawnCD = 10;
                Spawn();
            }
        }
    }

    public void removeFromList(GameObject b)
    {
        enemySpawned.Remove(b);
    }

    void Spawn()
    {
        for (int i = 0; i < Random.Range(3,5); i++)
        {
            Vector3 d = transform.position + Random.insideUnitSphere * 10;
            d.z = 0;
            GameObject a = Instantiate(enemys[Random.Range(0,enemys.Count)],d,Quaternion.identity) as GameObject;

            a.GetComponent<Agent>().ES = this;
            enemySpawned.Add(a);
            
        }
    }
}
