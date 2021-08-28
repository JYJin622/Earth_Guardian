using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap: MonoBehaviour
{
    public string transferMapName;

    public Transform target;
    public BoxCollider2D targetBound;

    GameObject player;
    PlayerManager playerScript;


    private PlayerManager thePlayer;
    private CameraManager theCamera;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        player = GameObject.FindGameObjectWithTag("MainCharacter");
        playerScript = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MainCharacter")
        {
            /*playerScript.inputLeft = false;
            playerScript.inputRight = false;
            playerScript.inputUp = false;
            playerScript.inputDown = false;*/
            thePlayer.currentMapName = transferMapName;
            theCamera.SetBound(targetBound);
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = target.transform.position;
        }
    }


}