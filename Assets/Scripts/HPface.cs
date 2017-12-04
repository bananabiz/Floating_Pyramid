using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPface : MonoBehaviour
{
    public Transform target; // player's position
    public float speed = 10f; 

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 targetDir = target.position - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0f); 
        transform.rotation = Quaternion.LookRotation(newDir); //rotate to face player
    }
}
