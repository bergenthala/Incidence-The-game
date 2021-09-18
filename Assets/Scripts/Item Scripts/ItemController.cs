using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;   //Check for item type
    public bool isWeapon;
    public bool isArmor;
    public bool isCard;
    public bool isKeyItem;

    [Header("Item Info")]
    public string itemName;  //Item name and stuff
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStrength;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;

    public int armorStrength;

    public CharacterStats thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    { 

        if (isItem)
        {
            if (affectHP)
            {
                thePlayer.currentHP += amountToChange;
                if(thePlayer.currentHP >= thePlayer.MaxHP)
                {
                    thePlayer.currentHP = thePlayer.MaxHP;
                }
            }

            /*if (affectMP)
            {
                thePlayer.currentMP += amountToChange;
                if (thePlayer.currentMP > thePlayer.MaxMP)
                {
                    thePlayer.currentMP = thePlayer.MaxMP;
                }
            } */
        }


    }

}
