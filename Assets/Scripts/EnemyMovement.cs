using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyMovement : MonoBehaviour {



    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.points[wavepointIndex];
    }

    private void FixedUpdate()
    {
        if (GameMap.instance.blocked == false)
        {
            /*if(Waypoints.points[wavepointIndex] && target != Waypoints.points[wavepointIndex] )
            {
                if (target == Waypoints.points[wavepointIndex + 1])
                    wavepointIndex++;
                else
                    target = Waypoints.points[wavepointIndex];
            }*/
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.1f)
            {
                GetNextWaypoint();
            }

            enemy.speed = enemy.startSpeed;
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            Wave_Spawner.instance.enemyCount--;
            PlayerStats.Lives--;
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
