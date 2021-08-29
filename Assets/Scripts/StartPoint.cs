using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    public string startPoint;
    public string startVar;
    private PlayerManager thePlayer;
    private CameraManager theCamera;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();
        if (startPoint == thePlayer.currentMapName)
        {
            if(thePlayer.beforeMapName == "Prov_Info")
            {
                theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = this.transform.position;
            }
            else if(thePlayer.beforeMapName == startVar)
            {
                theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = this.transform.position;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
