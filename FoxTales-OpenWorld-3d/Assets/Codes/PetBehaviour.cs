using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetBehaviour : MonoBehaviour
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
    void StateMachine()
    {
        switch (state)
        {
            case PetState.petChase:
                PetChase();
                break;
            case PetState.stopped:
                Stopped();
                break;
        }
    }

    public enum PetState
    {
        petChase,
        stopped,
    }

    public PetState state;
    void PetChase()
    {
        agent.isStopped = false;
        agent.destination = target.transform.position;
        anim.SetBool("Attack", false);
        anim.SetBool("Damage", false);
        if (Vector3.Distance(transform.position, target.transform.position) < 3)
        {
            state = PetState.stopped;
        }
    }
    void Stopped()
    {
        agent.isStopped = true;
        anim.SetBool("Attack", false);
        anim.SetBool("Damage", false);
        if (Vector3.Distance(transform.position, target.transform.position) > 3)
        {
            state = PetState.petChase;
        }
    }
}
