﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private Image imageScreen;
    [SerializeField]
    private float maxHP = 1000;
    private float currentHP;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    private void Awake()
    {
        currentHP = maxHP;
    }


    public void TakeDamege(int damage)
    {
        currentHP -= damage;


        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");


        if (currentHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    private IEnumerator HitAlphaAnimation()
    {
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;

        while(color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;


            yield return null;

        }
    }
}