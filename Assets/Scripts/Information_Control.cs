using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Information_Control : MonoBehaviour
{
    //public static Information_Control instance;
    public Animator popup;
    public Text title;
    public Text content;
    public bool isOpened = false;

    public void Load()
    {
        SetTitle("Before we start...");
        SetContent("You are here now because you contaminated the environment. To get out of here, you have to solve the given problems. All right, do your best!");
        Appear();
    }

    public void Appear()
    {
        popup.SetBool("Appear", true);
        isOpened = true;
    }

    public void Disappear()
    {
        popup.SetBool("Appear", false);
        isOpened = false;
    }
    public bool isActive()
    {
        return popup.GetBool("Appear");
    }

    public void SetTitle(string _title)
    {
        title.text = _title;
    }

    public void SetContent(string _content)
    {
        content.text = _content;
    }

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
