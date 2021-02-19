using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testnavmesh : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private bool isMoving;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Enemy>().hitPlayerEventStartOrStop.AddListener(startOrStop);
        GetComponentInChildren<Enemy>().DieEvent.AddListener(waitForAnimation);
        agent = GetComponent<NavMeshAgent>();
        isMoving = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && agent.isOnNavMesh)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void startOrStop(bool move)
    {
        isMoving = move;
        agent.enabled = isMoving;
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

}
