using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public Animator anim;
    public SkinnedMeshRenderer render;
    public Vector3 patrolposition;
    public float patrolDistance = 10;
    public float stoppedTime;
    public float distancetotrigger = 10;
    public float timetowait = 3;
    public float distance = 10;
    public NPCState state;
    public GameObject dialogo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        anim.SetFloat("Velocidade", agent.velocity.magnitude);
    }
    public enum NPCState
    {
        npcPatrol,
        stopped,
    }
    void StateMachine()
    {
        switch (state)
        {
            case NPCState.npcPatrol:
                NPCPatrol();
                break;
            case NPCState.stopped:
                Stopped();
                break;
        }
    }

    void Stopped()
    {
        agent.isStopped = true;
        anim.SetBool("Attack", false);
        anim.SetBool("Damage", false);
        if (Vector3.Distance(transform.position, target.transform.position) > 3)
        {
            state = NPCState.npcPatrol;
        }
    }
    void NPCPatrol()
    {
        agent.isStopped = false;
        agent.SetDestination(patrolposition);
        anim.SetBool("Attack", false);
        //tempo parado
        if (agent.velocity.magnitude < 0.1f)
        {
            stoppedTime += Time.deltaTime;
        }
        //se for mais q timetowait segundos
        if (stoppedTime > timetowait)
        {
            stoppedTime = 0;
            patrolposition = new Vector3(transform.position.x + Random.Range(-patrolDistance, patrolDistance), transform.position.y, transform.position.z + Random.Range(-patrolDistance, patrolDistance));
        }
        //ditancia do jogador for menor q distancetotrigger
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            state = NPCState.stopped;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Player" && Input.GetKeyDown(KeyCode.E))
        {
            dialogo.SetActive(true);
        }
        if (Vector3.Distance(transform.position, target.transform.position) > distance)
        {
            dialogo.SetActive(false);
        }

    }
}
