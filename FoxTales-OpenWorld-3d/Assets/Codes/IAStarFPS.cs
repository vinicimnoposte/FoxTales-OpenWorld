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
    public GameObject pontoFraco;
    public IADamage vidas;
    public ParticleSystem fogao;


    public enum States
    {
        pursuit,
        atacking,
        stoped,
        dead,
        damage,
        patrol,
        estagio1,
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
        if (Vector3.Distance(transform.position, target.transform.position) > 9)
        {
            state = States.pursuit;
        }
        if (Vector3.Distance(transform.position, target.transform.position) < 3)
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

    void ReturnPursuit()
    {
        state = States.pursuit;
       
    }
    public void Estado1()
    {
        if (vidas.lives <= 10)
            pontoFraco.SetActive(true);

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
        if (Vector3.Distance(transform.position, target.transform.position) < 5)
        {
            state = States.breath;
        }
        if (Vector3.Distance(transform.position, target.transform.position) >= distancetotrigger)
            state = States.patrol;
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
