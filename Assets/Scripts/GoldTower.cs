using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTower : MonoBehaviour
{
    private TowerWeapon towerWeapon;

    
    private WeaponType weaponType;
    public WeaponType WeaponType => weaponType;



    


    private void Awake()
    {

        

        weaponType = WeaponType.Gold;
        
        StartCoroutine(MakeGold());

        GameObject.Find("PlayerStats");



    }
    




    IEnumerator MakeGold()
    {



        if (weaponType == WeaponType.Gold)
        {
            while (true)
            {
                PlayerGold playerGold = GameObject.Find("PlayerStats").GetComponent<PlayerGold>();

                playerGold.CurrentGold++;

                Debug.Log("1원씩 증가");

                yield return new WaitForSeconds(1);
            }
        }
       

    }
        





}
