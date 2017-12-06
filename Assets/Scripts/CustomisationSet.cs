using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomisationSet : MonoBehaviour
{
    #region Variables
    public GUIStyle buttonStyle;

    [Header("Texture List")]
    //Texture2D List for skin,hair, mouth, eyes
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();

    [Header("Index")]
    //index numbers for our current skin, hair, mouth, eyes textures
    public int skinIndex;
    public int hairIndex, mouthIndex, eyesIndex, armourIndex, clothesIndex;

    [Header("Renderer")]
    //renderer for our character mesh so we can reference a material list
    public Renderer character;

    [Header("Max Index")]
    //max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int skinMax;
    public int hairMax, mouthMax, eyesMax, armourMax, clothesMax;

    [Header("Character Name")]
    //name of our character that the user is making
    public string charName = "Adventurer";
    
    [Header("Character Class")]
    public int Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma;
    public int[] skillStats;
    public string[] skillName = new string[6] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
    public int skillPoints = 10;
    public string[] classSize;
    public string classString = "Choose Class";
    public string skillInfo;
    string currentClass;
    public bool showClass = false;
    public Vector2 scrollPosClass;
    public GUIStyle chaClass;
    public enum CharacterClass
    {
        None,
        Bard, //Charisma 
        Wizard, //Intelligence
        Paladin,// Constitution
        Ranger,//Dexterity
        Fighter,//Strength
        Monk //Wisdom
    }
    #endregion
    
    void Start()
    {
        //text for class drop down button
        classSize = new string[6];
        classSize[0] = CharacterClass.Bard.ToString();
        classSize[1] = CharacterClass.Wizard.ToString();
        classSize[2] = CharacterClass.Paladin.ToString();
        classSize[3] = CharacterClass.Ranger.ToString();
        classSize[4] = CharacterClass.Fighter.ToString();
        classSize[5] = CharacterClass.Monk.ToString();

        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/Skin_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the skin List
            skin.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of hair textures we need
        for (int i = 0; i < hairMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Hair_#
            Texture2D temp = Resources.Load("Character/Hair_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the hair List
            hair.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of mouth textures we need  
        for (int i = 0; i < mouthMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Mouth_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the mouth List
            mouth.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of eyes textures we need
        for (int i = 0; i < eyesMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Eyes_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the eyes List  
            eyes.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of armour textures we need
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Armour_" + i.ToString()) as Texture2D;
            armour.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of clothes textures we need
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Clothes_" + i.ToString()) as Texture2D;
            clothes.Add(temp);
        }
        #endregion
        //connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        #region do this after making the function SetTexture
        //SetTexture skin, hair, mouth, eyes, armour, clothes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Armour", 0);
        SetTexture("Clothes", 0);
        #endregion
    }
   
    //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing
    void SetTexture(string type, int dir)
    {
        //we need variables that exist only within this function
        //these are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        //inside a switch statement that is swapped by the string name of our material

        #region Switch Material
        switch (type)
        {
            //case skin
            case "Skin":
                //index is the same as our skin index
                index = skinIndex;
                //max is the same as our skin max
                max = skinMax;
                //textures is our skin list .ToArray()
                textures = skin.ToArray();
                //material index element number is 1
                matIndex = 1;
                //break
                break;
            //now repeat for each material 

            //hair is 2
            case "Hair":
                //index is the same as our index
                index = hairIndex;
                //max is the same as our max
                max = hairMax;
                //textures is our list .ToArray()
                textures = hair.ToArray();
                //material index element number is 2
                matIndex = 2;
                //break
                break;

            //mouth is 3
            case "Mouth":
                //index is the same as our index
                index = mouthIndex;
                //max is the same as our max
                max = mouthMax;
                //textures is our list .ToArray()
                textures = mouth.ToArray();
                //material index element number is 3
                matIndex = 3;
                //break
                break;

            //eyes are 4
            case "Eyes":
                //index is the same as our index
                index = eyesIndex;
                //max is the same as our max
                max = eyesMax;
                //textures is our list .ToArray()
                textures = eyes.ToArray();
                //material index element number is 4
                matIndex = 4;
                //break
                break;

            //armour is 5
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 5;
                break;

            //clothes is 6
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 6;
                break;
        }
        #endregion

        #region OutSide Switch
        //outside our switch statement
        //index plus equals our direction
        index += dir;
        //cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        //Material array is equal to our characters material list
        Material[] mat = character.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        //our characters materials are equal to the material array
        character.materials = mat;
        //create another switch that is goverened by the same string name of our material
        #endregion

        #region Set Material Switch
        switch (type)
        {
            //case skin
            case "Skin":
                //skin index equals our index
                skinIndex = index;
                //break
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Eyes":
                eyesIndex = index;
                break;

            case "Armour":
                armourIndex = index;
                break;

            case "Clothes":
                clothesIndex = index;
                break;
        }
        #endregion
       
    }

    void CharacterClassStats(string cClass)
    {
        currentClass = cClass;
        skillStats = new int[6]{Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma};

        /* skillInfo = cClass + "\nStrength: " + Strength.ToString() + "\nDexterity: " + Dexterity.ToString()
            + "\nConstitution: " + Constitution.ToString() + "\nIntelligence: " + Intelligence.ToString()
            + "\nWisdom: " + Wisdom.ToString() + "\nCharisma: " + Charisma.ToString(); */

        switch (currentClass)
        {
            case "Bard":
                Strength = 3;
                Dexterity = 5;
                Constitution = 4;
                Intelligence = 6;
                Wisdom = 7;
                Charisma = 8;
                break;

            case "Wizard":
                Strength = 4;
                Dexterity = 3;
                Constitution = 5;
                Intelligence = 8;
                Wisdom = 7;
                Charisma = 6;
                break;

            case "Paladin":
                Strength = 7;
                Dexterity = 6;
                Constitution = 8;
                Intelligence = 6;
                Wisdom = 5;
                Charisma = 5;
                break;

            case "Ranger":
                Strength = 5;
                Dexterity = 8;
                Constitution = 5;
                Intelligence = 5;
                Wisdom = 6;
                Charisma = 5;
                break;

            case "Fighter":
                Strength = 8;
                Dexterity = 6;
                Constitution = 7;
                Intelligence = 4;
                Wisdom = 4;
                Charisma = 3;
                break;

            case "Monk":
                Strength = 3;
                Dexterity = 5;
                Constitution = 4;
                Intelligence = 7;
                Wisdom = 8;
                Charisma = 5;
                break;

            default:
                Strength = 0;
                Dexterity = 0;
                Constitution = 0;
                Intelligence = 0;
                Wisdom = 0;
                Charisma = 0;
                break;
        }
        
    }

    //Save indexes to PlayerPrefs
    void Save()
    {
        //SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        //SetString CharacterName
        PlayerPrefs.SetString("CharacterName", charName);
        //Save string for character class
        PlayerPrefs.SetString("CharacterClass", currentClass);
        //Save skills stats
        PlayerPrefs.SetInt("Strength", Strength);
        PlayerPrefs.SetInt("Dexterity", Dexterity);
        PlayerPrefs.SetInt("Constitution", Constitution);
        PlayerPrefs.SetInt("Intelligence", Intelligence);
        PlayerPrefs.SetInt("Wisdom", Wisdom);
        PlayerPrefs.SetInt("Charisma", Charisma);
    }

    
    //Function for GUI elements
    void OnGUI()
    {
        //create the floats scrW and scrH that govern our 16:9 ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        //create an int that will help with shuffling your GUI elements under eachother
        int i = 0;

        #region Skin
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<", buttonStyle))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Skin", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Skin", buttonStyle);
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">", buttonStyle))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Skin", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion

        //set up same things for Hair, Mouth and Eyes
        #region Hair
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<", buttonStyle))
        {
            //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
            SetTexture("Hair", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence material Name
        GUI.Box(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Hair", buttonStyle);
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">", buttonStyle))
        {
            //when pressed the button will run SetTexture and grab the  Material and move the texture index in the direction  1
            SetTexture("Hair", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion

        #region Mouth
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<", buttonStyle))
        {
            SetTexture("Mouth", -1);
        }
        GUI.Box(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Mouth", buttonStyle);
        if (GUI.Button(new Rect(2.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">", buttonStyle))
        {
            SetTexture("Mouth", 1);
        }
        i++;
        #endregion

        #region Eyes
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<", buttonStyle))
        {
            SetTexture("Eyes", -1);
        }
        GUI.Box(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Eyes", buttonStyle);
        if (GUI.Button(new Rect(2.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">", buttonStyle))
        {
            SetTexture("Eyes", 1);
        }
        i++;
        #endregion

        #region Armour
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<", buttonStyle))
        {
            SetTexture("Armour", -1);
        }
        GUI.Box(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Armour", buttonStyle);
        if (GUI.Button(new Rect(2.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">", buttonStyle))
        {
            SetTexture("Armour", 1);
        }
        i++;
        #endregion

        #region Clothes
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<", buttonStyle))
        {
            SetTexture("Clothes", -1);
        }
        GUI.Box(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Clothes", buttonStyle);
        if (GUI.Button(new Rect(2.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">", buttonStyle))
        {
            SetTexture("Clothes", 1);
        }
        i++;
        #endregion

        #region Random Reset
        //create 2 buttons one Random and one Reset
        //Random will feed a random amount to the direction 
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Random", buttonStyle))
        {
            SetTexture("Skin", Random.Range(0, skinMax - 1));
            SetTexture("Hair", Random.Range(0, hairMax - 1));
            SetTexture("Mouth", Random.Range(0, mouthMax - 1));
            SetTexture("Eyes", Random.Range(0, eyesMax - 1));
            SetTexture("Armour", Random.Range(0, armourMax - 1));
            SetTexture("Clothes", Random.Range(0, clothesMax - 1));
        }

        //reset will set all to 0 both use SetTexture
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Reset", buttonStyle))
        {
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Armour", armourIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion

        #region Character Name and Save & Play
        //name of our character equals a GUI TextField that holds our character name and limit of characters
        charName = GUI.TextField(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 12, buttonStyle);
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        //GUI Button called Save and Play
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Save & Play", buttonStyle))
        {
            //this button will run the save function and also load into the game level
            Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        #endregion

        #region Dropdown Character Classes

        CharacterClassStats(classString);
        //display character skills
        for (int sSize = 0; sSize < skillName.Length; sSize++)
        {
            GUI.Box(new Rect(12f * scrW, 1f * scrH + sSize * (scrH * 0.5f), 1.75f * scrW, 0.5f * scrH), skillName[sSize] + ": " + skillStats[sSize].ToString(), chaClass);
        }
        
        #region add skill points on Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma
        
        if ((GUI.Button(new Rect(13.75f * scrW, 1f * scrH + 0 * (scrH * 0.5f), 0.5f * scrW, 0.5f * scrH), "+", chaClass)) && (Strength < 10) && (Strength != 0))
        {
            skillStats[0]++;
            skillPoints--;
            Debug.Log("Character changed to Bard. " + skillStats[0].ToString() + "; " + Strength.ToString() + "; " + skillPoints);
        }

        if ((GUI.Button(new Rect(13.75f * scrW, 1f * scrH + 1 * (scrH * 0.5f), 0.5f * scrW, 0.5f * scrH), "+", chaClass)) && (Dexterity < 10) && (Dexterity != 0))
        {
            skillStats[1]++;
            skillPoints--;
            Debug.Log("Character changed to Wizard. " + skillStats[0].ToString() + "; " + Strength.ToString() + "; " + skillPoints);
        }

        if ((GUI.Button(new Rect(13.75f * scrW, 1f * scrH + 2 * (scrH * 0.5f), 0.5f * scrW, 0.5f * scrH), "+", chaClass)) && (Constitution < 10) && (Constitution != 0))
        {
            skillStats[2]++;
            skillPoints--;
            Debug.Log("Character changed to Paladin. " + skillStats[0].ToString() + "; " + Strength.ToString() + "; " + skillPoints);
        }

        if ((GUI.Button(new Rect(13.75f * scrW, 1f * scrH + 3 * (scrH * 0.5f), 0.5f * scrW, 0.5f * scrH), "+", chaClass)) && (Intelligence < 10) && (Intelligence != 0))
        {
            skillStats[3]++;
            skillPoints--;
            Debug.Log("Character changed to Ranger. " + skillStats[0].ToString() + "; " + Strength.ToString() + "; " + skillPoints);
        }

        if ((GUI.Button(new Rect(13.75f * scrW, 1f * scrH + 4 * (scrH * 0.5f), 0.5f * scrW, 0.5f * scrH), "+", chaClass)) && (Wisdom < 10) && (Wisdom != 0))
        {
            skillStats[4]++;
            skillPoints--;
            Debug.Log("Character changed to Fighter. " + skillStats[0].ToString() + "; " + Strength.ToString() + "; " + skillPoints);
        }

        if ((GUI.Button(new Rect(13.75f * scrW, 1f * scrH + 5 * (scrH * 0.5f), 0.5f * scrW, 0.5f * scrH), "+", chaClass)) && (Charisma < 10) && (Charisma != 0))
        {
            skillStats[5]++;
            skillPoints--;
            Debug.Log("Character changed to Monk. " + skillStats[0].ToString() + "; " + Strength.ToString() + "; " + skillPoints);
        }
        #endregion

        //show skillpoints
        GUI.Box(new Rect(12f * scrW, 4.25f * scrH, 2.25f * scrW, 0.5f * scrH), skillPoints.ToString()+" Skill Points to add", chaClass);

        //dropdown button show character classes
        if (GUI.Button(new Rect(12f * scrW, 5 * scrH, 1.75f * scrW, 0.5f * scrH), classString, chaClass))
        {
            //toggle on and off dropdown
            showClass = !showClass;
        }
        if (showClass)
        {
            GUI.Box(new Rect(12f * scrW, 5 * scrH, 1.75f * scrW, 0.5f * scrH), "", chaClass);
            //open a scroll view
            scrollPosClass = GUI.BeginScrollView(new Rect(12f * scrW, 5.5f * scrH, 1.75f * scrW, 3 * scrH), scrollPosClass, new Rect(0, 0, 1.75f * scrW, 3f * scrH));

            for (int claSize = 0; claSize < classSize.Length; claSize++)
            {
                if (GUI.Button(new Rect(0f * scrW, 0 * scrH + claSize * (scrH * 0.5f), 1.75f * scrW, 0.5f * scrH), classSize[claSize], chaClass))
                {
                    //behaviour goes here
                    classString = classSize[claSize];

                    //turn off the drop down
                    showClass = false;

                }
            }
            //the end of scroll view
            GUI.EndScrollView();
            
        }
        
        #endregion
    }
}