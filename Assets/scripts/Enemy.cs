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
    private bool canMove;
    private Animator animator;
    private BoxCollider col;
    private Rigidbody rb;
    bool dead = false;
    public float GravityMultiplier;
    private AudioSource deathSound;
    public UnityEvent DieEvent;
    public GameObject parent;
    public hitPlayerEventStartOrStop hitPlayerEventStartOrStop;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        deathSound = GetComponent<AudioSource>();

    }
    private void Awake()
    {
        DieEvent = new UnityEvent();
        hitPlayerEventStartOrStop = new hitPlayerEventStartOrStop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            Vector3 movement = Physics.gravity * GravityMultiplier;
            //controller.Move(movement * speed * Time.fixedDeltaTime);
            if (canMove && !dead)
            {
                //movement += Vector3.Normalize(player.transform.position - transform.position);
                //transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            //controller.Move(movement * speed * Time.fixedDeltaTime);
            //agent.Warp(Vector3.zero);
            }
       
        //if (dead)
       // {
            //controller.SimpleMove(Vector3.down * 10);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canMove = false;
            animator.SetTrigger("idle");
            Invoke("enableMove", 2.0f);
            hitPlayerEventStartOrStop.Invoke(false);
        }
    }

    private void enableMove()
    {
        canMove = true;
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
        canMove = false;
        animator.SetTrigger("die");
        DieEvent.Invoke();
        dead = true;
    }

}
