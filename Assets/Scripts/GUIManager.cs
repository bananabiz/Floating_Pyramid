using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GUIManager : MonoBehaviour
{

    #region Variables
    //public bool gameScene, showOptions, pause;
    //public GameObject menu, options;

    [Header("Bools")]
    public bool gameScene;
    public bool showOptions;
    public bool pause;
    public bool fullScreenToggle;
    public bool ismute;

    [Header("Resolutions")]
    public int index;
    public int[] resX, resY;
    public Dropdown resolutionDropdown;
    //Dropdown has a value variable that you can use to reference arrays

    [Header("Keys")]
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode sprint;
    public KeyCode interact;
    // this remembers the key code fo a key
    // we are trying to change
    public KeyCode holdingKey;

    [Header("GUI Text")]
    public Text forwardText;
    public Text backwardText;
    public Text leftText;
    public Text rightText;
    public Text jumpText;
    public Text crouchText;
    public Text sprintText;
    public Text interactText;

    [Header("GUI Elements")]
    public GameObject menu;
    public GameObject options;
    public Toggle fullWindowToggle;

    [Header("References")]
    public Slider volumeSlider;
    public Scrollbar brightnessScrollbar;
    public AudioSource music;
    public Light dirLight;

    #endregion

    void Start()
    {
        Time.timeScale = 1;

        #region MainMenu
        fullScreenToggle = true; //change when you load in the info
        
        if (PlayerPrefs.HasKey("Volume"))
        {
            Load();
        }
        if (volumeSlider != null && music != null)
        {
            volumeSlider.value = music.volume;
        }
        if (brightnessScrollbar != null && dirLight != null)
        {
            brightnessScrollbar.value = dirLight.intensity;
        }

        #region PauseMenu
        if (gameScene)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        #endregion
        #endregion
        #region SetUp Keys
        //Set out keys to the preset keys we may have saved, else set keys to default
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));

        forwardText.text = forward.ToString();
        backwardText.text = backward.ToString();
        leftText.text = left.ToString();
        rightText.text = right.ToString();
        jumpText.text = jump.ToString();
        crouchText.text = crouch.ToString();
        sprintText.text = sprint.ToString();
        interactText.text = interact.ToString();

        #endregion

    }
    void Update()
    {
        if (volumeSlider != null && music != null)
        {
            // Debug.Log("1");
            if (music.volume != volumeSlider.value)
            {
                // Debug.Log("2");
                music.volume = volumeSlider.value;
            }
        }
        if (brightnessScrollbar != null && dirLight != null)

        {
            if (brightnessScrollbar.value != dirLight.intensity)
            {
                dirLight.intensity = brightnessScrollbar.value;
            }
        }
        if (gameScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void ShowOptions()
    {
        ToggleOptions();
    }
    public bool ToggleOptions()
    {
        if (showOptions)
        {
            showOptions = false;
            menu.SetActive(true);
            options.SetActive(false);
            return false;
        }
        else
        {
            showOptions = true;
            menu.SetActive(false);
            options.SetActive(true);
            return true;
        }
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", music.volume);
        PlayerPrefs.SetFloat("Brightness", dirLight.intensity);

        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());
    }
    public void Load()
    {
        music.volume = PlayerPrefs.GetFloat("Volume");
        dirLight.intensity = PlayerPrefs.GetFloat("Brightness");

        forwardText.text = PlayerPrefs.GetString("Forward");
        backwardText.text = PlayerPrefs.GetString("Backward");
        leftText.text = PlayerPrefs.GetString("Left");
        rightText.text = PlayerPrefs.GetString("Right");
        jumpText.text = PlayerPrefs.GetString("Jump");
        crouchText.text = PlayerPrefs.GetString("Crouch");
        sprintText.text = PlayerPrefs.GetString("Sprint");
        interactText.text = PlayerPrefs.GetString("Interact");
    }
    public void Default()
    {
        volumeSlider.value = 1;
        brightnessScrollbar.value = 1;
        PlayerPrefs.SetFloat("Volume", 1);
        PlayerPrefs.SetFloat("Brightness", 1);

    }
    public bool TogglePause()
    {
        if (pause)
        {
            if (!showOptions)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                menu.SetActive(false);
                pause = false;
            }
            else
            {
                showOptions = false;
                options.SetActive(false);
                menu.SetActive(true);
            }
            return false;
        }
        else
        {
            Time.timeScale = 0;
            pause = true;
            return true;
        }

    }

    public void Mute()
    {
        ismute = !ismute;
        AudioListener.volume = ismute ? 0 : 1;
    }

    #region Key Press Event
    void OnGUI()
    {
        Event e = Event.current;
        //if forward is set to none
        if (forward == KeyCode.None)
        {
            //if an even is triggered by a key press
            if (e.isKey)
            {
                Debug.Log("Key Pressed; " + e.keyCode);
                //if that key is not the same as any other keys
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    //set forward to the event key that was pressed
                    forward = e.keyCode;
                    //set the holding key to none
                    holdingKey = KeyCode.None;
                    //set the GUI to the new Key
                    forwardText.text = forward.ToString();
                }
                //otherwise if it is the same as another key
                else
                {
                    //set the forward key back to the previous key
                    forward = holdingKey;
                    //set the holding key to none
                    holdingKey = KeyCode.None;
                    //set the GUI to the previous key
                    forwardText.text = forward.ToString();
                }
            }
        }

        if (backward == KeyCode.None)
        {
            //if an even is triggered by a key press
            if (e.isKey)
            {
                Debug.Log("Key Pressed; " + e.keyCode);
                //if that key is not the same as any other keys
                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    //set forward to the event key that was pressed
                    backward = e.keyCode;
                    //set the holding key to none
                    holdingKey = KeyCode.None;
                    //set the GUI to the new Key
                    backwardText.text = backward.ToString();
                }
                //otherwise if it is the same as another key
                else
                {
                    //set the forward key back to the previous key
                    backward = holdingKey;
                    //set the holding key to none
                    holdingKey = KeyCode.None;
                    //set the GUI to the previous key
                    backwardText.text = backward.ToString();
                }
            }
        }

        if (left == KeyCode.None)
        {
            if (e.isKey)
            {
                Debug.Log("Key Pressed; " + e.keyCode);
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    left = e.keyCode;
                    holdingKey = KeyCode.None;
                    leftText.text = left.ToString();
                }
                else
                {
                    left = holdingKey;
                    holdingKey = KeyCode.None;
                    leftText.text = left.ToString();
                }
            }
        }

        if (right == KeyCode.None)
        {
            if (e.isKey)
            {
                Debug.Log("Key Pressed; " + e.keyCode);
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    right = e.keyCode;
                    holdingKey = KeyCode.None;
                    rightText.text = right.ToString();
                }
                else
                {
                    right = holdingKey;
                    holdingKey = KeyCode.None;
                    rightText.text = right.ToString();
                }
            }
        }

        if (jump == KeyCode.None)
        {
            if (e.isKey)
            {
                Debug.Log("Key Pressed; " + e.keyCode);
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    jump = e.keyCode;
                    holdingKey = KeyCode.None;
                    jumpText.text = jump.ToString();
                }
                else
                {
                    jump = holdingKey;
                    holdingKey = KeyCode.None;
                    jumpText.text = jump.ToString();
                }
            }
        }

        if (crouch == KeyCode.None)
        {
            if (e.isKey)
            {
                Debug.Log("Key Pressed; " + e.keyCode);
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == interact))
                {
                    crouch = e.keyCode;
                    holdingKey = KeyCode.None;
                    crouchText.text = crouch.ToString();
                }
                else
                {
                    crouch = holdingKey;
                    holdingKey = KeyCode.None;
                    crouchText.text = crouch.ToString();
                }
            }
        }

        if (sprint == KeyCode.None)
        {
            if (e.isKey)
            {
                Debug.Log("Key Pressed; " + e.keyCode);
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == interact))
                {
                    sprint = e.keyCode;
                    holdingKey = KeyCode.None;
                    sprintText.text = sprint.ToString();
                }
                else
                {
                    sprint = holdingKey;
                    holdingKey = KeyCode.None;
                    sprintText.text = sprint.ToString();
                }
            }
        }

        if (interact == KeyCode.None)
        {
            if (e.isKey)
            {
                Debug.Log("Key Pressed; " + e.keyCode);
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint))
                {
                    interact = e.keyCode;
                    holdingKey = KeyCode.None;
                    interactText.text = interact.ToString();
                }
                else
                {
                    interact = holdingKey;
                    holdingKey = KeyCode.None;
                    interactText.text = interact.ToString();
                }
            }
        }
    }
    #endregion

    #region Controls
    public void Forward()
    {
        //if none of the other keys are blank
        //then we can edit this key
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set out holding key to the key of this button
            holdingKey = forward;
            //set this button to non allowing only this to be editable
            forward = KeyCode.None;
            //Set the text to none
            forwardText.text = forward.ToString();
        }
    }

    public void Backward()
    {
        //if none of the other keys are blank
        //then we can edit this key
        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set out holding key to the key of this button
            holdingKey = backward;
            //set this button to non allowing only this to be editable
            backward = KeyCode.None;
            //Set the text to none
            backwardText.text = backward.ToString();
        }
    }

    public void Left()
    {
        //if none of the other keys are blank
        //then we can edit this key
        if (!(backward == KeyCode.None || forward == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set out holding key to the key of this button
            holdingKey = left;
            //set this button to non allowing only this to be editable
            left = KeyCode.None;
            //Set the text to none
            leftText.text = left.ToString();
        }
    }

    public void Right()
    {
        //if none of the other keys are blank
        //then we can edit this key
        if (!(backward == KeyCode.None || forward == KeyCode.None || left == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set out holding key to the key of this button
            holdingKey = right;
            //set this button to non allowing only this to be editable
            right = KeyCode.None;
            //Set the text to none
            rightText.text = right.ToString();
        }
    }

    public void Jump()
    {
        //if none of the other keys are blank
        //then we can edit this key
        if (!(backward == KeyCode.None || forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set out holding key to the key of this button
            holdingKey = jump;
            //set this button to non allowing only this to be editable
            jump = KeyCode.None;
            //Set the text to none
            jumpText.text = jump.ToString();
        }
    }

    public void Crouch()
    {
        //if none of the other keys are blank
        //then we can edit this key
        if (!(backward == KeyCode.None || forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set out holding key to the key of this button
            holdingKey = crouch;
            //set this button to non allowing only this to be editable
            crouch = KeyCode.None;
            //Set the text to none
            crouchText.text = crouch.ToString();
        }
    }

    public void Sprint()
    {
        //if none of the other keys are blank
        //then we can edit this key
        if (!(backward == KeyCode.None || forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || interact == KeyCode.None))
        {
            //set out holding key to the key of this button
            holdingKey = sprint;
            //set this button to non allowing only this to be editable
            sprint = KeyCode.None;
            //Set the text to none
            sprintText.text = sprint.ToString();
        }
    }

    public void Interact()
    {
        //if none of the other keys are blank
        //then we can edit this key
        if (!(backward == KeyCode.None || forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None))
        {
            //set out holding key to the key of this button
            holdingKey = interact;
            //set this button to non allowing only this to be editable
            interact = KeyCode.None;
            //Set the text to none
            interactText.text = interact.ToString();
        }
    }

    #endregion

    #region FullScreen Toggle and Resolution
    public void FullScreenToggle()
    {
        fullScreenToggle = !fullScreenToggle;
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Setsolution()
    {
        index = resolutionDropdown.value;
        Screen.SetResolution(resX[index], resY[index], fullScreenToggle);
    }

    /* RESOLUTIONS
    3840 * 2160 
    2560 * 1440
    1920 * 1080
    1600 * 900
    1280 * 720
    1024 * 576
    640 * 480
    Screen.SetResolution(x,y,fullscreen(bool));
    */
    #endregion

}

/* Homework 20170818
Save, Load and set up all option menu data between both scenes
- volume
- mute
- brightness
- keybinding
- fullscreen
- relolutions

Pause Menu
- Toggle on an off correctly
- Cursor visibility and lock toggling
- Return button returns to game
    
All buttons on both scenes linking correctly

Code comments

    Bonus Tasks
    - Carry over music from game scene
    - make sure music doesn't duplicate
*/
