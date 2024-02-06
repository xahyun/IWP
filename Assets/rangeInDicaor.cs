using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeInDicaor : MonoBehaviour
{

    [SerializeField]GameObject player;
    [SerializeField] LineRenderer LR;

    public void DrawCircle(int steps, float radius)
    {
        LR.positionCount = steps;

        for (int currentSteps = 0; currentSteps<steps; currentSteps++)
        {
            float circumferenceProgress = (float)currentSteps / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, 5);

            LR.SetPosition(currentSteps, currentPosition+transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + new Vector3((Mathf.Sin(1) * Time.deltaTime * 100), 0,0);
        Item i = InventoryManager.instance.GetSelectedItem(false);
        if(i==null)
        {
            DrawCircle(600, 2);
            return;
        }
        if ( i.type== Item.ItemType.Wand || i.type == Item.ItemType.BuildingBlocks || i.type == Item.ItemType.Tools)
        {
            DrawCircle(600,i.range.x);
        }
        else
        {

            LR.positionCount=0;
        }

    }
}
