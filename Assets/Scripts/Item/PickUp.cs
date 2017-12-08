using UnityEngine;
using System.Collections;

[AddComponentMenu("Character Set Up / Interact")]
public class PickUp : MonoBehaviour 
{
    [Header("Player and Camera connection")]
    //create two gameobject variables one called player and the other mainCam
    public GameObject player;
    public GameObject mainCam;
    public GameObject chestBox;
    private DragAndDropChest dragChest;
    private CharacterHandler playerCH;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //connect player to the player variable via tag
        player = GameObject.FindGameObjectWithTag("Player");
        //connect Camera to the mainCam variable via tag
        mainCam = this.gameObject;

        playerCH = this.GetComponent<CharacterHandler>();
        chestBox = GameObject.FindGameObjectWithTag("ChestBox");
        dragChest = chestBox.GetComponent<DragAndDropChest>();
    }

    void Update()
    {
        //if our interact key is pressed
        if (Input.GetButtonDown("Fire2"))
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
                    //restore health once pick up the item
                    playerCH.curHealth += 20;
                    if (playerCH.curHealth > playerCH.maxHealth)
                    {
                        playerCH.curHealth = playerCH.maxHealth;
                    }
                    Destroy(hitInfo.transform.gameObject);
                    //Debug that we hit an Item
                    Debug.Log("Hit an Item");
                }
                #endregion

                #region ChestBox
                //and if hits info is tagged ChestBox
                if (hitInfo.collider.CompareTag("ChestBox"))
                {
                    //activate a window that shows what's inside ChestBox
                    //dragChest.showChest = true;
                    dragChest.ToggleInv();
                    //Cursor.lockState = CursorLockMode.None;
                    //Cursor.visible = true;
                    Debug.Log("Hit a ChestBox");
                }
                #endregion
            }
        }
    }
}






