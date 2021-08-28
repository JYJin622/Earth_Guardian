using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    static public PlayerManager instance;

    public string currentMapName;
    public string beforeMapName;
    public bool questMode = false;

    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;

    private AudioManager theAudio;


    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputUp = false;
    public bool inputDown = false;
    public bool inputOK = false;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;


    private bool canMove = true;
    public bool notMove = false;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            theAudio = FindObjectOfType<AudioManager>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Button ui = GameObject.FindGameObjectWithTag("Managers").GetComponent<Button>();
        ui.Init();
    }

    bool GetInput()
    {
        if ((inputLeft == true || inputRight == true || inputUp == true || inputDown == true) && !questMode)
            return true;
        else return false;
    }
    float GetAxisData(int data)
    {
        if (data == 1)
        {
            if (inputLeft == true) return -1;
            else if (inputRight == true) return 1;
            else return 0;
        }
        else
        {
            if (inputUp == true) return 1;
            else if (inputDown == true) return -1;
            else return 0;
        }
    }

    IEnumerator MonoCoroutine()
    {
        while (GetInput() && !notMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }
            vector.Set(GetAxisData(1), GetAxisData(2), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;


            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);


            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
                break;

            animator.SetBool("Walking", true);

            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;
                case 2:
                    theAudio.Play(walkSound_2);
                    break;
                case 3:
                    theAudio.Play(walkSound_3);
                    break;
                case 4:
                    theAudio.Play(walkSound_4);
                    break;

            }

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);

            }

            currentWalkCount = 0;
        }


        animator.SetBool("Walking", false);
        canMove = true;
    }
    // Update is called once per frame
    void Update()
    {

        if (canMove && !notMove)
        {
            if (GetInput() )
            {
                canMove = false;
                StartCoroutine(MonoCoroutine());
            }
        }
    }
}
