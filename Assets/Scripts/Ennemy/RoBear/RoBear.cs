using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoBear : Enemy 
{
    [HideInInspector]
    public List<string> possibleAttackStates = new List<string>(new string[] { "normalRanged", "normalMelee", "Summon" });
    [HideInInspector]
    public StateInfo stateInfo = new StateInfo();
    [HideInInspector]
    public List<string> pickedAttackStates = new List<string>();
    [HideInInspector]
    public BossStats bossStats = new BossStats();
    [Header("Objects")]
    public GameObject meleeShot;

    [Header("Summon")]
    public List<GameObject> summonPool = new List<GameObject>();
    public Transform summonSpawn;

    private IEnumerator coroutine;

    public Text testText;




    protected override void Start()
    {
        base.Start();
        PlaySound(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.EXISTING);
        bossStats.rangedCD = enemyStats.rangedCD;
        enemyMovementInfo.FacingDirection = -1; // RoBear always faces left
        stateInfo.nextHealthMilestone = enemyStats.baseHealth - ((stateInfo.milestonePercentage * enemyStats.baseHealth) / 100);
        ChangeState(new RoBearIdleState());

        
    }

    protected void Update()
    {
        currentState.Execute();
        HandleBar();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!enemyInfo.isDisabled)
        {
            base.OnTriggerEnter2D(other);
            currentState.OnTriggerEnter2D(other);
        }
    }

    //***MELEE

    public override void Attack()
    {
        stateInfo.currentMeleeShot = Instantiate(meleeShot, shotSpawnTransform.position, shotSpawnTransform.rotation);
        stateInfo.currentMeleeShot.transform.parent = shotSpawnTransform;
        coroutine = ChargeShot();
        StartCoroutine(coroutine);
    }

    public IEnumerator ChargeShot()
    {
        float chargeTime = 0f;
        while (chargeTime < bossStats.normalMeleeChargingTime)
        {
            chargeTime += .5f;
            yield return new WaitForSeconds(.5f);
        }
        EnemyAnim.SetTrigger(parameterLaunchMeleeHash);
    }

    public void ReleaseShot()
    {
        if (GetComponentInChildren<LineWithChargeProjectileMover>() != null)
            GetComponentInChildren<LineWithChargeProjectileMover>().Release();
        PickNextAttackState();
        stateInfo.currentMeleeShot = null;
    }

    //****RANGED
    public override void Shoot()
    {
        coroutine = ShootAndWait();
        StartCoroutine(coroutine);
    }

    public IEnumerator ShootAndWait()
    {
        int i = 1;
        for (i = 1; i <= bossStats.numberOfNormalShots; i++)
        {
            ShootProjectile();
            yield return new WaitForSeconds(bossStats.rangedCD);
        }
        
        //not optimal shouldn't be here but i was stuck couldn't find a solution
        if (i > bossStats.numberOfNormalShots)
        {
            EnemyAnim.SetTrigger(parameterIdleHash);
            PickNextAttackState();
        }
           
    }

    public override void ShootProjectile()
    {
        GameObject newShot = Instantiate(shot, shotSpawnTransform.position, new Quaternion());
        newShot.transform.parent = shotSpawnTransform;
        newShot.transform.localScale = new Vector2(-newShot.transform.localScale.x, -newShot.transform.localScale.y); //just sprite not same direction
    }

    //***BOSS STATE
    public void BuildCycle()
    {
        pickedAttackStates = new List<string>(possibleAttackStates);
        IListExtensions.Shuffle(pickedAttackStates);
    }

    public void PickNextAttackState()
    {
        if (pickedAttackStates.Count > 0)
        {
            string currentAttackState = pickedAttackStates[0];
            pickedAttackStates.RemoveAt(0);
            switch (currentAttackState)
            {
                case "normalRanged":
                    ChangeState(new RoBearNormalRangedState());
                    break;

                case "normalMelee":
                    ChangeState(new RoBearNormalMeleeState());
                    break;

                case "Summon":
                    ChangeState(new RoBearSummonState());
                    break;

                default:
                    Debug.Log("No method found for the attack state : " + currentAttackState);
                    break;
            }
        }
        else
        {
            stateInfo.isInCycle = false;
            ChangeState(new RoBearIdleState());
        }

    }

    public void Summon()
    {
        Instantiate(summonPool[0], summonSpawn.position, summonSpawn.rotation);
        stateInfo.summonCounter--;
        if (stateInfo.summonCounter <= 0)
        {
            EnemyAnim.SetTrigger(parameterEndSummongHash);
            PickNextAttackState();
        }
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }

    public override void LookAtTarget()
    {
        
    }

    public void HandleBar()
    {
        if (target != null)
        {
            transform.Find("EnemyBossCanvas").gameObject.SetActive(true);
        }
    }

    public override void TakeDamage(float damageTaken)
    {
        enemyStats.currentHealth -= damageTaken;
        if (!IsDead)
        {
            if (enemyStats.currentHealth < stateInfo.nextHealthMilestone)
            {
                PlaySound(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.FATIGUE);
                if (coroutine != null)
                    StopCoroutine(coroutine);
                //Default values
                stateInfo.startedMeleeAttacking = false;
                stateInfo.startedRangedAttacking = false;
                stateInfo.isInCycle = false;
                enemyInfo.isRangedAttacking = false;
                enemyInfo.isMeleeAttacking = false;
                stateInfo.isSummoning = false;
                //EnemyAnim.SetTrigger(parameterEndSummongHash);
                if (stateInfo.currentMeleeShot != null)
                {
                    Destroy(stateInfo.currentMeleeShot);
                    stateInfo.currentMeleeShot = null;
                }
                ChangeState(new RoBearIdleState());
                stateInfo.nextHealthMilestone = stateInfo.nextHealthMilestone - ((stateInfo.milestonePercentage * enemyStats.baseHealth) / 100);
                bossStats.rageLevel += 1;
                bossStats.Enrage(bossStats.rageLevel);
                EnemyAnim.SetTrigger(parameterHurtHash);
            }
        }
        else
        {
            stateInfo.isDying = true;
            PlaySoundOnceOnManager(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.DUYING);
            Die();
            EnemyAnim.SetTrigger(parameterDieHash);
        }
    }

    public override void Move()
    {

    }

    public override void PlaySoundAnimation(string soundtype)
    {
        throw new NotImplementedException();
    }

    public override void RemoveTarget()
    {
        base.RemoveTarget();
        //ChangeState(new RoBearIdleState());
    }

    public class StateInfo
    {
        public float nextHealthMilestone = 0;
        public float milestonePercentage = 25f;
        public bool isInCycle = false;
        public GameObject currentMeleeShot = null;
        public bool isSummoning = false;
        public int summonCounter = 0;
        public bool isDying = false;
        public bool startedRangedAttacking = false;
        public bool startedMeleeAttacking = false;

    }

    public class BossStats
    {
        public int numberOfNormalShots = 4;
        public float rangedCD = .5f;
        public float normalMeleeChargingTime = 2f;
        public float idleDuration = 2f;
        public int summonNumber = 1;

        public int rageLevel = 1;

        public void Enrage(int rageLevel)
        {
            switch (rageLevel)
            {
                case 1:
                    numberOfNormalShots = 3;
                    rangedCD = .7f;
                    normalMeleeChargingTime = 2.5f;
                    idleDuration = 3f;
                    summonNumber = 1;
                    break;
                case 2:
                    numberOfNormalShots = 4;
                    rangedCD = .5f;
                    normalMeleeChargingTime = 2f;
                    idleDuration = 2.5f;
                    summonNumber = 1;
                    break;
                case 3:
                    numberOfNormalShots = 6;
                    rangedCD = .4f;
                    normalMeleeChargingTime = 1.5f;
                    idleDuration = 2f;
                    summonNumber = 2;
                    break;
                case 4:
                    numberOfNormalShots = 8;
                    rangedCD = .2f;
                    normalMeleeChargingTime = .75f;
                    idleDuration = 1f;
                    summonNumber = 4;
                    break;
                default:
                    Debug.Log("RageLevel Not Assigned");
                    break;
            }
        }
    }


    //trigger parameters
    [HideInInspector]
    public int parameterRangedHash = Animator.StringToHash("ranged");
    [HideInInspector]
    public int parameterIdleHash = Animator.StringToHash("idle");
    [HideInInspector]
    public int parameterMeleeHash = Animator.StringToHash("melee");
    [HideInInspector]
    public int parameterSummonHash = Animator.StringToHash("summon");
    [HideInInspector]
    public int parameterLaunchMeleeHash = Animator.StringToHash("launchMelee");
    [HideInInspector]
    public int parameterHurtHash = Animator.StringToHash("hurt");
    [HideInInspector]
    public int parameterDieHash = Animator.StringToHash("die");
    [HideInInspector]
    public int parameterEndSummongHash = Animator.StringToHash("endSummon");
    

}
