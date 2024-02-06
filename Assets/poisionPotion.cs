using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class poisionPotion : Interect
{
    public Collider2D col;
    public LayerMask LM;
    private PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void interect(Item currentItem)
    {
        base.interect(currentItem);
        pm = FindObjectOfType<PlayerMovement>();
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(Vector3.Distance(pos, pm.transform.position));
        if(Vector3.Distance(pos,pm.transform.position)>=11)
        {
            Debug.Log("asdadfdfgsd");
            return;
        }

        Debug.LogError(pos);

        col = Physics2D.OverlapCircle(new Vector3(pos.x, pos.y, 0), 10,LM);
        Debug.LogError(col);
        if(!col)
        {
            return;
        }
        if(col.TryGetComponent<Agent>(out Agent a))
        {
            a.setPosion();
            InventoryManager.instance.GetSelectedItem(true);
            audioManeger.ins.PlayAudio(5);
        }

    }
}
