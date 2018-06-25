using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour {

    //**** Public variables
    [Header("Stats")]
    public EnemyData data;
    public EnemyLootData lootData;

    [Space]
    [Header("Objects")]
    public GameObject shot;
    public Transform shotSpawnTransform;

    [Space]
    [Header("Player Interaction")]
    public string playerProjectileTag = "PlayerBolt";
    public Color[] flashingColors = new Color[2];

    [Space]
    [Header("FSM")]
    public bool aiActive = true;
    public AI_State currentAIState;
    public AI_State remainInAIState;
    //New FSM
    [HideInInspector] public enum TimerType { IDLE, PATROL }
    [HideInInspector] protected float stateTimeElapsed;
    [HideInInspector] public Dictionary<string, float> CDDictionnary = new Dictionary<string, float>();
    //--------------------------

    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public EnemyStats enemyStats = new EnemyStats();
    [HideInInspector]
    public EnemyInfo enemyInfo = new EnemyInfo();
    [HideInInspector]
    public EnemyMovementInfo enemyMovementInfo = new EnemyMovementInfo();
    [HideInInspector]
    public IEnemyState currentState;
    [HideInInspector]
    public Animator EnemyAnim { get; private set; }
    [HideInInspector]
    public Transform EnemyTransform { get; private set; }
    [HideInInspector]
    public Rigidbody2D EnemyRB { get; private set; }
    [HideInInspector]
    public AudioManager audioManager;
    //--------------------------
    

    private Canvas healthBarCanvas;
    ////This dictionnary is used to call a function that will handle the animation 
    //protected Dictionary<String, Func<bool>> animationDictionary = new Dictionary<String, Func<bool>>();



    //**** Properties
    [HideInInspector]
    public bool InMeleeRange
    {
        get
        {
            if (target != null)
            {
                return Vector2.Distance(transform.position, target.transform.position) <= enemyStats.meleeRange;
            }
            return false;
        }
    }
    [HideInInspector]
    public bool InRangedRange
    {
        get
        {
            if (target != null)
            {
                return Vector2.Distance(transform.position, target.transform.position) <= enemyStats.rangedRange;
            }
            return false;
        }
    }
    [HideInInspector]
    public bool IsDead
    {
        get
        {
            if (enemyStats.currentHealth <= 0) return true;
            else return false;
        }
    }

    protected virtual void Start()
    {
        audioManager = AudioManager.instance;
        enemyStats.baseHealth = data.baseHealth;
        enemyStats.currentHealth = data.baseHealth;
        enemyStats.contactDamage = data.contactDamage;
        enemyStats.meleeDamage = data.meleeDamage;
        enemyStats.movementSpeed = data.movementSpeed;
        enemyStats.rangedCD = data.rangedCD;
        enemyStats.rangedRange = data.rangedRange;
        enemyStats.meleeCD = data.meleeCD;
        enemyStats.meleeRange = data.meleeRange;
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        enemyMovementInfo.ChangedFacingDirection += new ChangedFacingDirectionEventHandler(HandleHealthBar);
        EnemyAnim = gameObject.GetComponent<Animator>();
        EnemyTransform = gameObject.GetComponent<Transform>();
        EnemyRB = gameObject.GetComponent<Rigidbody2D>();
        healthBarCanvas = GetComponentInChildren<Canvas>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerProjectileTag)
        {
            TakeDamage(Player.Instance.projectileStats.projectileBaseDamage);
            StartCoroutine(FlashOnHurt());
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" )
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public virtual void RemoveTarget()
    {
        //necessary to avoid problems when enemy is destroyed
        if (this != null)
        {
            target = null;
            EnemySight sightScript = gameObject.GetComponentInChildren<EnemySight>();
            //sightScript.targetTag = "";
        }

    }
    
    public void EndRanged()
    {
        enemyInfo.isRangedAttacking = false;
    }

    public void EndMelee()
    {
        enemyInfo.isMeleeAttacking = false;
    }

    public virtual void Die()
    {
        if (gameObject.name == "RoBear" || gameObject.name == "RoBear2")
            Destroy(transform.Find("EnemyBossCanvas").gameObject);
        else 
            Destroy(transform.Find("EnemyCanvas").gameObject);
        EnemyRB.gravityScale = 0;
        enemyInfo.isDisabled = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        GameController.Instance.SpawnCollectable("Coin", transform, UnityEngine.Random.Range(lootData.minGoldValue, lootData.maxGoldValue) );

    }

    public virtual IEnumerator FlashOnHurt()
    {
        List<Renderer> renderers = new List<Renderer> (GetComponentsInChildren<Renderer>());
        renderers.Add(GetComponent<Renderer>());
        foreach (Renderer currentRenderer in renderers)
        {
            if (currentRenderer != null)
                currentRenderer.material.color = flashingColors[0];
        }
        yield return new WaitForSeconds(.2f);
        foreach (Renderer currentRenderer in renderers)
        {
            if (currentRenderer != null)
                currentRenderer.material.color = flashingColors[1];
        }
        
    }

    public void HandleHealthBar()
    {
        if (gameObject.name != "RoBear")
        {
            if (enemyMovementInfo.spriteOriginalDirection == -1)
            {
                if (enemyMovementInfo.FacingDirection == -1) healthBarCanvas.gameObject.transform.localScale = new Vector2(Math.Abs(healthBarCanvas.gameObject.transform.localScale.x) * -1, healthBarCanvas.gameObject.transform.localScale.y);
                else healthBarCanvas.gameObject.transform.localScale = new Vector2(Math.Abs(healthBarCanvas.gameObject.transform.localScale.x), healthBarCanvas.gameObject.transform.localScale.y);
            }
            else
            {
                if (enemyMovementInfo.FacingDirection == 1) healthBarCanvas.gameObject.transform.localScale = new Vector2(Math.Abs(healthBarCanvas.gameObject.transform.localScale.x) * -1, healthBarCanvas.gameObject.transform.localScale.y);
                else healthBarCanvas.gameObject.transform.localScale = new Vector2(Math.Abs(healthBarCanvas.gameObject.transform.localScale.x), healthBarCanvas.gameObject.transform.localScale.y);
            }
        }
    }
    //************Play 
    public virtual void PlaySound(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName)
    {
        audioManager.PlaySound(enemyName, soundEffectName, gameObject);
    }
    public virtual void PlaySoundOnManager(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName)
    {
        audioManager.PlaySound(enemyName, soundEffectName, audioManager.gameObject);
    }
    //************Play Once
    public virtual void PlaySoundOnce(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName)
    {
        audioManager.PlaySoundOnce(enemyName, soundEffectName, gameObject);
    }
    public virtual void PlaySoundOnceOnManager(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName)
    {
        audioManager.PlaySoundOnce(enemyName, soundEffectName, audioManager.gameObject);
    }
    //************Play On Animation
    public virtual bool PlaySoundWithAnimation(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName,string animationName, bool test)
    {
       return audioManager.PlaySoundWithAnimation(enemyName, soundEffectName, gameObject, animationName, test);
    }
    public virtual bool PlaySoundWithAnimationOnManager(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName, string animationName, bool test)
    {
        return audioManager.PlaySoundWithAnimation(enemyName, soundEffectName, audioManager.gameObject, animationName, test);
    }
    //************Play Modified
    public virtual void PlaySoundModified(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName, float volumeEffector)
    {
        audioManager.PlaySoundModified(enemyName, soundEffectName, gameObject, volumeEffector);
    }
    public virtual void PlaySoundModifiedOnManager(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName, float volumeEffector)
    {
        audioManager.PlaySoundModified(enemyName, soundEffectName, audioManager.gameObject, volumeEffector);
    }
    //************Update
    public virtual void UpdateSound(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName, float volumeEffector)
    {
        audioManager.UpdateSound(enemyName, soundEffectName, gameObject, volumeEffector);
    }
    public virtual void StopSoundOnManager(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName, float volumeEffector)
    {
        audioManager.UpdateSound(enemyName, soundEffectName, audioManager.gameObject, volumeEffector);
    }
    //************Stop
    public virtual void StopSound(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName)
    {
        audioManager.StopSound(enemyName, soundEffectName, gameObject);
    }
    public virtual void StopSoundOnManager(AudioParams.SoundPoolGroups enemyName, AudioParams.SoundPools soundEffectName)
    {
        audioManager.StopSound(enemyName, soundEffectName, audioManager.gameObject);
    }


    public abstract void Destroy();

    public abstract void Move();

    public abstract void LookAtTarget();

    public abstract void Shoot();

    public abstract void ShootProjectile();

    public abstract void Attack();

    public abstract void TakeDamage(float damageTaken);

    public abstract void PlaySoundAnimation(string soundtype);

    //public abstract string SoundPicker(string type);

    //protected abstract void PickSoundWhenReadyCaller(string type, Func<string, string> soundPicker);

    //public abstract void PickSound(string soundType);



    public delegate void ChangedFacingDirectionEventHandler();

    public class EnemyMovementInfo
    {
        private float facingDirection = 1;
        public float spriteOriginalDirection = 1;
        public bool isGrounded = false;
        public bool isFalling = false;
        public bool canMoveRight = true;
        public bool canMoveLeft = true;

        public float FacingDirection
        {
            get { return facingDirection; }
            set
            {
                if (value != facingDirection)
                {
                    OnChangedFacingDirection();
                    facingDirection = value;
                }
                else
                {
                    facingDirection = value;
                }
            }
        }
        public event ChangedFacingDirectionEventHandler ChangedFacingDirection;
        public void OnChangedFacingDirection()
        {
            if (ChangedFacingDirection != null)
            {
                ChangedFacingDirection();
            }
        }

        //public EnemyMovementInfo()
        //{
        //    FacingDirection = 1;
        //    isGrounded = false;
        //    isFalling = false;
        //}
    }

    public class EnemyInfo
    {
        public bool isRangedAttacking = false;
        public bool isMeleeAttacking = false;
        public bool isDisabled = false;
        
    }

    public class EnemyStats
    {
        public float currentHealth = 100f;
        public float baseHealth = 100f;
        public float contactDamage = 40f;
        public float meleeDamage = 50f;
        public float movementSpeed = 2f;
        public float rangedCD = 7f;
        public float rangedRange = 5f;
        public float meleeCD = 2f;
        public float meleeRange = 3f;
    }

    //--------------------------
    //New FSM
    public virtual float GetTimerDuration(TimerType timerType) { return 0f; }
    public virtual bool CheckStateTime(float duration) { return false; }
    public virtual bool CheckCD(string CDName, float CD) { return false; }
    public virtual void TransitionToState(AI_State nextState) { }
    protected virtual void OnExitState() { }
    //--------------------------
}

//// ---------------
////  String => Func<bool>
//// ---------------
//[Serializable]
//public class StringFuncDictionary : SerializableDictionary<String, Func<bool>> { }

////---------------
//// String => Func<bool>
////---------------
//#if UNITY_EDITOR
//[UnityEditor.CustomPropertyDrawer(typeof(SliderAudioMixerGroupDictionary))]

//public class StringFuncDrawer : SerializableDictionaryDrawer<String, Func<bool>>
//{
//    protected override SerializableKeyValueTemplate<String, Func<bool>> GetTemplate()
//    {
//        return GetGenericTemplate<SerializableStringFuncTemplate>();
//    }
//}
//internal class SerializableStringFuncTemplate : SerializableKeyValueTemplate<String, Func<bool>> { }
//#endif
