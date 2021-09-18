using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public string characterName;
    public int playerLevel = 1;
    public int currentEXP;
    public int currentMoney = 10;
    public int[] EXPToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;
    public int currentHP;
    public int MaxHP;

    public int currentMP;
    public int MaxMP;
    public int[] MPLevelBonus;

    public int attackPower;
    public int defense;
    public int speed;

    public int attackTrack;
    public int defenseTrack;
    public int speedTrack;

    public int weaponPower;
    public int armorDefense;
    public string equippedWeapon;
    public string equippedArmor;
    public Sprite characterImage;

    // Start is called before the first frame update
    void Start()
    {
        EXPToNextLevel = new int[maxLevel];
        EXPToNextLevel[1] = baseEXP;

        for(int i = 2; i < EXPToNextLevel.Length; i++)
        {
            EXPToNextLevel[i] = Mathf.FloorToInt(EXPToNextLevel[i - 1] * EXPToNextLevel[i - 1] + (2 * (EXPToNextLevel[i - 1] + 1)) * (2 * (EXPToNextLevel[i - 1] + 1)));
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void AddEXP(int expToAdd)
    {
        currentEXP += expToAdd;
        if (playerLevel < maxLevel)
        {
            if (currentEXP > EXPToNextLevel[playerLevel])
            {
                currentEXP -= EXPToNextLevel[playerLevel];

                playerLevel++;


                //Add Strength or defense
               if(attackTrack > defenseTrack && attackTrack > speedTrack)
                {
                    if (attackTrack - 2 * defenseTrack <= 0 && attackTrack - 2 * speedTrack <= 0)
                    {
                        attackPower += 3;
                    }
                    else
                    {
                        attackPower += 1;
                    }
                }

                if (defenseTrack > attackTrack && defenseTrack > speedTrack)
                {
                    if (defenseTrack - 2 * attackTrack <= 0 && defenseTrack - 2 * speedTrack <= 0)
                    {
                        defense += 3;
                    }
                    else
                    {
                        defense += 1;
                    }
                }

                if (speedTrack > attackTrack && speedTrack > defenseTrack)
                {
                    if (speedTrack - 2 * attackTrack <= 0 && speedTrack - 2 * defenseTrack <= 0)
                    {
                        speed += 3;
                    }
                    else
                    {
                        speed += 1;
                    }
                }



                MaxHP = Mathf.FloorToInt(MaxHP + 1.92f);
                currentHP = MaxHP;

                if (playerLevel > 10)
                {
                    MaxMP++;
                    MaxMP += MPLevelBonus[playerLevel];
                    currentMP = MaxMP;
                }
                else
                {
                    currentMP = MaxMP;
                }

                attackTrack = 0;
                defenseTrack = 0;
                speedTrack = 0;
            }
        }

        if(playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }



    }

    //Money Scripts

    public void AddMoney(int moneyToAdd) //Add money (duh)
    {
        currentMoney += moneyToAdd;
        Debug.Log(moneyToAdd + "$ added");
    }

    public void SubtractMoney(int moneyToSubtract) //subtract money (for cases which =! a shop buy. Like losing battle in pokemon)
    {
        currentMoney -= moneyToSubtract;
        Debug.Log(moneyToSubtract + "$ subtracted");
    }

    public void ShopBuy(int price, string item) //shop purchase
    {
        if (currentMoney >= price)
        {
            SubtractMoney(price);
            //Add Item To Inventory
            //CAN SOMEONE  EXPLAIN THE ITEM SYSTEM!?!?!!??!?

            Debug.Log("Purchase Successful. " + item + "bought for " + price + "$");
        }
        else if (currentMoney < price)
        {
            Debug.Log("Purchase Failed, not enough $.");
        }
        else
        {
           Debug.Log("Purchase Failed. Something went wrong");
        }
    }

}
