using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
    [SerializeField]
    public Choice choice;
    private ChoiceManager theChoice;

    public bool flag;
    void Start()
    {
        theChoice = FindObjectOfType<ChoiceManager>();
        theChoice.ShowChoice(choice);
        Debug.Log(theChoice.GetResult());
    }

}
