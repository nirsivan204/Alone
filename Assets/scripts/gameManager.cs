using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.Timeline;
public class gameManager : MonoBehaviour
{
    public PlayableDirector pd_start;
    public PlayableDirector pd_liftoff;
    public GameObject player;
    public GameObject movieClipCamera;
    public GameObject enemyManager;
    public FirstPersonController playerScript;
    public TimelineAsset enemyShot;
    public TimelineAsset startingShot;
    private bool game_started = false;

    // Start is called before the first frame update
    void Start()
    {
        //showMovieclip(startingShot);
        showMovieclip(pd_start);
        //Invoke("showMovieclip", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMovieclip(PlayableDirector pd)//TimelineAsset shot)
    {
        playerScript.pause();
        movieClipCamera.SetActive(true);
        pd.Play();
    }

    public void clipEnded()
    {
        movieClipCamera.SetActive(false);
        resumeGame();
    }

    public void resumeGame()
    {
        if (!game_started)
        {
            enemyManager.GetComponent<EnemiesSpawner>().startSpawn();
            game_started = true;
        }
        playerScript.pause();
    }

}
