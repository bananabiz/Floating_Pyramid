﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerClockHardCode : MonoBehaviour {

    public float timer; //set this to the time you want in seconds + 1 second for PC load Start
    public GUIStyle clock;

	// Use this for initialization
	void Start () {

        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {

        if (timer > 0) //if we are greater than 0
        {
            timer -= Time.deltaTime; //count down... this may take us below 0

        }
        if (timer == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    void LateUpdate()
    {
        if (timer < 0)
        {
            timer = 0; //so this sets us back to 0
        }

    }

    void OnGUI()
    {
        //screen by aspect ratio 16:9
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        int mins = Mathf.FloorToInt(timer / 60);
        int secs = Mathf.FloorToInt(timer - mins * 60);
        string clockTime = string.Format("{0:0}:{1:00}", mins, secs);
        GUI.Box(new Rect(8.5f * scrW, 0.25f * scrH, 1.2f * scrW, 0.4f * scrH), clockTime, clock); //displaying our clock
    }

}
