using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XP : MonoBehaviour
{
    [SerializeField]Slider XpSlider;
    float XpNeededToLVlUP=10;
    float CurrentXP = 0;
    public static XP xp;

    [SerializeField] GameObject UpgradeButton;

    public Item[] UpgradeWand;
    public int NextWand = 1;


    public RuleTileWithData portal;

    private void Awake()
    {
        if(xp==null)
        {
            xp = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        changeSlideLvl();
        UpgradeButton.SetActive(false);
        SetStuff();
    }
    public void AddXP(int a)
    {
        CurrentXP += a;
        CurrentXP = Mathf.Clamp(CurrentXP, 0, XpNeededToLVlUP);
        changeSlideLvl();
        audioManeger.ins.PlayAudio(2);
    }

    void changeSlideLvl()
    {
        XpSlider.value = (CurrentXP / XpNeededToLVlUP);
        if(XpSlider.value>=1)
        {
            if (NextWand >= UpgradeWand.Length)
            {
                return;
            }
            UpgradeButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void buttonPress()
    {

        UpgradeButton.SetActive(false);
        if (InventoryManager.instance.GetWand(UpgradeWand[NextWand]))
        {
            NextWand++;

            CurrentXP = 0;
            changeSlideLvl();
        }
    }

    void SetStuff()
    {
        for(int i =2; i<UpgradeWand.Length;i++)
        {
            UpgradeWand[i].tile = portal;
        }
    }
    public void Reset()
    {
        NextWand = 1;
        InventoryManager.instance.GetWand(UpgradeWand[0]);
        CurrentXP = 0;
        SetStuff();
    }
}
