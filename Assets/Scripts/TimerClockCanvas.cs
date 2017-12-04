using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerClockCanvas : MonoBehaviour {

    public float timer;
    public string clockTime;
    public Text clockText;
    

    // Use this for initialization
    void Start () {

        Time.timeScale = 1;
      
	}

    // Update is called once per frame
    void Update() {

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        int mins = Mathf.FloorToInt(timer / 60);
        int secs = Mathf.FloorToInt(timer - mins * 60);

        clockTime = string.Format("{0:0}:{1:00}", mins, secs);
        clockText.text = clockTime;
    }

    void LateUpdate()
    {
        if (timer < 0)
        {
            timer = 0;
        }
    }
}
