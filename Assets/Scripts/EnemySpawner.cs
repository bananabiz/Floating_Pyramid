using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] enemy;

    private GameObject[] enemies;

    void Start()
    {
        
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length < 2)
        {
            int i = Random.Range(0, 2);
            Instantiate(enemy[i], new Vector3(15, 106, 21.5f), Quaternion.identity);
        }
        Debug.Log(enemies.Length);
    }
}