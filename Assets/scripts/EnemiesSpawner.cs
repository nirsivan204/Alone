using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class updateUIEvent : UnityEvent<int , int>
{
}

public class EnemiesSpawner : MonoBehaviour
{
    public Transform spawnPlace1;
    public Transform spawnPlace2;
    public Transform spawnPlace3;
    public Transform spawnPlace4;
    private Transform[] places;
    public int enemyNumAtStart;
    public int enemyToAdd;
    private int timeUntilNextWave;
    public int timeBetweenWaves;
    private int timeToFirstWave;
    private int waveNum;
    private int livingEnemies;
    private int enemeiesLeftToSpwan;
    private int lastPlaceIndex;
    public GameObject enemy;
    private bool finishSpawn;
    public textManager txtmgr;
    public updateUIEvent updateUIevent;
    private bool isPause = true;
    private void Awake()
    {
        updateUIevent = new updateUIEvent();
    }
    // Start is called before the first frame update

    void Start()
    {
        places = new Transform[] { spawnPlace1, spawnPlace2, spawnPlace3, spawnPlace4};
        finishSpawn = false;
        timeToFirstWave = 5;
        timeUntilNextWave = timeToFirstWave;
        updateUI();
        countTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(finishSpawn)
        {
            waveNum++;
            timeUntilNextWave = timeBetweenWaves;
            countTime();
            finishSpawn = false;
        }
    }

    private void countTime()
    {
        updateUI();
        if (timeUntilNextWave > 0)
        {
            timeUntilNextWave--;
            Invoke("countTime", 1);
        }
        else
        {
            startNextWave();
        }

    }

    private void startNextWave()
    {
        txtmgr.showText("Starting wave no. " + (waveNum+1));
        enemeiesLeftToSpwan = enemyNumAtStart + enemyToAdd * waveNum;
        spawn();
    }
    private void spawn()
    {
        if(enemeiesLeftToSpwan == 0)
        {
            finishSpawn = true;
            return;
        }
        if(lastPlaceIndex == places.Length)
        {
            lastPlaceIndex = 0;
        }
        GameObject clone = Instantiate(enemy, transform);
        clone.transform.position = places[lastPlaceIndex].position;
        clone.SetActive(true);
        clone.GetComponentInChildren<Enemy>().DieEvent.AddListener(killOneEnemy);
        enemeiesLeftToSpwan--;
        lastPlaceIndex++;
        livingEnemies++;
        updateUI();
        Invoke("spawn", 0.5f);
    }

    private void updateUI()
    {
        updateUIevent.Invoke(livingEnemies, timeUntilNextWave);
    }

    public void killOneEnemy()
    {
        livingEnemies--;
    }

}
