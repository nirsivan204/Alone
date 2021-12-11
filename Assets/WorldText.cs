using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class WorldText : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text text;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position + (transform.position.y - player.transform.position.y)*Vector3.up);
    }

    public void setText(string str)
    {
        text.SetText(str);
    }
}
