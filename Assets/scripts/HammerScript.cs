using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource AS;
    [SerializeField] private Collider hammerTrigger;
    private bool canHammer = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setVisibility(bool state)
    {
        mesh.enabled = state;
    }

    public void shoot()
    {
        if (canHammer)
        {
            canHammer = false;
            animator.SetTrigger("hit");
            AS.Play();
            hammerTrigger.enabled = true;
            Invoke("disableHammerTrigger", 0.3f);
        }
    }

    public void disableHammerTrigger()
    {
        hammerTrigger.enabled = false;
        canHammer = true;
    }
}
