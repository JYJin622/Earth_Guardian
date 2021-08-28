using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Quest
{
    public Information_Control theInfo;
    public SendMessage quest;
    public ChoiceManager selection;
    public Choice[] choices;
    public int[] answers;
    public string[] alerts;
}

public class Quest_Main1 : MonoBehaviour
{
    [SerializeField]
    private Quest quest;
    public Quest quest1;
    public Quest quest2;
    public int currentStage;
    private int count;

    private int correctState = 0;
    private bool done = false;
    private LocalDBManager gameData;

    private PlayerManager thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (LocalDBManager.Instance.isQuestCleared(1) == true)
        {
            currentStage = 2; 
        }
        else
        {
            currentStage = 1; 
        }
        switch(currentStage)
        {
            case 1: quest = quest1;
                break;
            case 2: quest = quest2;
                break;
            default: break;
        }
        thePlayer = FindObjectOfType<PlayerManager>();
        thePlayer.questMode = true;
        count = 0;
        quest.theInfo.Load();
        quest.selection.ShowChoice(quest.choices[0]);
    }

    void Trans()
    {
        thePlayer.questMode = false;
        thePlayer.currentMapName = "M-2";
        if (currentStage == 1)
            thePlayer.beforeMapName = "Q-1";
        else thePlayer.beforeMapName = "Q-4";
        SceneManager.LoadScene("M-2");
    }

    void Finish()
    {
        FadeObject obj = FindObjectOfType<FadeObject>();
        quest.quest.SetMessage("Finn");
        count++;
        if (quest.theInfo.popup.GetBool("Appear") == false && done == false)
        {
            if(correctState == quest.choices.Length)
            {
                quest.theInfo.SetTitle("Quest Cleared!");
                if(currentStage == 1)
                    quest.theInfo.SetContent("You have cleared this stage. Find the NPC2 and clear Quest 2. Good luck!");
                else
                    quest.theInfo.SetContent("You have cleared this stage.Now, find the exit point to end this game!");
                quest.theInfo.Appear();
            }
            else
            {
                quest.theInfo.SetTitle("A little bit more...");
                quest.theInfo.SetContent("You made some wrong answers.. you can retry this quest.");
                quest.theInfo.Appear();
            }
            done = true;
        }
        if (done == true && !quest.theInfo.isOpened)
        {
            if(correctState == quest.choices.Length)
                LocalDBManager.Instance.QuestClear(currentStage);
            obj.FadeIn(obj.fadeTime);
            Invoke("Trans", 3);
        }
    }

    private void Correct(int round)
    {
        correctState++;
        quest.quest.SetMessage("Correct");
        quest.theInfo.SetTitle("Correct!");
        switch (round)
        {
            case 0:
                quest.theInfo.SetContent(quest.alerts[0]);
                quest.theInfo.Appear();
                break;
            case 1:
                quest.theInfo.SetContent(quest.alerts[1]);
                quest.theInfo.Appear();
                break;
            case 2:
                quest.theInfo.SetContent(quest.alerts[2]);
                quest.theInfo.Appear();
                break;
            default:
                break;

        }
    }

    void Wrong(int round)
    {
        quest.quest.SetMessage("Wrong");
        quest.theInfo.SetTitle("Wrong..");
        switch (round)
        {
            case 0:
                quest.theInfo.SetContent(quest.alerts[0]);
                quest.theInfo.Appear();
                break;
            case 1:
                quest.theInfo.SetContent(quest.alerts[1]);
                quest.theInfo.Appear();
                break;
            case 2:
                quest.theInfo.SetContent(quest.alerts[2]);
                quest.theInfo.Appear();
                break;
            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(quest.theInfo.isOpened == false)
        {
            quest.quest.Appear();
        }
        if (quest.selection.isActive == false)
        {
            if(count < quest.choices.Length)
            {
                if (quest.selection.GetResult() == quest.answers[count])
                {
                    Correct(count);
                }
                else Wrong(count);
            }
            if (count < quest.choices.Length -1)
            {
                count++;
                quest.selection.ShowChoice(quest.choices[count]);
            }
            else Finish();
        }
    }
}
