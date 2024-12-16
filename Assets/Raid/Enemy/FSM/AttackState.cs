using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnemyState(Enemy enemy)
    {
        enemy.animeState = 2;
        enemy.targetPoint = enemy.attackList[0];
    }

    public override void OnUpdate(Enemy enemy)
    {
        // no target -> patrol
        if(enemy.attackList.Count == 0)
        {
            enemy.TransitionToState(enemy.patrolState);
        }

        //multiple targets -> find the closest one
        if(enemy.attackList.Count > 1)
        {
            for(int i = 0; i < enemy.attackList.Count; i++) 
            {
                if(Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].position.x) <
                   Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) 
                )
                {
                    enemy.targetPoint = enemy.attackList[i];
                }    
            }
        }

        //only one target -> find the first one in the list
        if(enemy.attackList.Count == 1)
        {
            enemy.targetPoint = enemy.attackList[0];
        }

        if(enemy.targetPoint.tag == "Player")
        {
            enemy.AttackAction();
            enemy.animator.SetBool("shouldChase", false);
        }
        AnimatorStateInfo info =  enemy.animator.GetCurrentAnimatorStateInfo(1);

        if (Vector3.Distance(enemy.targetPoint.position, this.transform.position) >= enemy.attackRange01 && Time.time < enemy.coolDown01)
        {
            enemy.animator.SetBool("shouldChase", true);
            enemy.MoveToTarget();
        }
        else if (Vector3.Distance(enemy.targetPoint.position, this.transform.position) >= enemy.attackRange02)
        {
            enemy.animator.SetBool("shouldChase", true);
            enemy.MoveToTarget();
        }
        else if (Vector3.Distance(enemy.targetPoint.position, this.transform.position) >= enemy.attackRange03) 
        {
            enemy.animator.SetBool("shouldChase", true);
            enemy.MoveToTarget();
        }
        // else
        // {
        //     enemy.animator.SetBool("shouldChase", true);
        //     enemy.MoveToTarget();
        // }




    }
}
