using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NPC/Dialogue")]

public class Dialogue : MonoBehaviour
{
    #region Variables
    [Header("References")]
    public bool showDlg;
    public int index, optionsIndex;
    public GameObject player;
    public MouseLook mainCam;

    [Header("NPC Name and Dialogue")]
    public string npcName;
    public string[] text;
    public GUIStyle dialog;

    #endregion

    #region Start
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
    }
    #endregion

    #region OnGUI
    private void OnGUI()
    {
        if (showDlg)
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;

            GUI.Box(new Rect(6 * scrW, 7 * scrH, 4 * scrW , 1 * scrH), npcName + ": " + text[index], dialog);
            // if not at the end of the dialogue or not at the options part
            if (!(index+1 >= text.Length || index == optionsIndex))
            {
                // next button
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Next", dialog))
                {
                    index++;
                }
            }
            // else if we are at options
            else if (index == optionsIndex)
            {
                // Accept button
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Accept", dialog))
                {
                    index++;
                    movementEnable();
                    Destroy(this.gameObject);
                }
                // Decline button
                if (GUI.Button(new Rect(14 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Decline", dialog))
                {
                    index = text.Length - 1;
                    movementEnable();
                }
            }
        //else we are at the end
        else
        {
            // goodbye
            if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Bye.", dialog))
            {
                    index = 0;
                    movementEnable();
            }
        }

        }
    }
    
    #endregion
    
    void movementEnable()
    {
        showDlg = false;
        mainCam.enabled = true;
        player.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<Movement>().enabled = true;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}