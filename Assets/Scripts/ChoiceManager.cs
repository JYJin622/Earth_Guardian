using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;

    public bool isActive = false;
    #region Singleton
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    #endregion Singleton
    private AudioManager theAudio;
    private string question;
    private List<string> answerList;

    public GameObject go;

    public Text question_Text;
    public Text[] answers_text;

    public GameObject[] answer_Panel;

    public Animator anim;

    public string keySound;
    public string enterSound;

    public bool choiceIng;
    private bool keyInput;

    private int count;
    private int result;

    public bool check = true;

    GameObject player;
    PlayerManager playerScript;

    Quest_Main1 qs1;
    Quest_Main1 qs2;


    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCharacter");
        playerScript = player.GetComponent<PlayerManager>();
        theAudio = FindObjectOfType<AudioManager>();
        answerList = new List<string>();
        for(int i=0; i<answers_text.Length; i++)
        {
            answers_text[i].text = "";
            answer_Panel[i].SetActive(false);
        }
        question_Text.text = "";
    }

    public void ShowChoice(Choice _choice)
    {
        go.SetActive(true);
        choiceIng = true;
        Color color = answer_Panel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for (int i = 0; i <= count; i++)
        {
            answer_Panel[i].GetComponent<Image>().color = color;
        }
        result = 0;
        question = _choice.question;
        for (int i = 0; i < _choice.answers.Length; i++)
        {
            answerList.Add(_choice.answers[i]);
            answer_Panel[i].SetActive(true);
            count = i;
        }
        anim.SetBool("Appear", true);
        isActive = true;
        StartCoroutine(ChoiceCoroutine());
    }

    public int GetResult()
    {
        return result;
    }    

    public void ExitChoice()
    {
        question_Text.text = "";
        for (int i=0; i<=count; i++)
        {
            answers_text[i].text = "";
            answer_Panel[i].SetActive(false);
        }

        choiceIng = false;
        anim.SetBool("Appear", false);
        answerList.Clear();
        isActive = false;
        //go.SetActive(false); //////////
    }

    IEnumerator ChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(TypingQuestion());
        StartCoroutine(TypingAnswer(1));
        if(count >= 1)
            StartCoroutine(TypingAnswer(2));
        if(count >= 2)
            StartCoroutine(TypingAnswer(3));
        if(count >= 3)
            StartCoroutine(TypingAnswer(4));
        yield return new WaitForSeconds(0.5f);

        keyInput = true;
    }    

    IEnumerator TypingQuestion()
    {
        for(int i=0; i<question.Length; i++)
        {
            question_Text.text += question[i];
            yield return waitTime;
        }
    }
    IEnumerator TypingAnswer(int p)
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < answerList[p-1].Length; i++)
        {
            answers_text[p-1].text += answerList[p-1][i];
            yield return waitTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(keyInput&&check)
        {
            check = false;
            if (playerScript.inputUp == true)
            {
                theAudio.Play(keySound);
                if (result > 0)
                    result--;
                else
                    result = count;
                Selection();
            }
            else if (playerScript.inputDown == true)
            {
                theAudio.Play(keySound);
                if (result < count)
                    result++;
                else
                    result = 0;
                Selection();
            }
            else if(playerScript.inputOK == true && (!qs1.isOpenedDialog() || !qs2.isOpenedDialog()))
            {
                theAudio.Play(enterSound);
                keyInput = false;
                ExitChoice();
            }
            StartCoroutine(WaitForIt());
        }
    }
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.1f);
        check = true;
    }
    public void Selection()
    {
        Color color = answer_Panel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for(int i=0; i<=count; i++)
        {
            answer_Panel[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        answer_Panel[result].GetComponent<Image>().color = color;
    }
}
