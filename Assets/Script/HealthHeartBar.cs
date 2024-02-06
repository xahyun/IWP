using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    [SerializeField] GameObject HealthIcon;

    int maxHealth = 8;
    int curHealth = 8;
    public static HealthHeartBar hb;
    [SerializeField] PlayerMovement pm;
    [SerializeField] GameObject GameOver; 
    private void Awake()
    {
        if (hb == null)
        {
            hb = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        UpdateHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DetuctFromHealth(int a)
    {
        curHealth -= a;
        UpdateHealth(curHealth);
        audioManeger.ins.PlayAudio(4);
    }
    public void DetuctFromHealth(int a,Vector3 Dir)
    {
        curHealth -= a;
        UpdateHealth(curHealth);
        pm.ForceBack(Dir);
    }

    public void AddFromHealth(int a)
    {
        if(curHealth==0)
        {
            return;
        }
        curHealth += a;
        UpdateHealth(curHealth);
    }
    public void Reset()
    {
        curHealth = maxHealth;
        UpdateHealth(curHealth);
    }

    public void UpdateHealth(int a)
    {
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        if(curHealth==0)
        {
            pm.gameObject.SetActive(false);
            
            GameOver.SetActive(true);
        }
        int actualBarForList = a - 1;
        for (int i = 0; i < HealthIcon.transform.childCount; i++)
        {
            if (i > actualBarForList)
            {
                HealthIcon.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                HealthIcon.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
