using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManeger : MonoBehaviour
{
    [SerializeField] AudioClip[] ALLAUDIO;
    AudioSource AS;
    public static audioManeger ins;
    private void Awake()
    {
        ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(int index)
    {
        AS.PlayOneShot(ALLAUDIO[index]);
    }

}
