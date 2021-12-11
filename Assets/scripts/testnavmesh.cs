using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testnavmesh : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private bool isMoving = false;
    private Rigidbody rb;
    private Collider coll;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Enemy>().hitPlayerEventStartOrStop.AddListener(startOrStop);
        GetComponentInChildren<Enemy>().DieEvent.AddListener(waitForAnimation);
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && agent.isOnNavMesh)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (!isMoving && agent.isOnNavMesh)
            {
                agent.SetDestination(target);
            }
        }
    }

    private void startOrStop(bool move)
    {
        isMoving = move;
        if (!isMoving)
        {
            target = player.transform.position - transform.forward * 2;
        }
        //agent.enabled = isMoving;
    }

    private void waitForAnimation()
    {
        rb.AddForce(Vector3.up * 20);
        Invoke("die", 5f);

    }
    private void die()
    {

        Destroy(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        startFollowPlayer();
    }

    private void startFollowPlayer()
    {
        isMoving = true;
        coll.enabled = false;
        rb.useGravity = false;
        agent.enabled = true;

    }
}
