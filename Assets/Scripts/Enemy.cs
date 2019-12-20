using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;
    public float health;
    public float radius = 2.0f;
    void Start()
    {
        health = 100;
        InvokeRepeating("FindPlayers", 1.0f, 1.0f);
    }

    public void TakeDamage()
    {
        StartCoroutine(Wait());
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        health -= 25;
    }

    void FindPlayers()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<CharacterMove>().TakeDamage();
            }
        }
    }
}