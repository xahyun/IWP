using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class textFloatUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1.4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up*Time.deltaTime * 5);
    }

    public void SetStuff(string value)
    {
        GetComponent<TextMeshProUGUI>().text = $"{value}";
    }
}
