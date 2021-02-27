using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.Timeline;
public class gameManager : MonoBehaviour
{
    public PlayableDirector pd;
    public GameObject player;
    public GameObject movieClipCamera;
    public GameObject enemyManager;
    public FirstPersonController playerScript;
    public TimelineAsset enemyShot;
    public TimelineAsset startingShot;

    // Start is called before the first frame update
    void Start()
    {
        //showMovieclip(startingShot);
        showMovieclip();
        //Invoke("showMovieclip", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMovieclip()//TimelineAsset shot)
    {
        //pd.playableAsset = startingShot;
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
