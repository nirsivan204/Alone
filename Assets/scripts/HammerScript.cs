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
    [SerializeField] private ParticleSystem effect1;
    [SerializeField] private ParticleSystem effect2;

    private bool canHammer = true;
    [SerializeField] private bool hit = false;
    public bool showHit = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (showHit && hit)
        {
            Instantiate(effect1, transform);
            Instantiate(effect2, transform);
            hit = false;
            AS.Play();
        }
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
            hammerTrigger.enabled = true;
            //Instantiate(effect,transform);
            Invoke("disableHammerTrigger", 0.3f);
        }
    }

    public void disableHammerTrigger()
    {
        hammerTrigger.enabled = false;
        canHammer = true;
    }
}
