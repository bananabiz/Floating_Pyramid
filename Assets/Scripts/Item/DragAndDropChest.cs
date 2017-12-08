using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropChest : MonoBehaviour
{

    public enum ChestSize
    {
        Small,
        Medium,
        Large
    }
    public int slotX, slotY;
    public ChestSize size;
    public List<Item> chestInventory = new List<Item>();
    public bool showChest = true;
    public DragAndDropInventory playerInv;
    public GUIStyle chestStyle;

    private Rect inventorySize;

    [Header("Dragging")]
    public bool dragging;
    public Item draggedItem;
    public int draggedFrom; //the slot we took the item from
    public GameObject droppedItem;
    [Header("Tool Tip")]
    public int toolTipItem;
    public bool showToolTip;
    private Rect toolTipRect;
    [Header("References and Locations")]
    public Movement playerMove;
    public MouseLook mainCam, playerCam;
    private float scrW, scrH;


    void Start ()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 8;
        
        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<DragAndDropInventory>();

        if (size == ChestSize.Small)
        {
            slotX = 3;
            slotY = 2;
            for (int y = 0; y < slotY; y++)
            {
                for (int x = 0; x < slotX; x++)
                {
                    inventorySize = new Rect(9 * scrW, 3 * scrH, 3 * scrW, 2f * scrH);
                }

            }
        }
        if (size == ChestSize.Medium)
        {
            slotX = 3;
            slotY = 3;

        }
        if (size == ChestSize.Large)
        {
            slotX = 4;
            slotY = 3;

        }

        inventorySize = ClampToScreen(GUI.Window(1, inventorySize, InventoryDrag, "My Inventory", chestStyle));
    }

    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }

    void InventoryDrag(int windowID)
    {
        GUI.Box(new Rect(9 * scrW, 3 * scrH, 6 * scrW, 0.5f * scrH), "Treasures", chestStyle);
        showToolTip = false;
        #region Nested For Loop
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(scrW * 0.125f + x * (scrW * 0.75f), scrH * 0.75f + y * (scrH * 0.65f), 0.75f * scrW, 0.65f * scrH);
                GUI.Box(slotLocation, "");
                #region Pickup Item
                if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !dragging && chestInventory[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {
                    draggedItem = chestInventory[i];//we pick up item
                    chestInventory[i] = new Item();//the slot item is now blank
                    dragging = true;//we are holding an item
                    draggedFrom = i;//this is so we know where the item came from
                    Debug.Log("Dragging: " + draggedItem.Name);
                }
                #endregion
                #region Swap Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && dragging && chestInventory[i].Name != null)
                {
                    Debug.Log("Swapping: " + draggedItem.Name + " :With: " + chestInventory[i].Name);

                    chestInventory[draggedFrom] = chestInventory[i];//our pick up slot now has our other item
                    chestInventory[i] = draggedItem;//the slot we are dropping the Item is now filled with our drag item
                    draggedItem = new Item();//the item we use to be dragging is empty
                    dragging = false;//we are not holding an item

                }
                #endregion
                #region Place Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && dragging && chestInventory[i].Name == null)
                {
                    Debug.Log("Place: " + draggedItem.Name + " :Into: " + i);

                    chestInventory[i] = draggedItem;//the slot we are dropping the Item is now filled with our drag item
                    draggedItem = new Item();//the item we use to be dragging is empty
                    dragging = false;//we are not holding an item

                }
                #endregion
                #region Return Item
                if (e.button == 0 && e.type == EventType.MouseUp && i == ((slotX * slotY) - 1) && dragging)
                {
                    Debug.Log("Return: " + draggedItem.Name + " :Into: " + draggedFrom);

                    chestInventory[draggedFrom] = draggedItem;//the slot we are dropping the Item is now filled with our drag item
                    draggedItem = new Item();//the item we use to be dragging is empty
                    dragging = false;//we are not holding an item

                }
                #endregion
                #region Draw Item Icon
                if (chestInventory[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, chestInventory[i].Icon);
                    #region Set ToolTip on Mouse Hover
                    if (slotLocation.Contains(e.mousePosition) && !dragging && showChest)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }
        #endregion
    }

    void Update ()
    {
        if (showChest)
        {
             ToggleInv();
           
        }

	}

    #region ToggleInventory and Controls
    public bool ToggleInv()
    {
        if (showChest)
        {
            showChest = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mainCam.enabled = true;
            playerCam.enabled = true;
            playerMove.enabled = true;
            return (false);
        }
        else
        {
            showChest = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainCam.enabled = false;
            playerCam.enabled = false;
            playerMove.enabled = false;
            return (true);
        }
    }
    #endregion

    void OnGUI()
    {
        Event e = Event.current;
        #region Draw Inventory if showInv is true
        if (showChest)
        {
            inventorySize = ClampToScreen(GUI.Window(1, inventorySize, InventoryDrag, "My Inventory", chestStyle));
        }
        #endregion
        #region Draw ToolTip
        if (showToolTip && showChest)
        {
            toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.01f, scrW * 2, scrH * 3);
            GUI.Window(15, toolTipRect, DrawToolTip, "");
        }
        #endregion
        #region Drop Item is not showInv and mouse is up
        if (e.button == 0 && e.type == EventType.MouseUp && dragging)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Incase inventory closes drop dragged item
        if (e.button == 0 && e.type == EventType.MouseUp && dragging && !showChest)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Draw Item on Mouse
        if (dragging)
        {
            if (draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scrW * 0.5f, scrH * 0.5f);
                GUI.Window(2, mouseLocation, DrawItem, "");
            }
        }
        #endregion
    }

    private string ToolTipText(int iD)
    {
        string toolTipText =
        "Name: " + chestInventory[iD].Name +
        "\nDescription: " + chestInventory[iD].Description +
        "\nType: " + chestInventory[iD].Type +
        "\nID: " + chestInventory[iD].ID;
        return toolTipText;
    }

    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scrW * 2, scrH * 3), ToolTipText(toolTipItem));
    }

    public void DropItem(int iD)
    {
        droppedItem = Resources.Load("Prefabs/" + ItemGen.CreateItem(iD).Mesh) as GameObject;
        Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.Euler(0, 0, 90));
        return;
    }
    
    void DrawItem(int windowID)
    {
        if (draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scrW * 0.5f, scrH * 0.5f), draggedItem.Icon);
        }
    }
}
