using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnTime = 0;
    [SerializeField]
    private Transform[] wayPoint;
    private List<Enemy> enemyList; //현재 맵에 존재하는 모든 적의 정보

    public List<Enemy> EnemyList => enemyList;
    private void Awake()
    {
        enemyList = new List<Enemy>();

        StartCoroutine("SpawnEnemy");

    }
    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject clone = Instantiate(enemyPrefab);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoint);
            enemyList.Add(enemy);

            yield return new WaitForSeconds(spawnTime);

        }
    }

    public void DestroyEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        Destroy(enemy.gameObject);
    }
}
