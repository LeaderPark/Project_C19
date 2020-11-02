using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField]
    //private GameObject enemyPrefab;
    //[SerializeField]
    //private float spawnTime = 0;
    [SerializeField]
    private Transform[] wayPoint;
    [SerializeField]
    private PlayerHP playerHP;
    private List<Enemy> enemyList; //현재 맵에 존재하는 모든 적의 정보
    [SerializeField]
    private PlayerGold playerGold;
    private Wave currentWave;
    private int currentEnemyCount;

    public List<Enemy> EnemyList => enemyList;

    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEnemyCount;
    private void Awake()
    {
        enemyList = new List<Enemy>();

        //StartCoroutine("SpawnEnemy");

    }
    public void StartWave(Wave wave)
    {
        currentWave = wave;

        currentEnemyCount = currentWave.maxEnemyCount;

        StartCoroutine("SpawnEnemy");
    }




    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;

        while(spawnEnemyCount < currentWave.maxEnemyCount)
        {
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoint);
            enemyList.Add(enemy);

            spawnEnemyCount++;

            yield return new WaitForSeconds(currentWave.spawnTime);

        }
    }

    public void DestroyEnemy(EnemyDestroyType type,Enemy enemy, int gold)
    {
        if( type == EnemyDestroyType.Arrive)
        {
            playerHP.TakeDamege(1);
        }
        else if( type == EnemyDestroyType.kill)
        {
            playerGold.CurrentGold += gold;
        }

        currentEnemyCount --;

        enemyList.Remove(enemy);

        Destroy(enemy.gameObject);
    }
}
