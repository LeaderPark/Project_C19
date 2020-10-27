using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField]
    private int currenntGold = 100;

    public int CurrentGold
    {
        set => currenntGold = Mathf.Max(0, value);
        get => currenntGold;
    }
}
