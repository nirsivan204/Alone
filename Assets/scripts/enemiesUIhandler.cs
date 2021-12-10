using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class enemiesUIhandler : MonoBehaviour
{
    public GameObject enemySpawner;
    public TextMeshProUGUI enemiesTextComponent;
    public TextMeshProUGUI nWaveTimeTextComponent;
    public GameObject skipMsg;
    public gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner.GetComponent<EnemiesSpawner>().updateUIevent.AddListener(updateUI);
    }

    // Update is called once per frame

    public void updateUI(int numOfEnemies,int timeUntilNextWave)
    {
        enemiesTextComponent.text = numOfEnemies.ToString();
        nWaveTimeTextComponent.text = timeUntilNextWave.ToString();
    }

    internal void showSkipMsg(bool v)
    {
        skipMsg.gameObject.SetActive(v);
    }
}
