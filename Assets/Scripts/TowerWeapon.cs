using System.Collections;
using UnityEngine;


public enum WeaponType { Cannon = 0, Slow, Gold, }
public enum WeaponState { SearchTarget = 0, TryAttackToCannon, }

public class TowerWeapon : MonoBehaviour
{

    [Header("Commons")]
    [SerializeField]
    private TowerTemplate towerTemplate;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    public WeaponType weaponType;

    [Header("Cannon")]
    [SerializeField]
    private GameObject projectilePreFab;

    private SpriteRenderer spriteRenderer;
    private PlayerGold playerGold;
    private TowerSpawner towerSpawner;
    private int level = 0;
    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;
    private TowerWeapon weapon;

    

    public Sprite TowerSprite => towerTemplate.weapon[level].sprite;
    public float Damage => towerTemplate.weapon[level].damage;

    public float Rate => towerTemplate.weapon[level].rate;

    public float Range => towerTemplate.weapon[level].range;
    
    public int Level => level + 1;
    public int MaxLevel => towerTemplate.weapon.Length;
    public float Slow => towerTemplate.weapon[level].slow;
    
    public WeaponType WeaponType => weaponType;

    
    
     

    
    public void Setup(TowerSpawner towerSpawner, EnemySpawner enemySpawner, PlayerGold playerGold)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.towerSpawner = towerSpawner;
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;

        if (weaponType == WeaponType.Cannon)
        {
            ChangeState(WeaponState.SearchTarget);
        }
    }

    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());

        weaponState = newState;

        StartCoroutine(weaponState.ToString());
    }
    private void Update()
    {
        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }
    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }
    private Transform FindClosestAttackTarget()
    {
        float closestDistSqr = Mathf.Infinity;

        for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
        {
            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
            if (distance <= towerTemplate.weapon[level].range && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                attackTarget = enemySpawner.EnemyList[i].transform;

            }

            
        }
        return attackTarget;
    }

    /*private bool IsPossibleToAttackTarget()
    {
        if (attackTarget == null)
        {

            return false;

        }
        float distance = Vector3.Distance(attackTarget.position, transform.position);

        if (distance > towerTemplate.weapon[level].range)
        {
            attackTarget = null;
            return false;
        }
        return false;
    }*/

    private IEnumerator SearchTarget()
    {

        while (true)
        {


            float closestDistSqr = Mathf.Infinity;

            for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                if(distance <= towerTemplate.weapon[level].range && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;

                }


            }
            attackTarget = FindClosestAttackTarget();

            if (attackTarget != null)
            {
           
                
                    ChangeState(WeaponState.TryAttackToCannon);
                

            }

            yield return null;
        }
    }
    private IEnumerator TryAttackToCannon()
    {
        while (true)
        {
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;

            }

            float distance = Vector3.Distance(attackTarget.position, transform.position);

            if (distance > towerTemplate.weapon[level].range)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            
            

            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);

            SpawnProjectile();

        }
    }
    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePreFab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<ProjectTile>().Setup(attackTarget, towerTemplate.weapon[level].damage);
    }

    public bool Upgrade()
    {
        if(playerGold.CurrentGold < towerTemplate.weapon[level + 1].cost)
        {
            return false;
        }


        level++;

        spriteRenderer.sprite = towerTemplate.weapon[level].sprite;

        playerGold.CurrentGold -= towerTemplate.weapon[level].cost;

        return true;

    }

     
        
    
}
