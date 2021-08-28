using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    public Animator ani;
    public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        ani.Play("Ending Credit");
        Invoke("Load", 30f);
    }
    private void Load()
    {
        SceneManager.LoadScene("the_end");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
