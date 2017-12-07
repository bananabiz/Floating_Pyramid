using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerClockHardCode : MonoBehaviour {

    public float timer; //set this to the time you want in seconds + 1 second for PC load Start
    public float timerCount;
    public GUIStyle clock;
    public GameObject player;
    public GameObject youDead;
    private CharacterHandler playerCH;

    // Use this for initialization
    void Start ()
    {
        youDead.SetActive(false);
        Time.timeScale = 1;
        playerCH = player.GetComponent<CharacterHandler>();
        timerCount = timer;
    }
	
	// Update is called once per frame
	void Update () {

        if (timerCount > 0) //if we are greater than 0
        {
            timerCount -= Time.deltaTime; //count down... this may take us below 0

        }
        if (timerCount == 0)
        {
            playerCH.curHealth = 0;
            youDead.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
	}

    void LateUpdate()
    {
        if (timerCount < 0)
        {
            timerCount = 0; //sets timecount back to 0
        }

    }

    void OnGUI()
    {
        //screen by aspect ratio 16:9
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        int mins = Mathf.FloorToInt(timerCount / 60);
        int secs = Mathf.FloorToInt(timerCount - mins * 60);
        string clockTime = string.Format("{0:0}:{1:00}", mins, secs);
        GUI.Box(new Rect(8.5f * scrW, 0.25f * scrH, 1.2f * scrW, 0.4f * scrH), clockTime, clock); //displaying our clock
    }

}
