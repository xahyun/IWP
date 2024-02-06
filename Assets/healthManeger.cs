using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManeger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]float hp;
    float Currenthp;
    [SerializeField] float Maxhp;
    [SerializeField] Slider slider;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject textmesh;
    [SerializeField] GameObject Loot;
    [SerializeField] Item XP;
    [SerializeField] List<Item> itemToDrop = new List<Item>();
    public float health
    {
        get { return hp; }
        set { hp = value; CheckHp(Currenthp-hp); }
    }

    public float Maxhealth
    {
        get { return Maxhp; }
        set { Maxhp = value;  }
    }

    void Start()
    {
        hp = Currenthp = Maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            health -= 2;
        }
    }

    void CheckHp(float value)
    {
        slider.value = (hp / Maxhp);
        GameObject a = Instantiate(textmesh, canvas.transform);
        a.GetComponent<textFloatUp>().SetStuff((-1 * value).ToString());
        Currenthp = hp;
        if (hp<=0)
        {
            //die code
          
            for (int i = 0; i < Random.Range(5,8); i++)
            {
                Vector3 pos = Random.insideUnitCircle;
                GameObject xp=Instantiate(Loot,transform.position+ pos, Quaternion.identity);

                xp.GetComponent<Loot>().Initialize(XP);
            }
            Vector3 pos1 = Random.insideUnitCircle;
            GameObject loot = Instantiate(Loot, transform.position + pos1, Quaternion.identity);

            loot.GetComponent<Loot>().Initialize(itemToDrop[Random.Range(0,itemToDrop.Count)]);

            Destroy(gameObject);
        }
    }
}
