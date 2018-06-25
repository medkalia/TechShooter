using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTimersBar : MonoBehaviour {

    [Tooltip("The cards timer container")]
    public Transform cardTimerContainer;
    [Tooltip("The cards timer prefab")]
    public GameObject cardTimerPrefab;

    private void Start()
    {
        
    }

    public void AddTimer(int cardId)
    {
        GameObject timerInstance =  Instantiate(cardTimerPrefab, cardTimerContainer);
        timerInstance.GetComponent<CardTimer>().SetTimer(DataController.Instance.GetCardService().GetCardById(cardId));
    }
}
