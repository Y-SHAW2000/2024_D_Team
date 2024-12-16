using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnemyState(Enemy enemy)
    {
        enemy.animeState = 0;
        //load path
        enemy.LoadPath(enemy.wayPointObj[WayPointManager.Instance.usingIndex[0]]);
    }

    public override void OnUpdate(Enemy enemy)
    {
        //patrol after idle animation is done
        if(!enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            enemy.animeState = 1;
            enemy.MoveToTarget();
        }
        

        //culculate distance between enemy and way point
        float distance = Vector3.Distance(enemy.transform.position, enemy.wayPoints[enemy.index]);
        //Debug.Log(distance);
        //near the point
        if(distance <= 0.5f)
        {
            enemy.animeState = 0;
            enemy.animator.Play("Idle");
            enemy.index++; //next point
            enemy.index = Mathf.Clamp(enemy.index, 0, enemy.wayPoints.Count-1);
            if(Vector3.Distance(enemy.transform.position, enemy.wayPoints[enemy.wayPoints.Count - 1]) <= 0.5f)
            {
                enemy.index = 0;
            }
        }

        if(enemy.attackList.Count > 0)
        {
            enemy.TransitionToState(enemy.attackState);
        }
    }
}
