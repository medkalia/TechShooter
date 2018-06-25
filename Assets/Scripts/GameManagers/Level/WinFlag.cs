using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinFlag : MonoBehaviour {

    public string playerTag;
    public string winScreenTag;
    public Transform spawnPoint;
    public Transform finishPoint;
    public Slider levelProgressSlider;

    private GameObject winScreen;
    private Player player;
    public float lerpSpeed = 2f;
    private float playerLevelProgress;
    private float initialDistance;
    //private bool hasReachedHalfTheLevel = false;

    private void Start()
    {
        winScreen = GameObject.FindGameObjectWithTag(winScreenTag);
        winScreen.SetActive(false);
        player = Player.Instance;
        if (finishPoint != null && spawnPoint != null)
        {
            playerLevelProgress = Vector2.Distance(player.gameObject.transform.position, finishPoint.position);
            initialDistance = Vector2.Distance(spawnPoint.position, gameObject.transform.position);
        }
            
    }

    private void Update()
    {
        if (Player.Instance != null && finishPoint != null && spawnPoint != null)
        {
            playerLevelProgress = Mathf.Abs(Vector2.Distance(player.gameObject.transform.position, finishPoint.position)) - 2; //-1 for the win flag trigger size/2
            levelProgressSlider.value = Mathf.Lerp(levelProgressSlider.value, MathfUtil.Map(-playerLevelProgress, -initialDistance, 0, 0, 100), Time.deltaTime * lerpSpeed);
            //if ((-playerLevelProgress >= -initialDistance / 2) && !hasReachedHalfTheLevel)
            //{
            //    AdMobManager.Instance.ShowBanner();
            //    hasReachedHalfTheLevel = true;
            //}
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
            winScreen.GetComponent<Animator>().SetTrigger(parameterWonHash);
            winScreen.GetComponent<WinScreen>().SetWiningText();
        }
    }
    [HideInInspector]
    public int parameterWonHash = Animator.StringToHash("won");
}
