using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAStarFPS : MonoBehaviour
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
    
    public enum States
    {
        pursuit,
        atacking,
        stoped,
        dead,
        damage,
        patrol,
        
    }

    public States state;

    // Start is called before the first frame update
    void Start()
    {
        patrolposition = new Vector3(transform.position.x + Random.Range(-patrolDistance, patrolDistance), transform.position.y, transform.position.z + Random.Range(-patrolDistance, patrolDistance));
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
            case States.pursuit:
                PursuitState();
                break;
            case States.atacking:
                AttackState();
                break;
            case States.stoped:
                StoppedState();
                break;
            case States.dead:
                DeadState();
                break;
            case States.damage:
                DamageState();
                break;
            case States.patrol:
                PatrolState();
                break;

        }
    }

    void ReturnPursuit()
    {
        state = States.pursuit;
       
    }
    public void Damage()
    {
        state = States.damage;
        Invoke("ReturnPursuit", 1);
        //StartCoroutine(ReturnDamage());
    }
    IEnumerator ReturnDamage()
    {
        for (int i = 0; i < 4; i++)
        {
            render.material.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(0.05f);
            render.material.DisableKeyword("_EMISSION");
            yield return new WaitForSeconds(0.05f);
        }

    }

    public void Dead()
    {
        state = States.dead;
    }


    void PursuitState()
    {
        agent.isStopped = false;
        agent.destination = target.transform.position;
        anim.SetBool("Attack", false);
        anim.SetBool("Damage", false);
        if (Vector3.Distance(transform.position, target.transform.position) < 3)
        {
            state = States.atacking;
        }
        if (Vector3.Distance(transform.position, target.transform.position) >= distancetotrigger)
            Stopped();
    }

    void AttackState()
    {
        agent.isStopped = true;
        anim.SetBool("Attack", true);
        anim.SetBool("Damage", false);
        if (Vector3.Distance(transform.position, target.transform.position) > 4)
        {
            state = States.pursuit;
        }
    }

    void StoppedState()
    {
        agent.isStopped = true;
        anim.SetBool("Attack", false);
        anim.SetBool("Damage", false);
    }

    void DeadState()
    {
        agent.isStopped = true;
        anim.SetBool("Attack", false);
        anim.SetBool("Dead", true);
        anim.SetBool("Damage", false);
    }

    void DamageState()
    {
        agent.isStopped = true;
        anim.SetBool("Damage", true);
    }
    
    void PatrolState()
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
        if (Vector3.Distance(transform.position, target.transform.position) < distancetotrigger)
        {
            state = States.pursuit;
        }
    }
    void Stopped()
    {
        agent.isStopped = true;
        anim.SetBool("Attack", false);
        state = States.patrol;

    }

}
