using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    [SerializeField]
    private WaveSystem waveSystem;
    [SerializeField]
    private EnemySpawner enemySpawner;
    private void Update()
    {
        gameClear();
    }

    private void gameClear()
    {

        if (waveSystem.CurrentWave == waveSystem.MaxWave)
        {

            
            if (enemySpawner.EnemyList.Count == 0)
            {
                SceneManager.LoadScene("GameClear");
            }
        }

    }








}
