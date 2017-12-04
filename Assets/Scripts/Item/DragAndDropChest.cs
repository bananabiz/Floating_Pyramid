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
    public List<Item> chestInv = new List<Item>();
    public bool showChest;
    public DragAndDropInventory playerInv;
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
        if (size == ChestSize.Small)
        {
            slotX = 3;
            slotY = 2;
            for (int y = 0; y < slotY; y++)
            {
                for (int x = 0; x < slotX; x++)
                {

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

        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<DragAndDropInventory>(); 
	}
	

	void Update ()
    {
		
	}
}
