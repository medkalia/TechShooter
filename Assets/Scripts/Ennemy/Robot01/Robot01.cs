using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot01 : Enemy {

    
    [HideInInspector]
    public Animator Robot01JetAnim { get; private set; }

    public Text debugText = null;


    protected override void Start () {
        //**Initialisations
        base.Start();
        AudioManager.instance.PlaySound(AudioParams.SoundPoolGroups.ROBOT01, AudioParams.SoundPools.WALKING,gameObject);
        Robot01JetAnim = EnemyTransform.Find("JetRenderer").gameObject.GetComponent<Animator>();
        ChangeState(new Robot01IdleState());
        enemyMovementInfo.spriteOriginalDirection = -1;
        enemyMovementInfo.FacingDirection = -1;



    }
	
	void Update () {
        if (!enemyInfo.isDisabled)
        {
            LookAtTarget();
            if (currentState != null)
                currentState.Execute();
            if (debugText != null)
                debugText.text = currentState.GetType().ToString() + "\n"
                    + "Target ? " + (target != null ? "true" : "false")
                    + "Can Move L : R ?" + enemyMovementInfo.canMoveLeft.ToString() + " : " + enemyMovementInfo.canMoveRight.ToString();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!enemyInfo.isDisabled)
        {
            base.OnTriggerEnter2D(other);
            if (currentState != null)
                currentState.OnTriggerEnter2D(other);
        }
        if (other.tag == playerProjectileTag)
        {
            if (target == null)
            {
                ContactPoint2D[] contacts = new ContactPoint2D[10];
                Vector2 pointOfContact = other.transform.position;
                Vector2 contactDirection = pointOfContact - (Vector2)transform.position;
                enemyMovementInfo.FacingDirection = Mathf.Sign(contactDirection.x);
                ChangeState(new Robot01PatrolState());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!enemyInfo.isDisabled)
        {
            currentState.OnTriggerStay2D(other);
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
            Robot01JetAnim.SetFloat(parameterSpeedHash, 1);
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
            if (InRangedRange && (targetDirection < 0 && enemyMovementInfo.FacingDirection == 1) || (targetDirection > 0 && enemyMovementInfo.FacingDirection == -1) )
            {
                enemyMovementInfo.FacingDirection *= -1;
                EnemyTransform.localScale = new Vector3(Mathf.Abs(EnemyTransform.localScale.x) * -enemyMovementInfo.FacingDirection, EnemyTransform.localScale.y, EnemyTransform.localScale.z);// we put - enemyMovementInfo.FacingDirection cause this enemy's sprite starts facing left 
            }
        }
    }
    //this starts the animation for ranged
    public override void Shoot()
    {
        enemyInfo.isMeleeAttacking = false;
        enemyInfo.isRangedAttacking = true;
        Robot01JetAnim.SetFloat(parameterSpeedHash, 0);
        EnemyAnim.SetTrigger(parameterRangedHash);
    }
    //this launches the projectile 
    public override void ShootProjectile()
    {
        GameObject newShot = Instantiate(shot, shotSpawnTransform.position, shotSpawnTransform.rotation);
        newShot.transform.parent = shotSpawnTransform;
    }
    //this starts the animation for melee
    public override void Attack()
    {
        enemyInfo.isRangedAttacking = false;
        enemyInfo.isMeleeAttacking = true;
        Robot01JetAnim.SetFloat(parameterSpeedHash, 0);
        EnemyAnim.SetTrigger(parameterMeleeHash);
    }

    public override void TakeDamage(float damageTaken)
    {
        enemyStats.currentHealth -= damageTaken;
        if (!IsDead)
        {
            //EnemyAnim.SetTrigger(parameterDamageHash);
        }
        else
        {
            EnemyAnim.SetTrigger(parameterDieHash);
            Robot01JetAnim.SetTrigger(parameterDieHash);
            Die();
        }
    }

    public override void RemoveTarget()
    {
        base.RemoveTarget();
        ChangeState(new Robot01PatrolState());
    }

    public override void PlaySoundAnimation(string soundtype)
    {
        switch (soundtype)
        {
            case "Melee":
                PlaySound(AudioParams.SoundPoolGroups.ROBOT01, AudioParams.SoundPools.MELEE);
                break;
            case "Dying":
                PlaySoundOnce(AudioParams.SoundPoolGroups.ROBOT01, AudioParams.SoundPools.DUYING);
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


}
