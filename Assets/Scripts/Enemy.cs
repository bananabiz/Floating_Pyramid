using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Base Enemy Stats")]
    public float startSpeed = 3;
    public float startHealth = 50;
    public int damage = 20;
    public NavMeshAgent nav;
    public Image healthBar;
    public GameObject enemyHealthBar;
    private float health;
    private bool isDead = false;
    
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    protected void Start()
    {
        health = startHealth;
        nav.speed = startSpeed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public IEnumerator Slow(float slow, int slowTime)
    {
        nav.speed = startSpeed * (1f - slow);
        yield return new WaitForSeconds(slowTime);
        if (nav == null)
        {
            yield break;
        }
        else
        {
            nav.speed = startSpeed;
        }
    }
   
}
