using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionLava : MonoBehaviour
{
    public Transform playerSpawn;
    public GameObject player;
    public GameObject youDead;
    public GameObject timer;
    private TimerClockHardCode startTime;
    private CharacterHandler playerCH;

    private void Awake()
    {
        playerCH = player.GetComponent<CharacterHandler>();
        startTime = timer.GetComponent<TimerClockHardCode>();
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
            playerCH.curHealth = 0;
            youDead.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            print("You are dead!");
        }
        else
        {
            print("Something hit me!");
        }
    }

    public void Reborn()
    {
        Time.timeScale = 1;
        youDead.SetActive(false);
        startTime.timerCount = startTime.timer;
        playerCH.curHealth = playerCH.maxHealth;
        playerCH.curMana = playerCH.maxMana;
        playerCH.curStamina = playerCH.maxStamina;
        player.transform.position = playerSpawn.position;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
