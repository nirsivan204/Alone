using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.Timeline;
using UnityEngine.Events;

public class gameManager : MonoBehaviour
{
    public PlayableDirector pd_start;
    public PlayableDirector pd_liftoff;
    public FirstPersonController player;
    public GameObject movieClipCamera;
    public GameObject enemyManager;
    public FirstPersonController playerScript;
    public TimelineAsset enemyShot;
    public TimelineAsset startingShot;
    private bool game_started = false;
    public UnityEvent gameStarted; 
    [SerializeField] enemiesUIhandler ui;

    // Start is called before the first frame update
    void Start()
    {
        //showMovieclip(startingShot);
        pd_start.RebuildGraph(); // the graph must be created before getting the playable graph
        pd_start.playableGraph.GetRootPlayable(0).SetSpeed(1.60f);
        showMovieclip(pd_start);

        //SkipClip();
        //Invoke("showMovieclip", 10);
    }

    // Update is called once per frame

    public void showMovieclip(PlayableDirector pd, bool canBeSkipped = true)//TimelineAsset shot)
    {
        playerScript.pause();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        //if(pd == pd_start)
       // {
        movieClipCamera.SetActive(true);
        //}
        //movieClipCamera.SetActive(true);
        currentDirector = pd;
        pd.Play();
        if (canBeSkipped)
        {
            ui.showSkipMsg(true);
        }
    }
    PlayableDirector currentDirector;
    public void SkipClip()
    {
        SkipClip(currentDirector);
    }
    private void SkipClip(PlayableDirector pd)
    {
        //pd. //Stop();
        pd.enabled = false;
        movieClipCamera.SetActive(false);
        resumeGame();
    }

    public void resumeGame()
    {
        if (!game_started)
        {
            enemyManager.GetComponent<EnemiesSpawner>().startSpawn();
            game_started = true;
            gameStarted.Invoke();

        }
        ui.showSkipMsg(false);
        movieClipCamera.SetActive(false);
        playerScript.pause();
    }

}
