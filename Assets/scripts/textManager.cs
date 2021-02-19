using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textManager : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private Animator animator;
    bool isShowingText;
    private Queue<string> textQueue;
    // Start is called before the first frame update
    void Start()
    {
        isShowingText = false;
    }

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
        textQueue = new Queue<string>();
}

// Update is called once per frame
void Update()
    {
        if(textQueue.Count > 0 && !isShowingText)
        {
            showFromQueue();
        }
    }

    public void showText(string msg)
    {
        textQueue.Enqueue(msg);
    }
    private void showFromQueue()
    {
        textComponent.SetText(textQueue.Dequeue());
        animator.SetTrigger("showText");
        isShowingText = true;
        Invoke("stopShowing", 2.5f);
    }

    private void stopShowing()
    {
        isShowingText = false;
    }
}
