using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGameData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LocalDBManager.Instance.LoadGameData();
        LocalDBManager.Instance.SaveGameData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}