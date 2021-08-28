using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMessage : MonoBehaviour
{
    public Text text;
    public Animator Ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onStart()
    {
        text.text = "Test";
    }

    public void Disappear()
    {
        Ani.SetBool("Appear", false);
    }
    
    public void Appear()
    {
        Ani.SetBool("Appear", true);
    }

    public void SetMessage(string _msg)
    {
        text.text = _msg;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
