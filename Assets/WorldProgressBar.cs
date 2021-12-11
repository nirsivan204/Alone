using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldProgressBar : MonoBehaviour
{
    [SerializeField] GameObject bar;
    [SerializeField] GameObject player;
    public float progress = 0;
    public bool isFull = false;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position + (transform.position.y - player.transform.position.y)*Vector3.up);
    }

    public void resetBar()
    {
        progress = 0;
        bar.transform.localScale = new Vector3(0,bar.transform.localScale.y, bar.transform.localScale.z);
        gameObject.SetActive(true);
        isFull = false;
    }
    public void fill(float amount)
    {
        if (progress + amount < 100)
        {
            bar.transform.localScale += Vector3.right * amount / 100;
            progress += amount;
        }
        else
        {
            print("bar full");
            isFull = true;
            gameObject.SetActive(false);

        }
    }
}
