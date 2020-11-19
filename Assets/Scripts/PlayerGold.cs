using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField]
    public int CurrentGold = 100;

    

    public int CurrenntGold
    {
        set => CurrentGold = Mathf.Max(0, value);
        get => CurrentGold;
    }
}
