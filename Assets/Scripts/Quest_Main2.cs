using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest_Main2 : MonoBehaviour
{
    [SerializeField]
    public Information_Control theInfo;
    public SendMessage quest;
    public ChoiceManager selection;
    public Choice[] choices;
    public int[] answers;
    private int count;

    private int correctState = 0;
    private bool done = false;
    private LocalDBManager gameData;

    private PlayerManager thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        count = 0;
        theInfo.SetTitle("Before we start...");
        theInfo.SetContent("You are here now because you contaminated the environment. To get out of here, you have to solve the given problems. All right, do your best!"); ;
        theInfo.Appear();
        selection.ShowChoice(choices[0]);
    }

    void Trans()
    {
        thePlayer.questMode = false;
        thePlayer.currentMapName = "M-2";
        thePlayer.beforeMapName = "Q-4";
        SceneManager.LoadScene("M-2");
    }

    void Finish()
    {
        
        quest.SetMessage("Finn");
        count++;
        if (theInfo.isOpened == false && done == false)
        {
            if(correctState == choices.Length)
            {
                theInfo.SetTitle("Quest Cleared!");
                theInfo.SetContent("You have cleared this stage. Now, find the exit point to end this game!");
                theInfo.Appear();
            }
            else
            {
                theInfo.SetTitle("A little bit more...");
                theInfo.SetTitle("You made some wrong answers.. you can retry this quest.");
            }
            done = true;
        }
        if (done == true && !theInfo.isOpened)
        {
            if(correctState == choices.Length)
                LocalDBManager.Instance.QuestClear(2);
            count = 0;
            Trans();
        }
    }

    private void Correct(int round)
    {
        correctState++;
        quest.SetMessage("Correct");
        theInfo.SetTitle("Correct!");
        switch (round)
        {
            case 0:
                theInfo.SetContent("This is Content 1");
                theInfo.Appear();
                break;
            case 1:
                theInfo.SetContent("This is Content 2");
                theInfo.Appear();
                break;
            case 2:
                theInfo.SetContent("This is Content 3");
                theInfo.Appear();
                break;
            default:
                break;

        }
    }

    void Wrong(int round)
    {
        quest.SetMessage("Wrong");
        theInfo.SetTitle("Correct!");
        switch (round)
        {
            case 0:
                theInfo.SetContent("This is Content 1");
                theInfo.Appear();
                break;
            case 1:
                theInfo.SetContent("This is Content 2");
                theInfo.Appear();
                break;
            case 2:
                theInfo.SetContent("This is Content 3");
                theInfo.Appear();
                break;
            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(theInfo.isOpened == false)
        {
            quest.Appear();
        }
        if (selection.isActive == false)
        {
            if(count < choices.Length-1)
            {
                if (selection.GetResult() == answers[count])
                {
                    Correct(count);
                }
                else Wrong(count);
            }
            if (count < choices.Length -1)
            {
                count++;
                selection.ShowChoice(choices[count]);
            }
            else Finish();
        }
    }
}
