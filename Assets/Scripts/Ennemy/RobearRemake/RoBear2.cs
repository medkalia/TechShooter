using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RoBear2 : Enemy
{
    #region Variables
    [Header("Melee")]
    public GameObject meleeShot;
    [Header("Summon")]
    public List<GameObject> summonPool = new List<GameObject>();
    public Transform summonSpawn;
    

    [HideInInspector] public BossStats bossStats = new BossStats();
    [HideInInspector] public StateInfo stateInfo = new StateInfo();
     public List<AttackStates> pickedAttackStates = new List<AttackStates>();
    [HideInInspector] public enum AttackStates { Ranged, Melee, Summon };
    [HideInInspector] public float currentIdleDuration;

    [HideInInspector] public int parameterIdleHash = Animator.StringToHash("idle");
    [HideInInspector] public int parameterRangedHash = Animator.StringToHash("ranged");
    [HideInInspector] public int parameterMeleeHash = Animator.StringToHash("melee");
    [HideInInspector] public int parameterLaunchMeleeHash = Animator.StringToHash("launchMelee");
    [HideInInspector] public int parameterSummonHash = Animator.StringToHash("summon");
    [HideInInspector] public int parameterHurtHash = Animator.StringToHash("hurt");
    [HideInInspector] public int parameterDieHash = Animator.StringToHash("die");


    private Dictionary<int, string> animationDictionary = new Dictionary<int, string>();
    #endregion

    #region Main methods
    protected override void Start()
    {
        base.Start();
        animationDictionary.Add(parameterIdleHash,"Idle");
        animationDictionary.Add(parameterRangedHash, "InitiateRanged");
        animationDictionary.Add(parameterMeleeHash, "InitiateMelee");
        animationDictionary.Add(parameterLaunchMeleeHash, "Melee");
        animationDictionary.Add(parameterSummonHash, "Summon");
        animationDictionary.Add(parameterHurtHash, "Hurt");
        animationDictionary.Add(parameterDieHash, "Die");

        PlaySound(AudioParams.SoundPoolGroups.ROBEAR, AudioParams.SoundPools.EXISTING);
        bossStats.rangedCD = enemyStats.rangedCD;
        stateInfo.nextHealthMilestone = enemyStats.baseHealth - ((stateInfo.milestonePercentage * enemyStats.baseHealth) / 100);
        currentIdleDuration = bossStats.idleDuration;
    }

    public void Update()
    {
        if (!aiActive)
            return;
        else
            currentAIState.UpdateState(this);

        HandleBar();
    }

    private void OnDrawGizmos()
    {
        if (currentAIState != null)
        {
            Gizmos.color = currentAIState.sceneGizmoColor;
            Gizmos.DrawCube(transform.position, new Vector3(2, 2, 3));
        }
    }

    public override void TakeDamage(float damageTaken)
    {
        enemyStats.currentHealth -= damageTaken;
    }
    #endregion

    #region State methods
    public override bool CheckStateTime(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        //Debug.Log("State " + currentAIState.name + " Asking for timer, timer =  " + stateTimeElapsed);
        return stateTimeElapsed >= duration;
    }
    public override bool CheckCD(string CDName, float CD)
    {
        if (!CDDictionnary.ContainsKey(CDName))
            CDDictionnary.Add(CDName, 0);
        CDDictionnary[CDName] += Time.deltaTime;
        if (CDDictionnary[CDName] > CD)
        {
            CDDictionnary[CDName] = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void TransitionToState(AI_State nextAIState)
    {
        if (nextAIState != remainInAIState)
        {
            currentAIState.ExitState(this);
            OnExitState();
            currentAIState = nextAIState;
            currentAIState.EnterState(this);
        }
    }
    //reset all state variables to avoid incoherence
    protected override void OnExitState()
    {
        stateTimeElapsed = 0;
        //---- Ranged Attack
        enemyInfo.isRangedAttacking = false;
        stateInfo.startedRangedAttacking = false;
        //---- Melee Attack
        enemyInfo.isMeleeAttacking = false;
        stateInfo.startedMeleeAttacking = false;
        stateInfo.canLaunchMelee = false;
        if (stateInfo.currentMeleeShot != null)
        {
            Destroy(stateInfo.currentMeleeShot);
            stateInfo.currentMeleeShot = null;
        }
        //---- Summon Attack
        stateInfo.isSummoning = false;
        stateInfo.canSummonNext = false;
    }

    public void BuildCycle()
    {
        if (pickedAttackStates.Count > 0)
        {
            currentIdleDuration = 0;
        }
        else
        {
            currentIdleDuration = bossStats.idleDuration;
            pickedAttackStates = Enum.GetValues(typeof(AttackStates)).Cast<AttackStates>().ToList();
            IListExtensions.Shuffle(pickedAttackStates);
        }
        
    }
    #endregion

    #region Animation methods
    public void HandleAnimation(int parameterHash)
    {
        if (animationDictionary != null && animationDictionary.ContainsKey(parameterHash))
        {
            if (!EnemyAnim.GetCurrentAnimatorStateInfo(0).IsName(animationDictionary[parameterHash]))
                EnemyAnim.SetTrigger(parameterHash);
        }
        else
        {
            Debug.LogError("An Animation with hash (" + parameterHash + ") has been called while it doesn't exist in \"" + this.name + "\" => \"animationDictionnary\" [State :" + currentAIState.name + "]");
        }
    }

    public override void Shoot()
    {
        stateInfo.startedRangedAttacking = true;
    }

    public override void Attack()
    {
        stateInfo.startedMeleeAttacking = true;
    }

    public void LaunchMelee()
    {
        stateInfo.canLaunchMelee = true;
    }

    public void Summon()
    {
        stateInfo.canSummonNext = true;
    }

    public void FinishedHurting()
    {
        stateInfo.isHurting = false;
    }

    
    #endregion

    #region Helper methods
    public override float GetTimerDuration(TimerType timerType)
    {
        switch (timerType)
        {
            case TimerType.IDLE:
                return currentIdleDuration;

            case TimerType.PATROL:
                Debug.LogError("No duration assigned for " + timerType);
                return 0;
            default:
                Debug.LogError("No duration assigned for " + timerType);
                return 0;
        }
    }

    public void HandleBar()
    {
        if (target != null)
        {
            transform.Find("EnemyBossCanvas").gameObject.SetActive(true);
        }
    }
    #endregion

    #region Helper Classes
    public class BossStats
    {
        public int numberOfNormalShots = 4;
        public float rangedCD = .5f;
        public float normalMeleeChargingTime = 2f;
        public float idleDuration = 4f;
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

    public class StateInfo
    {
        public float nextHealthMilestone = 0;
        public float milestonePercentage = 25f;
        public bool isInCycle = false;
        public GameObject currentMeleeShot = null;
        public bool isSummoning = false;
        public bool canSummonNext = false;
        public int summonCounter = 0;
        public bool isDying = false;
        public bool isHurting = false;
        public bool startedRangedAttacking = false;
        public bool startedMeleeAttacking = false;
        public bool canLaunchMelee = false;

    }
    #endregion

    public override void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public override void LookAtTarget()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override void PlaySoundAnimation(string soundtype)
    {
        throw new System.NotImplementedException();
    }

    public override void ShootProjectile()
    {
        throw new System.NotImplementedException();
    }
}
