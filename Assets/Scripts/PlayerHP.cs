using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 20;
    private float currentHP;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;


    public void TakeDamege(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {

        }
    }
}