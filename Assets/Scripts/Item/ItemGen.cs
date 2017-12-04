using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script no need to attached to anything, just sit in the files and get references
public class ItemGen : MonoBehaviour
{
    public static Item CreateItem(int itemID)
    {
        Item temp = new Item();

        #region Variables
        string name = "";
        int value = 0;
        string description = "";
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Food;
        #endregion

        #region Switch Item Data
        switch (itemID)
        {
            #region Food 0-99
            case 0:
                name = "Apple";
                value = 5;
                description = "Munchies and Crunchies";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;

            case 1:
                name = "Cheese Wheel";
                value = 7;
                description = "Golden Goodness";
                icon = "Cheese Wheel";
                mesh = "Cheese Wheel";
                type = ItemType.Food;
                break;

            case 2:
                name = "Meat";
                value = 10;
                description = "Retore half quarter of HP";
                icon = "Meat";
                mesh = "Meat";
                type = ItemType.Food;
                break;

            // repeat to case 99 (the last item)
            #endregion

            #region Weapon 100-199
            case 100:
                name = "Axe";
                value = 8;
                description = "Iron Axe";
                icon = "Axe";
                mesh = "Axe";
                type = ItemType.Weapon;
                break;

            case 101:
                name = "Bow";
                value = 5;
                description = "Bamboo Bow";
                icon = "Bow";
                mesh = "Bow";
                type = ItemType.Weapon;
                break;

            case 102:
                name = "Sword";
                value = 10;
                description = "Iron Sword";
                icon = "Sword";
                mesh = "Sword";
                type = ItemType.Weapon;
                break;

            #endregion

            #region Apparel 200-299
            case 200:
                name = "Armor";
                value = 10;
                description = "Copper Armor";
                icon = "Armor";
                mesh = "Armor";
                type = ItemType.Apparel;
                break;

            case 201:
                name = "Cloak";
                value = 7;
                description = "Leather Cloak";
                icon = "Cloak";
                mesh = "Cloak";
                type = ItemType.Apparel;
                break;

            case 202:
                name = "Pants";
                value = 6;
                description = "Leather Pants";
                icon = "Pants";
                mesh = "Pants";
                type = ItemType.Apparel;
                break;

            case 203:
                name = "Helmet";
                value = 6;
                description = "Iron Helmet";
                icon = "Helmet";
                mesh = "Helmet";
                type = ItemType.Apparel;
                break;

            case 204:
                name = "Bag";
                value = 5;
                description = "Leather Bag";
                icon = "Bag";
                mesh = "Bag";
                type = ItemType.Apparel;
                break;

            case 205:
                name = "Boots";
                value = 5;
                description = "Leather Boots";
                icon = "Boots";
                mesh = "Boots";
                type = ItemType.Apparel;
                break;

            case 206:
                name = "Gloves";
                value = 5;
                description = "Leather Gloves";
                icon = "Gloves";
                mesh = "Gloves";
                type = ItemType.Apparel;
                break;

            case 207:
                name = "Bracers";
                value = 5;
                description = "Leather Bracers";
                icon = "Bracers";
                mesh = "Bracers";
                type = ItemType.Apparel;
                break;

            case 208:
                name = "Belt";
                value = 5;
                description = "Leather Belt";
                icon = "Belt";
                mesh = "Belt";
                type = ItemType.Apparel;
                break;
                
            case 209:
                name = "Shoulders";
                value = 5;
                description = "Leather Shoulders";
                icon = "Shoulders";
                mesh = "Shoulders";
                type = ItemType.Apparel;
                break;

            case 210:
                name = "Shield";
                value = 5;
                description = "Wooden Shield";
                icon = "Shield";
                mesh = "Shield";
                type = ItemType.Apparel;
                break;

            #endregion

            #region Crafting 300-399
            case 300:
                name = "Necklace";
                value = 10;
                description = "Blue Gem Necklace";
                icon = "Necklace";
                mesh = "Necklace";
                type = ItemType.Crafting;
                break;

            case 301:
                name = "Ring";
                value = 10;
                description = "Golden Ring";
                icon = "Ring";
                mesh = "Ring";
                type = ItemType.Crafting;
                break;

            #endregion

            #region Quest 400-499
            #endregion

            #region Money 500-599
            case 500:
                name = "Coins";
                value = 1;
                description = "Silver Coins";
                icon = "Coins";
                mesh = "Coins";
                type = ItemType.Money;
                break;

            case 501:
                name = "Gem";
                value = 100;
                description = "Blue Gem";
                icon = "Gem";
                mesh = "Gem";
                type = ItemType.Money;
                break;

            case 502:
                name = "Ingots";
                value = 20;
                description = "Silver Ingots";
                icon = "Ingots";
                mesh = "Ingots";
                type = ItemType.Money;
                break;

            #endregion

            #region Ingredients 600-699
            #endregion

            #region Potions 700-799
            case 700:
                name = "HP";
                value = 30;
                description = "Restore half HP";
                icon = "HP";
                mesh = "HP";
                type = ItemType.Potions;
                break;

            case 701:
                name = "MP";
                value = 20;
                description = "Restore half MP";
                icon = "MP";
                mesh = "MP";
                type = ItemType.Potions;
                break;

            #endregion

            #region Scrolls 800-899
            case 800:
                name = "Scroll";
                value = 5;
                description = "Character Scroll";
                icon = "Scroll";
                mesh = "Scroll";
                type = ItemType.Scrolls;
                break;

            case 801:
                name = "Book";
                value = 15;
                description = "Story Book";
                icon = "Book";
                mesh = "Book";
                type = ItemType.Scrolls;
                break;
                
            #endregion

            // very important, set default in case something wrong
            default:
                itemID = 0;
                name = "Apple";
                value = 5;
                description = "Munchies and Crunchies";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;
        }
        #endregion

        #region Temp Connect
        temp.ID = itemID;
        temp.Name = name;
        temp.Value = value;
        temp.Description = description;
        temp.Icon = Resources.Load("Icons/" + icon) as Texture2D;
        temp.Mesh = mesh;
        temp.Type = type;
        #endregion

        return temp;
    }

}
