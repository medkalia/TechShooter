using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedState : IEnemyState
{
    protected Enemy enemy;
    protected float rangedTimer;
    protected float rangedCD;
    public bool canRanged = true;

    public abstract void Enter(Enemy enemy);

    public abstract void Execute();

    public abstract void Exit();

    public abstract void OnTriggerEnter2D(Collider2D other);

    public abstract void OnTriggerStay2D(Collider2D other);

    public abstract void OnTriggerExit2D(Collider2D other);

    public abstract void Ranged();

}
