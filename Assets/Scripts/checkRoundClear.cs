using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkRoundClear : MonoBehaviour
{
    public GameObject obj;
    public bool isTest = true;
    // Start is called before the first frame update
    void Start()
    {
        if((LocalDBManager.Instance.isQuestCleared(1)&& LocalDBManager.Instance.isQuestCleared(2)) || isTest == true)
        {
            obj.transform.position = new Vector3(500, 500, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
