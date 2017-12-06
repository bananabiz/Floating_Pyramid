using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLava : MonoBehaviour
{
    public Transform playerSpawn;
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
            col.transform.position = playerSpawn.position;
            playerCH.curHealth = 100;
            print("You die!");
        }
        else
        {
            print("Something hit me!");
        }
    }
}
