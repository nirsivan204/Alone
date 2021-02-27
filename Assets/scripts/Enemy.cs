using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class hitPlayerEventStartOrStop : UnityEvent<bool>
{
}
public class Enemy : MonoBehaviour
{
    private CharacterController controller;
    public GameObject player;
    public float speed;
    public int demage;
    public int life;
    private Animator animator;
    private BoxCollider col;
    private Rigidbody rb;
    public bool dead = false;
    public float GravityMultiplier;
    private AudioSource deathSound;
    public UnityEvent DieEvent;
    public GameObject parent;
    public hitPlayerEventStartOrStop hitPlayerEventStartOrStop;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        deathSound = GetComponent<AudioSource>();
        //animator.SetTrigger("idle");
    }
    private void Awake()
    {
        DieEvent = new UnityEvent();
        hitPlayerEventStartOrStop = new hitPlayerEventStartOrStop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("idle");
            Invoke("enableMove", 2.0f);
            hitPlayerEventStartOrStop.Invoke(false);
        }
    }

    private void enableMove()
    {
        animator.SetTrigger("run");
        hitPlayerEventStartOrStop.Invoke(true);
    }

    public void hit()
    {
        life--;
        if (life == 0)
        {
            die();
        }
    }

    private void die()
    {
        hitPlayerEventStartOrStop.Invoke(false);
        deathSound.Play();
        animator.SetTrigger("die");
        DieEvent.Invoke();
        dead = true;
    }

}
