using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    #region Variables
   
    [Header("Bools")]
    public bool showOptions;
    public bool muteToggle;
    
    [Header("Keys")]
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode sprint;
    public KeyCode interact;
    public KeyCode holdingKey;

    [Header("Resolutions and Screen Elements")]
    public int index;
    public bool showRes;
    public bool fullScreenToggle;
    public int[] resX, resY;
    public float scrW, scrH;
    public Vector2 scrollPosRes;

    [Header("References")]
    public AudioSource music;
    public float volumeSlider, holdingVolume;
    public Light dirLight;
    public float brightnessSlider;

    public GUISkin guiSkin;
    public GUIStyle guiStyle;

    #endregion

    // Use this for initialization
    void Start ()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        fullScreenToggle = true;

        dirLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        music = GameObject.Find("MenuMusic").GetComponent<AudioSource>();
        volumeSlider = music.volume;
        brightnessSlider = dirLight.intensity;
	}

    // Update is called once per frame
    void Update()
    {
        if (music != null)
        {
            if (muteToggle == false)
            {
                if (music.volume != volumeSlider)
                {
                    holdingVolume = volumeSlider;
                    music.volume = volumeSlider;
                }
            }
            else
            {
                volumeSlider = 0;
                music.volume = 0;
            }
            
        }
        if (dirLight != null)

        {
            if (brightnessSlider != dirLight.intensity)
            {
                dirLight.intensity = brightnessSlider;
            }
        }
    }

    private void OnGUI()
    {
        if (!showOptions) //if we're on our Main Menu and not our Options Menu
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height),"", guiStyle); //background size, use the exact size
            GUI.skin = guiSkin;

            GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 2 * scrH), "Eric's Awesome World");
            //Buttons
            if (GUI.Button(new Rect(6 * scrW, 4 * scrH, 4 * scrW, scrH), "Play"))
            {
                SceneManager.LoadScene(1);
            }
            if (GUI.Button(new Rect(6 * scrW, 5 * scrH, 4 * scrW, scrH), "Options"))
            {
                showOptions = true;
            }
            if (GUI.Button(new Rect(6 * scrW, 6 * scrH, 4 * scrW, scrH), "Exit"))
            {
                Application.Quit();
            }
            }
        else if (showOptions) //if we're on our Options Menu!!!
        {
            // set our aspect shiz if screen size changes
            if (scrW != Screen.width / 16)
            {
                scrW = Screen.width / 16;
                scrH = Screen.height / 9;
            }
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), ""); //background size, use the exact size
            GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 2 * scrH), "Options"); //title

            if (GUI.Button(new Rect(14.875f * scrW, 8.375f * scrH, scrW, 0.5f * scrH), "Back"))
            {
                showOptions = false;
            }

            #region KeyBinding
            GUI.Box(new Rect(8.75f*scrW, 3f*scrH, 1.25f*scrW, 0.5f*scrH), "Forward");
            GUI.Box(new Rect(8.75f * scrW, 3.75f * scrH, 1.25f * scrW, 0.5f * scrH), "Backward");
            GUI.Box(new Rect(8.75f * scrW, 4.5f * scrH, 1.25f * scrW, 0.5f * scrH), "Left");
            GUI.Box(new Rect(8.75f * scrW, 5.25f * scrH, 1.25f * scrW, 0.5f * scrH), "Right");

            #endregion

            #region Brightness and Audio
            int lightSoundIndex = 0;
            GUI.Box(new Rect(0.25f * scrW, 3f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Volume"); //label
            volumeSlider = GUI.HorizontalSlider(new Rect(2f * scrW, 3.125f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), volumeSlider, 0, 1);
            if (GUI.Button(new Rect(3.75f * scrW, 3f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1f * scrW, 0.5f * scrH), "Mute"))
            {
                ToggleVolume();
            }

            lightSoundIndex++;
            GUI.Box(new Rect(0.25f * scrW, 3f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Brightness"); //label
            brightnessSlider = GUI.HorizontalSlider(new Rect(2f * scrW, 3.125f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), brightnessSlider, 0, 1);

            //box    0.25  ||  3 + i * 0.5    ||  1.75  ||  0.5
            //slider 2     ||  3.125 + i*0.5  ||  1.75  ||  0.25


            /*
            for (int w = 0; w < 16; w++)
            {
                for (int h = 0; h < 9; h++)
                {
                    GUI.Box(new Rect(0f * scrW + w * scrH, 0f * scrH + h * scrH, scrW, scrH), "");
                }
            }
            */

            #endregion

            #region Resolution and Screen
            lightSoundIndex++;
            lightSoundIndex++;
            if (GUI.Button(new Rect(0.25f * scrW, 3f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Resolutions"))
            {
                showRes = !showRes;
            }
            if(GUI.Button(new Rect(2f * scrW, 3f * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "FullScreenToggle"))
            {
                FullScreenToggle();
            }

            lightSoundIndex++;
            if (showRes)
            {
                GUI.Box(new Rect(0.25f * scrW, 3 * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 3.5f * scrH), "");

                scrollPosRes = GUI.BeginScrollView(new Rect(0.25f * scrW, 3 * scrH + (lightSoundIndex * (scrH * 0.5f)), 1.75f * scrW, 3.5f * scrH), scrollPosRes, new Rect(0, 0, 1.75f * scrW, 3.5f * scrH));

                for (int resSize = 0; resSize < resX.Length; resSize++)
                {
                    if (GUI.Button(new Rect(0f * scrW, 0 * scrH + resSize * (scrH * 0.5f), 1.75f * scrW, 0.5f * scrH), resX[resSize].ToString() + "x" + resY[resSize].ToString()))
                    {
                        Screen.SetResolution(resX[resSize], resY[resSize], fullScreenToggle);
                        showRes = false;
                    }
                }
                GUI.EndScrollView();
            }
        
            #endregion
        }

    }
    bool ToggleVolume()
    {
        if(muteToggle == true)
        {
            muteToggle = false;
            volumeSlider = holdingVolume;
            return false;
        }
        else
        {
            muteToggle = true;
            holdingVolume = volumeSlider;
            volumeSlider = 0;
            music.volume = 0;
            return true;
        }
    }

    bool FullScreenToggle()
    {
        if (fullScreenToggle)
        {
            fullScreenToggle = false;
            Screen.fullScreen = false;
            return false;
        }
        else
        {
            fullScreenToggle = true;
            Screen.fullScreen = true;
            return true;
        }
    }

}
