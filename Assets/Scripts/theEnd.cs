using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public void End()
    {
        LocalDBManager.Instance.Reset();
        LocalDBManager.Instance.SaveGameData();
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
