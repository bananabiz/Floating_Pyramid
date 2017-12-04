using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//Interact
[AddComponentMenu("Character Set Up / Interact")]
public class PickUp : MonoBehaviour 
{
    #region Variables
    //We are setting up these variable and the tags in update for week 3,4 and 5

    [Header("Player and Camera connection")]
    //create two gameobject variables one called player and the other mainCam
    public GameObject player;
    public GameObject mainCam;
    #endregion

    #region Start
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //connect our player to the player variable via tag
        player = GameObject.FindGameObjectWithTag("Player");
        //connect our Camera to the mainCam variable via tag
        mainCam = this.gameObject; 
    }
    #endregion

    #region Update
    void Update()
    {
        //if our interact key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            //create a ray
            Ray interact;
            //this ray is shooting out from the main cameras screen point center of screen
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info
            RaycastHit hitInfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interact, out hitInfo, 10))
            {
                #region NPC tag
                //and if hits info is tagged NPC
                if (hitInfo.collider.CompareTag("NPC"))
                {
                    //Debug that we hit a NPC
                    Debug.Log("Hit the NPC");
                    Dialogue dlg = hitInfo.transform.GetComponent<Dialogue>();
                    if (dlg != null)
                    {
                        dlg.showDlg = true;
                        player.GetComponent<MouseLook>().enabled = false;
                        player.GetComponent<Movement>().enabled = false;
                        mainCam.GetComponent<MouseLook>().enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
                #endregion

                #region Item
                //and if hits info is tagged Item
                if (hitInfo.collider.CompareTag("Item"))
                {
                    //Debug that we hit an Item
                    Debug.Log("Hit an Item");
                }
                #endregion
            }
        }
    }
	#endregion
}






