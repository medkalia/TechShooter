using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public delegate void DeadEventHandler();

public class Player : MonoBehaviour
{

    [Header("Stats")]
    public PlayerData data;
    [Space]
    [Header("Objects")]
    public GameObject shot;
    public Transform shotSpawnTransform;
    [Space]
    [Header("Enemy Interaction")]
    public List<string> rangedDamageSourcesTags ;
    public List<string> contactDamageSourcesTags;
    public List<string> meleeDamageSourcesTags;
    public GameObject barrier;

    [Space]
    [Header("Visuals")]
    [Tooltip("The flashing colors when getting hurt : first color is hurt color second is the original")]
    public Color[] flashingColors = new Color[2];

    [HideInInspector]
    public MovementInfo movementInfo = new MovementInfo();
    [HideInInspector]
    public PlayerInfo playerInfo = new PlayerInfo();
    [HideInInspector]
    public PlayerStats playerStats = new PlayerStats();
    [HideInInspector]
    public ProjectileStats projectileStats = new ProjectileStats();
    [HideInInspector]
    public event DeadEventHandler Dead;
    // this is used to make player into a singleton
    [HideInInspector]
    public static Player Instance { get; set; }
    //**** Private variables
    private Rigidbody2D playerRB;
    private Transform playerTransform;
    private Animator playerAnim;
#pragma warning disable CS0169 // Le champ 'Player.playerVelocity' n'est jamais utilisé
    private Vector3 playerVelocity;
#pragma warning restore CS0169 // Le champ 'Player.playerVelocity' n'est jamais utilisé
#pragma warning disable CS0169 // Le champ 'Player.velocityXSmoothing' n'est jamais utilisé
    private float velocityXSmoothing;
#pragma warning restore CS0169 // Le champ 'Player.velocityXSmoothing' n'est jamais utilisé
    private Vector3 originalScale;

    //**** Properties
    [HideInInspector]
    public bool IsDead
    {
        get
        {
            if (playerStats.currentHealth <= 0)
            {
                OnDead();
                
            }
            return playerStats.currentHealth <= 0;
        }
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        
        //**Initialisations
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        playerTransform = gameObject.GetComponent<Transform>();
        originalScale = playerTransform.localScale;
        movementInfo.isRuning = false;
        movementInfo.isGrounded = true;
        playerStats.fireRate = data.fireRate;
        playerStats.baseHealth = data.baseHealth;
        playerStats.currentHealth = data.baseHealth;
        playerStats.immortalityTime = data.immortalityTime;
        playerStats.maxTechPoints = data.maxTechPoints;
        playerStats.currentTechPoints = data.startingTechPoints;
        playerStats.techPointsRegenRate = data.techPointsRegenRate;
        playerStats.jumpVelocity = data.jumpVelocity;
        playerStats.maxSpeed = data.maxSpeed;
        playerStats.acceleration = data.acceleration;
        playerStats.fallMultiplier = data.fallMultiplier;
        playerStats.lowJumpMultiplier = data.lowJumpMultiplier;
        playerStats.recievedDamageAmplifier = data.recievedDamageAmplifier;
        projectileStats.projectileBaseDamage = data.projectileBaseDamage;
        projectileStats.projectileLifeTime = data.projectileLifeTime;
        projectileStats.projectileSpeed = data.projectileSpeed;






    }

    private void Update()
    {
        if (this != null)
        {
            HandleInput();

            if (playerAnim != null && playerRB != null)
            {
                playerAnim.SetBool(parameterIsLandingHash, movementInfo.isLanding);
                playerAnim.SetBool(parameterIsGroundedHash, movementInfo.isGrounded);
                playerAnim.SetBool(parameterIsRuningHash, (Mathf.Abs(playerRB.velocity.x) > 0.01f) && movementInfo.isRuning);
                playerAnim.SetBool(parameterIsShootingHash, movementInfo.isShooting);
                playerAnim.SetBool(parameterIsJumpingHash, movementInfo.isJumping);
            }

            //regenerates energy
            if (!playerInfo.isRegenEnergy && playerStats.currentTechPoints < playerStats.maxTechPoints)
            {
                StartCoroutine(RegainTechPoints());
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (this != null && playerRB != null)
        {
            //doing this separations garentees that the button push is detected (cause in update) and the physics are well applied (cause in fixed update)
            if (movementInfo.jumpRequest)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, playerStats.jumpVelocity );
                //playerRB.AddForce(Vector2.up * playerStats.jumpVelocity, ForceMode2D.Impulse);
                movementInfo.jumpRequest = false;
            }

            //dealing with player's gravity for jumping
            if (playerRB.velocity.y < 0)
            {
                playerRB.gravityScale = playerStats.fallMultiplier;
            }
            else if (playerRB.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
            {
                playerRB.gravityScale = playerStats.lowJumpMultiplier;
            }
            else
            {
                playerRB.gravityScale = 1f;
            }



            //animation for landing
            if (playerRB.velocity.y < -1.5f && !movementInfo.isGrounded)
                movementInfo.isLanding = true;
            //Debug.Log(playerRB.velocity.y);

            movementInfo.velocity = playerRB.velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            DataController.Instance.GetProgressService().levelGoldCount++;
        }
        if (rangedDamageSourcesTags.Contains(other.tag))
        {
           StartCoroutine(PlayerAction.TakeDamage(this,other,"ranged"));
        }else if (meleeDamageSourcesTags.Contains(other.tag))
        {
            StartCoroutine(PlayerAction.TakeDamage(this, other, "melee"));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (contactDamageSourcesTags.Contains(collision.collider.tag))
        {
            StartCoroutine(PlayerAction.TakeDamage(this, collision.collider, "contact"));
        }   
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (contactDamageSourcesTags.Contains(collision.collider.tag))
        {
            StartCoroutine(PlayerAction.TakeDamage(this, collision.collider, "contact"));
        }
    }

    //This methode handles capturing user input
    private void HandleInput()
    {
        //blocks input while being hit
        //if (playerInfo.isHurting)
        //{
        //    finishedLanding();
        //    return;
        //}

        if (!IsDead)
        {
            Vector2 input = new Vector2();
            // Input for walking
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
            }
            else
            {
                input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }

            PlayerAction.Walk(this, input);

            // Input for Jumping
            if (!IsDead && movementInfo.isGrounded)
            {
                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    if (CrossPlatformInputManager.GetButtonDown("Jump")) PlayerAction.Jump(this);
                }
                else
                {
                    if (Input.GetButtonDown("Jump")) PlayerAction.Jump(this);
                }
            }

            // Input for firing
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("LandingShoot"))
            {
                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    if (CrossPlatformInputManager.GetButton("Fire1")) movementInfo.isShooting = true;
                    else movementInfo.isShooting = false;

                    if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > playerInfo.nextFire)
                    {
                        //Debug.Log("At input: Time = " + Time.time + "playerInfo.nextFire" + playerInfo.nextFire);
                        playerInfo.nextFire = Time.time + playerStats.fireRate;
                        PlayerAction.Shoot(this);
                    }
                }
                else
                {
                    if (Input.GetButton("Fire1")) movementInfo.isShooting = true;
                    else movementInfo.isShooting = false;

                    if (Input.GetButton("Fire1") && Time.time > playerInfo.nextFire)
                    {
                        //Debug.Log("At input: Time = " + Time.time + "playerInfo.nextFire" + playerInfo.nextFire);
                        playerInfo.nextFire = Time.time + playerStats.fireRate;
                        PlayerAction.Shoot(this);
                    }
                }
                
            }
        } 
    }

    //this class contains all player actions
    public static class PlayerAction
    {

        public static void Walk(Player player, Vector2 input)
        {
            
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            Transform playerTransform = player.GetComponent<Transform>();
            Animator playerAnim = player.GetComponent<Animator>();
            if (playerRB != null)
            {
                playerRB.AddForce(Vector3.right * player.playerStats.acceleration * input.x);
                player.movementInfo.directionX = Mathf.Sign(playerRB.velocity.x);
                if (!player.movementInfo.isJumping)
                    player.movementInfo.isRuning = input.x != 0;
                //Orientation of running and shooting animations 
                if (input.x != 0)
                {
                    player.movementInfo.facingDirection = Mathf.Sign(input.x);
                    playerTransform.localScale = new Vector3(player.originalScale.x * player.movementInfo.facingDirection, player.originalScale.y, player.originalScale.z);
                }

                //limiting speed 
                if (Mathf.Abs(playerRB.velocity.x) > player.playerStats.maxSpeed)
                    playerRB.velocity = new Vector2(player.playerStats.maxSpeed * player.movementInfo.directionX, playerRB.velocity.y);

                //limiting speed 
                //if (playerRB.velocity.magnitude > player.playerStats.maxSpeed)
                //{
                //    playerRB.velocity = Vector3.ClampMagnitude(playerRB.velocity, player.playerStats.maxSpeed);
                //}

                //animation Speed
                playerAnim.SetFloat(player.parameterSpeedHash, Mathf.Clamp(Mathf.Abs(playerRB.velocity.x) / player.playerStats.maxSpeed, 0.5f, 1.0f));
            }
            
        }

        public static void Jump(Player player)
        {
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            Animator playerAnim = player.GetComponentInChildren<Animator>();
            player.movementInfo.jumpRequest = true;
            //animation for jumping
            playerAnim.SetTrigger(player.parameterJumpHash);
            player.movementInfo.isJumping = true;
            player.movementInfo.isRuning = false;

        }

        public static void Shoot(Player player)
        {

            Animator playerAnim = player.GetComponent<Animator>();
            if (!player.movementInfo.isRuning && player.movementInfo.isGrounded )
            {
                playerAnim.SetTrigger(player.parameterShootStillOnLandHash);
                playerAnim.ResetTrigger(player.parameterJumpShootHash);
                player.ShootBolt();
            }
            else if(player.movementInfo.isRuning && player.movementInfo.isGrounded  )
            {
                playerAnim.SetTrigger(player.parameterShootRunOnLandHash);
                playerAnim.ResetTrigger(player.parameterShootStillOnLandHash);
                playerAnim.ResetTrigger(player.parameterJumpShootHash);
                player.ShootBolt();
            }
            else if (!player.movementInfo.isGrounded)
            {
                playerAnim.SetTrigger(player.parameterJumpShootHash);
                player.ShootBolt();
            }
        }

       
        public static IEnumerator TakeDamage(Player player, Collider2D hostileCollider,string damageType)
        {
            if (!player.playerInfo.isImmortal)
            {
                Animator playerAnim = player.GetComponent<Animator>();
                Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
                Transform playerTransform = player.GetComponent<Transform>();

                player.StartCoroutine(player.FlashOnHurt());

                // Calculate Angle Between the collision point and the player (here it's really the actual point but the position of hostile object) 
                ContactPoint2D[] contacts = new ContactPoint2D[10];
                Vector2 pointOfContact = hostileCollider.transform.position;
                Vector2 contactDirection = pointOfContact - (Vector2)playerTransform.position;
                //If he is grounded we put the y to 0 that way he won't be pushed to the ground if the contact is from above (which will result in him not getting pushed at all)
                if (player.movementInfo.isGrounded) contactDirection = new Vector2(contactDirection.x, 0);

                // We then get the opposite (-Vector2) and normalize it
                contactDirection = -contactDirection.normalized;

                if (damageType == "ranged")
                {
                    playerRB.AddForce(contactDirection * 5, ForceMode2D.Impulse);
                    ProjectileData enemyProjectileProperties = hostileCollider.GetComponent<EnemyProjectileMover>().projectileData;
                    player.playerStats.currentHealth -= enemyProjectileProperties.baseDamage * player.playerStats.recievedDamageAmplifier;
                }
                else if (damageType == "melee")
                {
                    playerRB.AddForce(contactDirection * 15, ForceMode2D.Impulse);
                    Enemy enemy = hostileCollider.GetComponentInParent<Enemy>();
                    player.playerStats.currentHealth -= enemy.enemyStats.meleeDamage * player.playerStats.recievedDamageAmplifier;
                }
                else if (damageType == "contact")
                {
                    playerRB.AddForce(contactDirection * 10, ForceMode2D.Impulse);
                    Enemy enemy = hostileCollider.GetComponent<Enemy>();
                    player.playerStats.currentHealth -= enemy.enemyStats.contactDamage * player.playerStats.recievedDamageAmplifier;

                }
                if (!player.IsDead)
                {
                    //playerAnim.SetTrigger(player.parameterHurtHash);
                    //player.playerInfo.isHurting = true;
                    player.PlaySoundOnPlayer("HURTING");
                    player.playerInfo.isImmortal = true;
                    player.StartCoroutine(player.IndicateImmortal());
                    yield return new WaitForSeconds(player.playerStats.immortalityTime);
                    
                    player.playerInfo.isImmortal = false;
                }
                else
                {
                    playerAnim.SetTrigger(player.parameterDieHash);
                    player.playerInfo.isHurting = true;
                    player.GetComponent<Collider2D>().enabled = false;
                    playerRB.simulated = false;
                    //Destroy(playerRB);
                }
            }
        }
    }

    public void Kill()
    {
        Animator playerAnim = GetComponent<Animator>();
        Rigidbody2D playerRB = GetComponent<Rigidbody2D>();
        Transform playerTransform = GetComponent<Transform>();

        playerStats.currentHealth = 0;
        playerAnim.SetTrigger(parameterDieHash);
        playerInfo.isHurting = true;
        GetComponent<Collider2D>().enabled = false;
        playerRB.simulated = false;
        //Destroy(playerRB);
    }

    //Audio
    public void PlaySoundOnPlayer(string soundPoolName)
    {
        AudioManager.instance.PlaySound(AudioParams.SoundPoolGroups.PLAYER, EnumUtil.GetEnum<AudioParams.SoundPools>(soundPoolName), gameObject);
    }

    public void PlaySoundOnAudioManager(string soundPoolName)
    {
        AudioManager.instance.PlaySound(AudioParams.SoundPoolGroups.PLAYER, EnumUtil.GetEnum<AudioParams.SoundPools>(soundPoolName), AudioManager.instance.gameObject);
    }


    // Coroutines
    private IEnumerator RegainTechPoints()
    {
        playerInfo.isRegenEnergy = true;
        while (playerStats.currentTechPoints < playerStats.maxTechPoints)
        {
            playerStats.currentTechPoints += 1;
            yield return new WaitForSeconds(1/playerStats.techPointsRegenRate);
        }
        playerInfo.isRegenEnergy = false;
    }

    public IEnumerator IndicateImmortal()
    {
        while (playerInfo.isImmortal)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(.1f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    public virtual IEnumerator FlashOnHurt()
    {
        List<Renderer> renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
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

    // to avoid calling a null dead event
    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }

    //class contains the movement infos
    public class MovementInfo
    {
        public float directionX;
        public float facingDirection = 1f;
        public bool isLanding;
        public bool isGrounded;
        public bool isRuning;
        public bool isJumping;
        public bool isShooting;
        public Vector2 velocity;
        public bool jumpRequest;

    }

    //class contains the player state infos
    public class PlayerInfo
    {
        public float nextFire = 0.0f;
        public bool isHurting = false;
        public bool isImmortal = false;
        public bool isRegenHealth = false;
        public bool isRegenEnergy = false;
        public bool isRegenShield = false;
    }

    //class contains the player stats
    public class PlayerStats
    {
        public float maxSpeed = 20f;
        public float jumpVelocity = 7f;
        public float fireRate = 0.2f;
        public float baseHealth = 100f;
        public float currentHealth = 100f;
        public float maxTechPoints = 100f;
        public float currentTechPoints = 25f;
        public float techPointsRegenRate = 1f;
        public float immortalityTime = 2.5f;
        public float recievedDamageAmplifier = 1f;
        public float acceleration = 25f;
        public float fallMultiplier = 2.5f;    
        public float lowJumpMultiplier = 2f;
    }

    //class contains the player's projectile stats
    public class ProjectileStats
    {
        public float projectileBaseDamage = 25f;
        public float projectileLifeTime = 1f;
        public float projectileSpeed = 9f;
    }

    //Methods for animation events
    public void FinishedLanding()
    {
        movementInfo.isLanding = false;
        movementInfo.isJumping = false;
    }

    public void Destroy()
    {
        GameController.Instance.RespawnPlayerOnDeath();
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void ShootBolt()
    {
        if (Time.time < playerInfo.nextFire)
        {
            GameObject newShot = Instantiate(shot, shotSpawnTransform.position, shotSpawnTransform.rotation);
            playerAnim.SetTrigger(parameterBangHash);
            newShot.transform.parent = shotSpawnTransform;
        }
    }

    public void endPain()
    {
        playerInfo.isHurting = false;
    }

    

    

    //comparing hash is quicker than comparing strings plus this gives a way to quickly change parameter names
    //float parameters
    int parameterSpeedHash = Animator.StringToHash("speed");
    //trigger parameters
    int parameterJumpHash = Animator.StringToHash("jump");
    int parameterJumpShootHash = Animator.StringToHash("jumpShoot");
    int parameterShootStillOnLandHash = Animator.StringToHash("shootStillOnLand");
    int parameterShootRunOnLandHash = Animator.StringToHash("shootRunOnLand");
    int parameterBangHash = Animator.StringToHash("bang");
    int parameterHurtHash = Animator.StringToHash("hurt");
    int parameterDieHash = Animator.StringToHash("die");
    //bool parameters
    int parameterIsLandingHash = Animator.StringToHash("isLanding");
    int parameterIsGroundedHash = Animator.StringToHash("isGrounded");
    int parameterIsRuningHash = Animator.StringToHash("isRuning");
    int parameterIsShootingHash = Animator.StringToHash("isShooting");
    int parameterIsJumpingHash = Animator.StringToHash("isJumping");
    
    
}


