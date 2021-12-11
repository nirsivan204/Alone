using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class baseSpot : MonoBehaviour
{
    private int numOfparts;
    public GameObject leg_1;
    public GameObject leg_2;
    public GameObject leg_3;
    public GameObject leg_4;
    public GameObject body;
    public GameObject head;
    public textManager txtmgr;
    private GameObject[] parts;
    public GameObject player;
    public GameObject finishCamera;
    public GameObject afterburner;
    private Rigidbody rb;
    private AudioSource audioSource;
    public gameManager GM;
    public PlayableDirector pd_liftOff;
    [SerializeField] private WorldProgressBar hammerBar;
    private bool isHammerPhase =false;
    private bool isFinish = false;
    [SerializeField] WorldText baseText;
    public GameObject switchToHammerText;


    // Start is called before the first frame update
    void Start()
    {
        numOfparts = 0;
        parts = new GameObject[] { leg_1, leg_2, leg_3, leg_4, body, head };
        txtmgr.showText("Find all the parts before time is running out!!!");
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        baseText.setText("Collect 4 Wings");
        GM.gameStarted.AddListener(startGame);
    }

    private void startGame()
    {
        baseText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isFinish)
        {

            if (other.CompareTag("part_leg"))
            {
                Destroy(other.gameObject);
                audioSource.Play();
                parts[numOfparts].SetActive(true);
                numOfparts++;
                if (numOfparts < 4)
                {
                    txtmgr.showText("Collected " + numOfparts + "/4 " + "wings!");
                }
                if (numOfparts == 4)
                {
                    txtmgr.showText("Roll Mouse Wheel to Change to Hammer");
                    startHammerPhase();
                }
            }
            if (other.CompareTag("part_body"))
            {
                if (numOfparts == 4)
                {
                    if (hammerBar.isFull)
                    {
                        audioSource.Play();
                        Destroy(other.gameObject);
                        parts[numOfparts].SetActive(true);
                        numOfparts++;
                        txtmgr.showText("Collected body!");
                        startHammerPhase();
                    }
                    else
                    {
                        txtmgr.showText("Hammer All Nails First!");
                    }

                }
                else
                {
                    txtmgr.showText("Collect all wings first!");
                }

            }
            if (other.CompareTag("part_head"))
            {
                if (numOfparts == 5)
                {
                    if (hammerBar.isFull)
                    {
                        audioSource.Play();
                        Destroy(other.gameObject);
                        parts[numOfparts].SetActive(true);
                        numOfparts++;
                        startHammerPhase();
                    }
                    else
                    {
                        txtmgr.showText("Hammer All Nails First!");
                    }
                }
                else
                {
                    if (numOfparts == 4)
                    {
                        if (hammerBar.isFull)
                        {
                            txtmgr.showText("Collect body first!");
                        }
                        else
                        {
                            txtmgr.showText("Hammer All Nails First!");
                        }
                    }
                    else
                    {
                        txtmgr.showText("Collect all wings and the body first!");
                    }

                }
            }
            if (other.CompareTag("hammer"))
            {
                if (isHammerPhase)
                {
                    hammerBar.fill(15);
                    if (hammerBar.isFull)
                    {
                        isHammerPhase = false;
                        if (numOfparts == 4)
                        {
                            txtmgr.showText("Collect Rocket Body");
                            baseText.setText("Collect Rocket Body");
                        }
                        if (numOfparts == 5)
                        {
                            txtmgr.showText("Collect Rocket Head");
                            baseText.setText("Collect Rocket Head");
                        }
                        if (numOfparts == 6)
                        {
                            finishGame();
                        }
                        switchToHammerText.SetActive(false);
                    }
                }
            }
        }
    }

    private void startHammerPhase()
    {

        hammerBar.resetBar();
        isHammerPhase = true;
        txtmgr.showText("Hammer All Nails");
        baseText.setText("Hammer All Nails");
        switchToHammerText.SetActive(true);

    }

    private void finishGame()
    {
        isFinish = true;
        baseText.gameObject.SetActive(false);
        txtmgr.showText("You win, restarting in 10 seconds");
        player.SetActive(false);
        GM.showMovieclip(pd_liftOff, false);
        afterburner.SetActive(true);
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        Invoke("liftOff", 3f);
        Invoke("restart", 10);
    }

    private void liftOff()
    {
        rb.AddForce(Vector3.up*10, ForceMode.Force);
        Invoke("liftOff", 0.1f);
    }

    [System.Obsolete]
    private void restart()
    {
        SceneManager.LoadScene("game");
        //SceneManager.LoadScene("MainMenu");

    }
}
