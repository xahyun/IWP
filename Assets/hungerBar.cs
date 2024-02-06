using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hungerBar : MonoBehaviour
{
    [SerializeField]GameObject hungerIcon;

    int maxHunger = 5;
    int curHunger = 5;
    public static hungerBar hb;

    float speedDecrease = 1;
    [SerializeField]float timer = 60;
    [SerializeField] float timerToDeductHealth = 10;
    [SerializeField] float timerToIncreaseHealth = 2;
    private void Awake()
    {
        if(hb==null)
        {
            hb = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        UpdateHunger(maxHunger);

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime * speedDecrease;
        if(timer<=0)
        {
            timer = 60;
            curHunger -= 1;
            UpdateHunger(curHunger);
        }

        if(curHunger<=0)
        {
            timerToDeductHealth -= Time.deltaTime ;
            if (timerToDeductHealth <= 0)
            {
                timerToDeductHealth = 10;
                HealthHeartBar.hb.DetuctFromHealth(1);
            }
        }
        else if (curHunger == maxHunger)
        {

            timerToDeductHealth = 10;
            timerToIncreaseHealth -= Time.deltaTime;
            if (timerToIncreaseHealth <= 0)
            {
                timerToIncreaseHealth = 2;
                HealthHeartBar.hb.AddFromHealth(1);
            }
        }
        else
        {
            timerToIncreaseHealth = 2;
            timerToDeductHealth = 10;
        }
 
    }
    public void SpeedUP(bool a)
    {
        speedDecrease = a? 1.5f:3f;
    }

    public void DetuctFromHunger(int a)
    {
        curHunger -= a;
        UpdateHunger(curHunger);
    }

    public void AddFromHunger(int a)
    {
        curHunger += a;
        UpdateHunger(curHunger);
    }

    public void UpdateHunger(int a)
    {
        curHunger = Mathf.Clamp(curHunger, 0, maxHunger);
        int actualBarForList = a - 1;
        for (int i = 0; i < hungerIcon.transform.childCount; i++)
        {
            if(i>actualBarForList)
            {
                hungerIcon.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                hungerIcon.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
