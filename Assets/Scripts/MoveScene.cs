using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public string transferMapName;
    public AudioSource aud;
    public int fadeTime = 0;
    // Start is called before the first frame update
    public void LoadScene()
    {
        aud.Play();
        if (fadeTime != 0)
        {
            FadeObject obj = FindObjectOfType<FadeObject>();
            if(obj != null)
                obj.FadeIn(fadeTime);
            Invoke("Load", 2f);
        }
        else
            Invoke("Load", 0.5f);
    }

    public void Start()
    {
        LocalDBManager.Instance.LoadGameData();
        if(LocalDBManager.Instance.returnIsSaved() == true)
        {
            transferMapName = "M-1";
        }
    }
    private void Load()
    {
        SceneManager.LoadScene(transferMapName);
    }
}
