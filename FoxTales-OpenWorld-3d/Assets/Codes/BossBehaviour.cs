using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehaviour : MonoBehaviour
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
    public float time = 0;
    public ParticleSystem fogao;
    public float destroyCD = 1f;


    public enum States
    {
        estagio1,
        atacking,
        stop,
        dead,
        damage,
        patrol,
        breath,


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
            case States.atacking:
                AttackState();
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
            case States.estagio1:
                Estado1();
                break;
            case States.breath:
                Breath();
                break;


        }
    }
    public void Breath()
    {
        agent.isStopped = true;
        anim.SetBool("Attack", true);
        anim.SetBool("Damage", false);
        StartCoroutine(BreathRecharge());
        if(Vector3.Distance(transform.position, target.transform.position) > 9)
        {
            state = States.estagio1;
        }
        if(Vector3.Distance(transform.position, target.transform.position) < 3)
        {
            state = States.atacking;
        }
    }
    IEnumerator BreathRecharge()
    {
        yield return new WaitForSeconds(1f);
        ParticleSystem foguinho = Instantiate(fogao, gameObject.transform.position + gameObject.transform.forward * 2 + gameObject.transform.up, gameObject.transform.rotation);
        foguinho.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 10;
        Destroy(foguinho.gameObject, 1f);
        yield return new WaitForSeconds(10f);
    }
    public void Estado1()
    {
        agent.isStopped = false;
        agent.destination = target.transform.position;
        anim.SetBool("Attack", false);
        anim.SetBool("Damage", false);
        if (Vector3.Distance(transform.position, target.transform.position) < 9)
        {
            state = States.breath;        
        }
       
            



    }
    public void Damage()
    {
        state = States.damage;
        Invoke("ReturnPursuit", 1);
        //StartCoroutine(ReturnDamage());
    }
    public void Dead()
    {
        state = States.dead;
    }
    void AttackState()
    {
        agent.isStopped = true;
        anim.SetBool("Attack", true);
        anim.SetBool("Damage", false);
        if (Vector3.Distance(transform.position, target.transform.position) > 4)
        {
            state = States.breath;
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
            state = States.estagio1;
        }

    }
}
