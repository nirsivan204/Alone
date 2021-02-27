using System.Collections;
using System.Collections.Generic;
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


    // Start is called before the first frame update
    void Start()
    {
        numOfparts = 0;
        parts = new GameObject[] { leg_1, leg_2, leg_3, leg_4, body, head };
        txtmgr.showText("Find all the parts before time is running out!!!");
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("part_leg"))
        {
            Destroy(other.gameObject);
            audioSource.Play();
            parts[numOfparts].SetActive(true);
            numOfparts++;
            txtmgr.showText("Collected " + numOfparts + "/4 " + "legs!");
        }
        if (other.CompareTag("part_body"))
        {
            if (numOfparts == 4)
            {
                audioSource.Play();
                Destroy(other.gameObject);
                parts[numOfparts].SetActive(true);
                numOfparts++;
                txtmgr.showText("Collected body!");
            }
            else
            {
                txtmgr.showText("Collect all legs first!");
            }

        }
        if (other.CompareTag("part_head"))
        {
            if (numOfparts == 5)
            {
                audioSource.Play();
                Destroy(other.gameObject);
                parts[numOfparts].SetActive(true);
                numOfparts++;
                txtmgr.showText("You win, restarting in 10 seconds");
                finishGame();
            }
            else
            {
                if (numOfparts == 4)
                {
                    txtmgr.showText("Collect body first!");
                }
                else
                {
                    txtmgr.showText("Collect all legs and the body first!");
                }

            }
        }
    }

    private void finishGame()
    {
        player.SetActive(false);
        GM.showMovieclip(pd_liftOff);
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
