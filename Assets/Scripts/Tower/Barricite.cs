using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricite : MonoBehaviour
{

    [SerializeField]
    private float maxHp;

    private float currnetHP;


    private void Awake()
    {
        currnetHP = maxHp;

        

    }

    private void Update()
    {
        DestroyBarricite();
    }

    private void DestroyBarricite()
    {
        if(currnetHP == 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        

        if (collision.CompareTag("Enemy"))
        {
            currnetHP -= 1;

            Debug.Log("바리게이트 HP 1 감소");

            Destroy(collision.gameObject);
        }

    }



}
