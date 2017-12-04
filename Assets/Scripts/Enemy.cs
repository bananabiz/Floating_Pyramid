using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Base Enemy Stats")]
    public float startSpeed = 3;
    public float startHealth = 70;
    public int loot = 15;
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
    public void TakeDamage(float damage)
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
    void Update()
    {
        enemyHealthBar.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
    }
}
