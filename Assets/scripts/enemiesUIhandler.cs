using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemiesUIhandler : MonoBehaviour
{
    public GameObject enemySpawner;
    public TextMeshProUGUI enemiesTextComponent;
    public TextMeshProUGUI nWaveTimeTextComponent;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner.GetComponent<EnemiesSpawner>().updateUIevent.AddListener(updateUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateUI(int numOfEnemies,int timeUntilNextWave)
    {
        enemiesTextComponent.text = numOfEnemies.ToString();
        nWaveTimeTextComponent.text = timeUntilNextWave.ToString();
    }
}
