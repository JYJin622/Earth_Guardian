using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferScene : MonoBehaviour
{
    public string transferMapName;
    GameObject player;
    PlayerManager playerScript;

    private bool isNotPrimary = false;

    private PlayerManager thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        player = GameObject.FindGameObjectWithTag("MainCharacter");
        playerScript = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Load()
    {
        SceneManager.LoadScene(transferMapName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "MainCharacter")
        {
            playerScript.inputLeft = false;
            playerScript.inputRight = false;
            playerScript.inputUp = false;
            playerScript.inputDown = false;
            if(transferMapName!="NULL")
            {
                thePlayer.beforeMapName = SceneManager.GetActiveScene().name;
                thePlayer.currentMapName = transferMapName;
            }
            if(transferMapName=="Prov_Info")
            {
                if (!LocalDBManager.Instance.isQuestCleared(1))
                    playerScript.questMode = true;
                else isNotPrimary = true;
            }
            else if (transferMapName == "Prov_Info2")
            {
                if (LocalDBManager.Instance.isQuestCleared(1)&& !LocalDBManager.Instance.isQuestCleared(2))
                    playerScript.questMode = true;
                else isNotPrimary = true;
            }
            if (!isNotPrimary)
                Invoke("Load", 3);
        }
    }
}
