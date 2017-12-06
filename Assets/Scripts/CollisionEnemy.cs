using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEnemy : Enemy
{
    public GameObject player;
    private CharacterHandler playerCH;

    private void Awake()
    {
        playerCH = player.GetComponent<CharacterHandler>();
    }

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
}
