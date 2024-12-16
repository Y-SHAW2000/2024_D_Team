using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For expand basic states of enemy
/// </summary>
public abstract class EnemyBaseState : MonoBehaviour
{
    public abstract void EnemyState(Enemy enemy);
    public abstract void OnUpdate(Enemy enemy);

}
