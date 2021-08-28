using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPressed : MonoBehaviour
{
    private SendMessage obj;
    // Start is called before the first frame update
    void Start()
    {
        obj= FindObjectOfType<SendMessage>();
        obj.Disappear();
        Invoke("Load", 2);
    }

    void Load()
    {
        //obj.onLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
