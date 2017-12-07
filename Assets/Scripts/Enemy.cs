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
    public int damage = 10;
    public float deathDelay = 1f;
    public NavMeshAgent nav;
    private Collider enemyCollider;

    [Header("Base Enemy HP")]
    public Image healthBar;
    public GameObject enemyHealthBar;
    public GameObject player;
    private CharacterHandler playerCH;
    private float health;
    private bool isDead = false;
    
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCH = player.GetComponent<CharacterHandler>();
        enemyCollider = this.GetComponent<Collider>(); 
    }

    protected virtual void Start()
    {
        health = startHealth;
        nav.speed = startSpeed;
    }

    //health decreased once collide with player
    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    //detect collision with player, player health decreased
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerCH.curHealth -= damage;
            TakeDamage(playerCH.attackDamage);
        }
        else
        {
            print("Something hit me!");
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
        StartCoroutine(OnDeath(deathDelay));
    }

    //delay seconds to display death animation
    IEnumerator OnDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    //slow down enemy movement
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
