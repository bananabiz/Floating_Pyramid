using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public Transform playerSpawn;

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
            col.transform.position = playerSpawn.position;
            print("You die!");
        }
        else
        {
            print("Something hit me!");
        }
    }
}
