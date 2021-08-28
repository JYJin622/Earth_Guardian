using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManage : MonoBehaviour
{
    private PlayerManager thePlayer;
    private Text cStatus;
    public Text save;
    public Text load;
    private bool isActive = false;
    public void Load()
    {
        if(!isActive)
        {
            isActive = true;
            StartCoroutine(FadeTextToZeroAlpha(save));
            StartCoroutine(FadeTextToZeroAlpha(load));
            LocalDBManager.Instance.LoadGameData();
            cStatus.text = "Loading...";
            Invoke("Load", 1);
            cStatus.text = "Loading Complete";
            Invoke("Return", 3);
        }

    }

    public void Return()
    {
        if (thePlayer.currentMapName == "Start")
        {
            SceneManager.LoadScene("M-1");
        }
        else
        {
            string temp = thePlayer.beforeMapName;
            if (temp == string.Empty)
                temp = "M-1";
            thePlayer.beforeMapName = "GameSave";
            SceneManager.LoadScene(temp);
        }
    }

    public void Save()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(FadeTextToZeroAlpha(save));
            StartCoroutine(FadeTextToZeroAlpha(load));
            LocalDBManager.Instance.isSave();
            LocalDBManager.Instance.SaveGameData();

            cStatus.text = "Saving...";
            Invoke("Save", 1);
            cStatus.text = "Saved";
            Invoke("Return", 3);
        }        
    }
    // Start is called before the first frame update
    void Start()
    {
        cStatus = GameObject.Find("Check").GetComponent<Text>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    public IEnumerator FadeTextToFullAlpha(Text text) // 알파값 0에서 1로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(Text text)  // 알파값 1에서 0으로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
