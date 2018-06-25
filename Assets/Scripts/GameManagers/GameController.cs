using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameController : MonoBehaviour {
    [HideInInspector]
    public static GameController Instance { get; set; }

    
    public GameObject coinPrefab;

    public string deathScreenTag;

    [Header("Menu")]
    public GameObject pauseMenu;

    [Header("Camera")]
    public Camera mainCamera;

    //****COLLECTABLES
    private float collectableSpawnForceX = 5f;
    private float collectableSpawnForceY = 15f;

    private float coinsTimeStep = 0.2f;

    private GameObject deathScreen;




    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        deathScreen = GameObject.FindGameObjectWithTag(deathScreenTag);
        deathScreen.SetActive(false);
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Pause"))
            PauseGame();
    }

    public void PauseScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
        //    Instantiate(boss,bossSpawn.transform.position,bossSpawn.transform.rotation);
        //}
        if (collision.gameObject.tag == "Player")
        {
            Player.Instance.Kill();

        }
    }

    public void SpawnCollectable (string type, Transform transform, int numberToSpawn)
    {
        Vector2 position = transform.position;
        Quaternion rotation = transform.rotation;
        switch (type)
        {
            case "Coin":
                StartCoroutine(SpawnWithTime(coinPrefab, position, rotation, numberToSpawn, coinsTimeStep));
                break;
        }
    }

    private IEnumerator SpawnWithTime (GameObject objectToSpawn, Vector2 positionOfSpawn, Quaternion rotationOfSpawn, int numberToSpawn, float timeStep)
    {
        int spawnedNumber = 0;
        while (spawnedNumber < numberToSpawn)
        {
            GameObject newSpawn = Instantiate(objectToSpawn, positionOfSpawn, rotationOfSpawn);
            newSpawn.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, collectableSpawnForceX) * ((Random.Range(0, 2) * 2) - 1), collectableSpawnForceY), ForceMode2D.Impulse);
            spawnedNumber++;
            yield return new WaitForSeconds(timeStep);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void RespawnPlayerOnDeath()
    {
        Time.timeScale = 0;
        deathScreen.GetComponent<DeathScreen>().SetLoosingText();
        deathScreen.SetActive(true);
        deathScreen.GetComponent<Animator>().SetTrigger(parameterLostHash);
    }

    [HideInInspector]
    public int parameterLostHash = Animator.StringToHash("lost");



}
