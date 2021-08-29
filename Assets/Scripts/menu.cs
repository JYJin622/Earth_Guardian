using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour{

    public static menu instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }


    public GameObject go;
    public AudioManager theAudio;

    public string call_sound;
    public string cancel_sound;


    public GameObject[] gos;

    private bool activated;

    public void Exit()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        //System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    public void Continue()
    {
        activated = false;
        go.SetActive(false);
        theAudio.Play(cancel_sound);
    }
    public void SaveData()
    {
        SceneManager.LoadScene("GameSave");
    }
    public void GoToTitle()
    {
        for (int i = 0; i < gos.Length; i++)
            Destroy(gos[i]);
        go.SetActive(false);
        activated = false;
        SceneManager.LoadScene("title");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activated = !activated;

            if (activated)
            {
                go.SetActive(true);
                theAudio.Play(call_sound);
            }
            else
            {
                go.SetActive(false);
                theAudio.Play(cancel_sound);
            }
        }
    }
}
