using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Info_main : MonoBehaviour
{
    [SerializeField]
    public Information_Control theInfo;
    public SendMessage quest;
    public string[] Messages;
    public string[] Contents;
    GameObject player;
    PlayerManager playerScript;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCharacter");
        playerScript = player.GetComponent<PlayerManager>();
        quest.SetMessage("Quest..");
        theInfo.Disappear();
        NextWord();
    }
    IEnumerator TypingAnswer(int p)
    {
        quest.text.text = "";
        yield return new WaitForSeconds(0.4f);
        if (Messages[p].Length > 130) quest.text.fontSize = 30;
        for (int i = 0; i < Messages[p].Length; i++)
        {
            quest.text.text += Messages[p][i];
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void NextWord()
    { 
        StopAllCoroutines();
        StartCoroutine(TypingAnswer(count));
        count++;
    }

    public void Trans()
    {
        playerScript.inputOK = false;
        SceneManager.LoadScene("Quest1");
    }
    // Update is called once per frame
    void Update()
    {
        if(playerScript.inputOK == true && count < Messages.Length)
        {
            NextWord();
            playerScript.inputOK = false;
        }
        if (count >= Messages.Length && playerScript.inputOK == true && count < Messages.Length + Contents.Length)
        {
            theInfo.SetTitle("The content of the quiz");
            theInfo.SetContent(Contents[count - Messages.Length]);
            theInfo.Appear();
            count++;
            playerScript.inputOK = false;
        }
        if(count >= Messages.Length + Contents.Length && playerScript.inputOK == true) Trans();
    }
}
