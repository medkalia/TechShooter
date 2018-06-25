using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoboTurtle : Enemy {

    

    protected override void Start()
    {
        //**Initialisations
        base.Start();
        PlaySoundOnce(AudioParams.SoundPoolGroups.ROBOTURTLE, AudioParams.SoundPools.SPAWNING);
        ChangeState(new RoboTurtleIdleState());
        enemyMovementInfo.spriteOriginalDirection = -1;
        enemyMovementInfo.FacingDirection = -1;
    }

    void Update()
    {
        if (!enemyInfo.isDisabled)
        {
            enemyMovementInfo.isFalling = EnemyRB.velocity.y < -1.5f && !enemyMovementInfo.isGrounded;
            EnemyAnim.SetBool(parameterisFallingHash, enemyMovementInfo.isFalling);
            LookAtTarget();
            if (currentState != null)
                currentState.Execute();

            //Debug.Log(enemyMovementInfo.FacingDirection == 1 ? "Right" : "Left");
            //Debug.Log("Can move left = " + enemyMovementInfo.canMoveLeft);
        }
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
                    ContactPoint2D[] contacts = new ContactPoint2D[10];
                    Vector2 pointOfContact = other.transform.position;
                    Vector2 contactDirection = pointOfContact - (Vector2)transform.position;
                    enemyMovementInfo.FacingDirection = Mathf.Sign(contactDirection.x);
                    ChangeState(new RoboTurtlePatrolState());
                }
            }
        }
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (!enemyInfo.isDisabled)
        {
            currentState.OnTriggerStay2D(other);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            Die();
            EnemyAnim.SetTrigger(parameterDieHash);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!enemyInfo.isDisabled)
        {
            currentState.OnTriggerExit2D(other);
        }
    }

    public override void Move()
    {
        if ((enemyMovementInfo.FacingDirection == -1 && enemyMovementInfo.canMoveLeft) || (enemyMovementInfo.FacingDirection == 1 && enemyMovementInfo.canMoveRight))
        {
            EnemyAnim.SetFloat(parameterSpeedHash, 1);
            EnemyTransform.localScale = new Vector3(Mathf.Abs(EnemyTransform.localScale.x) * -enemyMovementInfo.FacingDirection, EnemyTransform.localScale.y, EnemyTransform.localScale.z);// we put - enemyMovementInfo.FacingDirection cause this enemy's sprite starts facing left 
            EnemyTransform.Translate(Vector2.right * enemyMovementInfo.FacingDirection * (enemyStats.movementSpeed * Time.deltaTime));
        }
    }

    public override void LookAtTarget()
    {
        if (target != null)
        {
            Transform targetTransform = target.GetComponent<Transform>();
            float targetDirection = targetTransform.position.x - EnemyTransform.position.x;
            if (InRangedRange && (targetDirection < 0 && enemyMovementInfo.FacingDirection == 1) || (targetDirection > 0 && enemyMovementInfo.FacingDirection == -1))
            {
                enemyMovementInfo.FacingDirection *= -1;
                EnemyTransform.localScale = new Vector3(Mathf.Abs(EnemyTransform.localScale.x) * -enemyMovementInfo.FacingDirection, EnemyTransform.localScale.y, EnemyTransform.localScale.z);// we put - enemyMovementInfo.FacingDirection cause this enemy's sprite starts facing left 
            }
        }
    }

    public override void Shoot()
    {
        EnemyAnim.SetFloat(parameterSpeedHash, 0);
        enemyInfo.isMeleeAttacking = false;
        enemyInfo.isRangedAttacking = true;
        EnemyAnim.SetTrigger(parameterRangedHash);
    }

    public override void ShootProjectile()
    {
        GameObject newShot = Instantiate(shot, shotSpawnTransform.position, shotSpawnTransform.rotation);
        newShot.transform.parent = shotSpawnTransform;
    }

    public override void Attack()
    {
        if (enemyMovementInfo.isGrounded)
        {
            EnemyAnim.SetFloat(parameterSpeedHash, 0);
            enemyInfo.isRangedAttacking = false;
            enemyInfo.isMeleeAttacking = true;
            EnemyAnim.SetTrigger(parameterMeleeHash);
            CalculateAndApplyJump();
        }        
    }

    private void CalculateAndApplyJump()
    {
        Vector3 p = target.transform.position;
        float gravity = Physics2D.gravity.magnitude;
        //select angle in radians
        float angle = 45f * Mathf.Deg2Rad;

        //positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPosition = new Vector3(EnemyTransform.position.x, 0, transform.position.z);

        //Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPosition);
        //Distance along the y axis between objects
        float yOffset = 0;// transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sign(angle), initialVelocity * Mathf.Cos(angle));

        //Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (p.x > EnemyTransform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        EnemyRB.velocity = finalVelocity;
        //EnemyRB.AddForce(finalVelocity * EnemyRB.mass, ForceMode2D.Impulse);
    }

    public override void TakeDamage(float damageTaken)
    {
        enemyStats.currentHealth -= damageTaken;
        if (!IsDead)
        {

            
        }
        else
        {
            Die();
            EnemyAnim.SetTrigger(parameterDieHash);
        }
    }

    public override void RemoveTarget()
    {
        base.RemoveTarget();
        ChangeState(new RoboTurtlePatrolState());
    }

    public override void PlaySoundAnimation(string soundtype)
    {
        switch (soundtype)
        {
            case "Ranged":
                PlaySound(AudioParams.SoundPoolGroups.ROBOTURTLE, AudioParams.SoundPools.RANGED);
                break;
            case "Jump":
                PlaySound(AudioParams.SoundPoolGroups.ROBOTURTLE, AudioParams.SoundPools.JUMPING);
                break;
            case "Dying":
                PlaySoundOnceOnManager(AudioParams.SoundPoolGroups.ROBOTURTLE, AudioParams.SoundPools.DUYING);
                break;


            default:
                break;
        }
    }

    //AnimationEvent
    public override void Destroy()
    {
        Destroy(gameObject, 1.0f);
    }

    public void ResetMeleeAttackTimer()
    {
        if (currentState is RoboTurtleMeleeState)
        {
            ((RoboTurtleMeleeState)currentState).canMelee = true;
        }
    }

    


    //float parameters
    [HideInInspector]
    public int parameterSpeedHash = Animator.StringToHash("speed");
    //trigger parameters
    [HideInInspector]
    public int parameterRangedHash = Animator.StringToHash("ranged");
    [HideInInspector]
    public int parameterMeleeHash = Animator.StringToHash("melee");
    [HideInInspector]
    public int parameterDamageHash = Animator.StringToHash("damage");
    [HideInInspector]
    public int parameterDieHash = Animator.StringToHash("die");
    //bool parameters
    [HideInInspector]
    public int parameterisFallingHash = Animator.StringToHash("isFalling");
    

}
