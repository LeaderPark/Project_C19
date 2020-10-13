using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDestroyType { kill = 0, Arrive }
public class Enemy : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currnetIndex = 0;
    private Movement2D movement2D;
    private EnemySpawner enemySpawner;

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currnetIndex].position;


        StartCoroutine("OnMove");

    }
    private IEnumerator OnMove()
    {
        

        while(true)
        {
            transform.Rotate(Vector3.forward * 10);

            if(Vector3.Distance(transform.position, wayPoints[currnetIndex].position) < 0.02f * movement2D.MoveSpeed )
            {
                NextMoveTo();
            }

            yield return null;

        }

    }

    private void NextMoveTo()
    {
        if(currnetIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currnetIndex].position;
            
            
            currnetIndex ++;
            Vector3 direction = (wayPoints[currnetIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            //Destroy(gameObject);
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        enemySpawner.DestroyEnemy(type, this);
    }
    

}

