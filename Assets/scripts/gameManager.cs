using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class gameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject movieClipCamera;
    public GameObject enemyManager;
    public FirstPersonController playerScript; 
    // Start is called before the first frame update
    void Start()
    {
        showMovieclip();
        ///playerScript = player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMovieclip()
    {

        playerScript.pause();
        movieClipCamera.SetActive(true);
    }

    public void clipEnded()
    {
        movieClipCamera.SetActive(false);
        resumeGame();
    }

    public void resumeGame()
    {
        playerScript.pause();
        enemyManager.SetActive(true);
    }

}
