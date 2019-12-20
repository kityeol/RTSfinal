using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CharacterMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public ParticleSystem clickParticle;
    public ParticleSystem enemyParticle;
    public float range = 10.0f;
    public float health;
    GameObject target;
    public GameObject playerDeathEffect;
    public GameObject selected;
    public Animator anim;
    private bool Walking = false;

    void Start()
    {
        health = 100;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        agent.isStopped = false;
        Vector3 lookAt = new Vector3(agent.destination.x, agent.transform.position.y, agent.destination.z);
        agent.transform.LookAt(lookAt);

        if (Vector3.Magnitude(agent.velocity) == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        if (target == null)
        {
            return;
        }

        agent.destination = target.transform.position;

        if (Vector3.Distance(agent.destination, transform.position) > range)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            target.GetComponent<Enemy>().TakeDamage();
            StartCoroutine(Wait());
            agent.isStopped = true;
        }

    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    public void SetDestination(Vector3 des)
    {
        agent.SetDestination(des);
    }

    IEnumerator Wait()
    {
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Attacking", false);
    }

    public void TakeDamage()
    {
        StartCoroutine(Wait2());
        Instantiate(playerDeathEffect, transform.position, Quaternion.identity);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(5);
        health -= 5;
    }
}