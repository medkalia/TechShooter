using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoboRam : Enemy 
{
    public RoboRamMovementInfo roboRamMovementInfo = new RoboRamMovementInfo();
    public RoboRamStats roboRamStats = new RoboRamStats();

    protected override void Start()
    {
        base.Start();
        roboRamMovementInfo.isGrounded = enemyMovementInfo.isGrounded;
        roboRamMovementInfo.isFalling = enemyMovementInfo.isFalling;
        roboRamMovementInfo.FacingDirection = enemyMovementInfo.FacingDirection;
        //roboRamMovementInfo = (RoboRamMovementInfo)enemyMovementInfo;
        roboRamStats.currentHealth = enemyStats.currentHealth;
        roboRamStats.baseHealth = enemyStats.baseHealth;
        roboRamStats.contactDamage = enemyStats.contactDamage;
        roboRamStats.meleeDamage = enemyStats.meleeDamage;
        roboRamStats.rangedCD = enemyStats.rangedCD;
        roboRamStats.rangedRange = enemyStats.rangedRange;
        roboRamStats.meleeCD = enemyStats.meleeCD;
        roboRamStats.meleeRange = enemyStats.meleeRange;
        //roboRamStats = (RoboRamStats)enemyStats;

        ChangeState(new RoboRamIdleState());
    }

    protected void Update()
    {
        enemyMovementInfo.FacingDirection = roboRamMovementInfo.FacingDirection ;
        enemyStats.currentHealth = roboRamStats.currentHealth;
        currentState.Execute();
        if (target != null)
            roboRamMovementInfo.ramDestination = target.transform.position;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!enemyInfo.isDisabled)
        {
            base.OnTriggerEnter2D(other);
            if (currentState != null)
                currentState.OnTriggerEnter2D(other);
            if (other.tag == playerProjectileTag)
            {
                if (target == null)
                {
                    target = other.gameObject;
                    EnemySight sight = (gameObject.GetComponentInChildren<EnemySight>());
                    sight.GetComponent<BoxCollider2D>().size = new Vector2( Vector2.Distance(EnemyTransform.position,Player.Instance.transform.position) + sight.GetComponent<BoxCollider2D>().size.x, sight.GetComponent<BoxCollider2D>().size.y);
                    LookAtTarget();
                }
            }
        }
        
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }

    public override void LookAtTarget()
    {
        if (target != null)
        {
            Transform targetTransform = target.GetComponent<Transform>();
            float targetDirection = targetTransform.position.x - EnemyTransform.position.x;
            if (targetDirection < 0 && roboRamMovementInfo.FacingDirection == 1 || targetDirection > 0 && roboRamMovementInfo.FacingDirection == -1)
            {
                roboRamMovementInfo.FacingDirection *= -1;
            }
            EnemyTransform.localScale = new Vector3(Mathf.Abs(EnemyTransform.localScale.x) * roboRamMovementInfo.FacingDirection, EnemyTransform.localScale.y, EnemyTransform.localScale.z);
            EnemyAnim.SetTrigger(parameterTurnHash);
        }
    }

    public override void Move()
    {
        if (roboRamMovementInfo.isCharging && !roboRamMovementInfo.isForwarding)
        {
            EnemyRB.velocity = new Vector2(roboRamStats.chargingSpeed * roboRamMovementInfo.FacingDirection ,0);
        }else if (roboRamMovementInfo.isForwarding)
        {
            EnemyRB.velocity = new Vector2(roboRamStats.forwardingSpeed * roboRamMovementInfo.FacingDirection, 0);
        }else if (roboRamMovementInfo.isDecharging)
        {
            EnemyRB.velocity = new Vector2(roboRamStats.dechargingSpeed * roboRamMovementInfo.FacingDirection, 0);
        }
        
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }

    public override void ShootProjectile()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damageTaken)
    {
        
        roboRamStats.currentHealth -= damageTaken;
        enemyStats.currentHealth = roboRamStats.currentHealth;
        if (!IsDead)
        {

        }
        else
        {
            PlaySoundOnceOnManager(AudioParams.SoundPoolGroups.ROBORAM, AudioParams.SoundPools.DUYING);
            Die();
            EnemyAnim.SetTrigger(parameterDieHash);
        }
    }

    public override void RemoveTarget()
    {
        base.RemoveTarget();
        //ChangeState(new RoboRamIdleState());
    }

    //Animation events
    public void FinishTurning()
    {
        roboRamMovementInfo.isTurning = false;
    }
    public void FinishCharging()
    {
        roboRamMovementInfo.isCharging = false;
        roboRamMovementInfo.isForwarding = true;
    }
    public void FinishDecharging()
    {
        roboRamMovementInfo.isDecharging = false;
    }

    public override void PlaySoundAnimation(string soundtype)
    {
        throw new NotImplementedException();
    }

    public class RoboRamMovementInfo : EnemyMovementInfo
    {
        public Vector2 ramDestination;
        public bool isTurning = false;
        public bool isCharging = false;
        public bool isForwarding = false;
        public bool isDecharging = false;

        //public RoboRamMovementInfo() : base ()
        //{
        //    isTurning = false;
        //    isCharging = false;
        //    isForwarding = false;
        //    isDecharging = false;
        //}
    }

    public class RoboRamStats : EnemyStats
    {
        public float chargingSpeed = 3f ;
        public float forwardingSpeed = 8f;
        public float dechargingSpeed = 2f;
    }

    //trigger parameters
    [HideInInspector]
    public int parameterDechargeHash = Animator.StringToHash("decharge");
    [HideInInspector]
    public int parameterTurnHash = Animator.StringToHash("turn");
    [HideInInspector]
    public int parameterDamageHash = Animator.StringToHash("damage");
    [HideInInspector]
    public int parameterDieHash = Animator.StringToHash("die");
    //bool parameters
    [HideInInspector]
    public int parameterisFallingHash = Animator.StringToHash("isFalling");
}
