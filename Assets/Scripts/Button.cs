using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    GameObject player;
    PlayerManager playerScript;
    // Start is called before the first frame update
    public void Init()
    {
        player = GameObject.FindGameObjectWithTag("MainCharacter");
        playerScript = player.GetComponent<PlayerManager>();
    }

    public void LeftDown()
    {
        playerScript.inputLeft = true;
    }

    public void LeftUp()
    {
        playerScript.inputLeft = false;
    }

    public void RightDown()
    {
        playerScript.inputRight = true;
    }

    public void RightUp()
    {
        playerScript.inputRight = false;
    }

    public void UpDown()
    {
        playerScript.inputUp = true;
        //Invoke("UpUp", 0.1f);
    }

    public void UpUp()
    {
        playerScript.inputUp = false;   
    }

    public void DownDown()
    {
        playerScript.inputDown = true;
    }

    public void DownUp()
    {
        playerScript.inputDown = false;
    }

    public void OKUp()
    {
        playerScript.inputOK = false;
    }

    public void OKDown()
    {
        playerScript.inputOK = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
