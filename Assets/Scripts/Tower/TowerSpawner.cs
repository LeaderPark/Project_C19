using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


public class TowerSpawner : MonoBehaviour

{
    [SerializeField]
    private TowerTemplate[] towerTemplate;
    //[SerializeField]
    //private GameObject towerPreFab;
    //[SerializeField]
    //private int towerBuildGold = 50;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private SystemTextViewer systemTextViewer;
    private bool isOnTowerButton = false;
    private GameObject followTowerClone = null;
    private int towerType;

    public void ReadyToSpawnTower(int type)
    {
        towerType = type;

        if(isOnTowerButton == true)
        {
            return;
        }
        if(towerTemplate[towerType].weapon[0].cost > playerGold.CurrentGold)
        {
            systemTextViewer.PrintText(SystemType.Money);
            return;
        }

        isOnTowerButton = true;

        followTowerClone = Instantiate(towerTemplate[towerType].followTowerPrefab);

        StartCoroutine("OnTowerCancelSystem");
    }

    public void SpwanTower(Transform tileTransform)
    {


        if (isOnTowerButton == false)
        {
            return;
        }
        //타워 건설 가능 여부 확인
        //1. 현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설X
        /*if ( towerTemplate.weapon[0].cost > playerGold.CurrentGold)
        {
            systemTextViewer.PrintText(SystemType.Money);
            return;
        
        }*/

        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true)
        {
            systemTextViewer.PrintText(SystemType.Build);
            return;
        }

        isOnTowerButton = false;

        // 타워가 건설되어 있음으로 설정
        tile.IsBuildTower = true;

        playerGold.CurrentGold -= towerTemplate[towerType].weapon[0].cost;
        // 선택한 타일의 위치에 타워 건설
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplate[towerType].towerPrefab, position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Setup(this, enemySpawner, playerGold);

        Destroy(followTowerClone);

        StopCoroutine("OnTowerCancelSystem");

        
        
    }

    private IEnumerator OnTowerCancelSystem()
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1))
            {
                isOnTowerButton = false;

                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }

    
}
//File: TowerSpawner.cs
//Desc
//    :타워 생성 제어

//Functions
//    :SpwanTower() - 매개변수의 위치에 타워 생성

