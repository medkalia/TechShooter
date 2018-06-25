using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsLogic : MonoBehaviour {

    [HideInInspector]
    public static CardsLogic Instance { get; set; }
    private EffectsChecker checker = new EffectsChecker();


    private void Start()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void AttachCardEffect(Button cardButton, Card card)
    {
        switch (card.Slug)
        {
            case "card_power_jump":
                cardButton.onClick.AddListener(delegate { PowerJumpEffect(cardButton,card); });
                break;
            case "card_rafale":
                cardButton.onClick.AddListener(delegate { RafaleEffect(cardButton, card); });
                break;
            case "card_heal":
                cardButton.onClick.AddListener(delegate { HealEffect(cardButton, card); });
                break;
            case "card_haste":
                cardButton.onClick.AddListener(delegate { HasteEffect(cardButton, card); });
                break;
            case "card_overcharge":
                cardButton.onClick.AddListener(delegate { OverchargeEffect(cardButton, card); });
                break;
            case "card_regenerate":
                cardButton.onClick.AddListener(delegate { RegenerateEffect(cardButton, card); });
                break;
            case "card_power_skin":
                cardButton.onClick.AddListener(delegate { PowerSkinEffect(cardButton, card); });
                break;
            case "card_tech_rush":
                cardButton.onClick.AddListener(delegate { TechRushEffect(cardButton, card); });
                break;
            case "card_tech_urge":
                cardButton.onClick.AddListener(delegate { TechUrgeEffect(cardButton, card); });
                break;
            case "card_barrier":
                cardButton.onClick.AddListener(delegate { BarrierEffect(cardButton, card); });
                break;
            default:
                Debug.Log("No effect attached to the card "+ card.Slug);
                break;
        }
        
    }

    //**************************************PowerJump**************************************
    public void PowerJumpEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            Player.Instance.StartCoroutine(PowerJump(card.EffectDuration, card.EffectModifier));
            CardStackView.Instance.Replace(cardButton.gameObject);
        }
        
    }
    IEnumerator PowerJump(float effectDuration, float effectModifier)
    {
        yield return new WaitWhile(() => checker.powerJumpIsOn);
        checker.powerJumpIsOn = true;
        Player.Instance.playerStats.jumpVelocity = effectModifier;
        yield return new WaitForSeconds(effectDuration);
        Player.Instance.playerStats.jumpVelocity = Player.Instance.data.jumpVelocity;
        checker.powerJumpIsOn = false;
    }
    /*------------------------------------------------------------------------------------------------------------------*/

    //**************************************Rafale**************************************
    public void RafaleEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            Player.Instance.StartCoroutine(Rafale(card.EffectDuration,card.EffectModifier));
            CardStackView.Instance.Replace(cardButton.gameObject);
        }
            
    }
    IEnumerator Rafale(float effectDuration, float effectModifier)
    {
        yield return new WaitWhile(() => checker.rafaleIsOn);
        checker.rafaleIsOn = true;
        Player.Instance.playerStats.fireRate = effectModifier;
        yield return new WaitForSeconds(effectDuration);
        Player.Instance.playerStats.fireRate = Player.Instance.data.fireRate;
        checker.rafaleIsOn = false;
    }
    /*------------------------------------------------------------------------------------------------------------------*/

    //**************************************Heal**************************************
    public void HealEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentHealth < Player.Instance.playerStats.baseHealth && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            Player.Instance.playerStats.currentHealth = Mathf.Clamp(Player.Instance.playerStats.currentHealth + Player.Instance.playerStats.baseHealth * card.EffectModifier,0 , Player.Instance.playerStats.baseHealth);
            CardStackView.Instance.Replace(cardButton.gameObject);
        }
            

    }
    /*------------------------------------------------------------------------------------------------------------------*/

    //**************************************Haste**************************************
    public void HasteEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            
            Player.Instance.StartCoroutine(Haste(card.EffectDuration, card.EffectModifier));
            CardStackView.Instance.Replace(cardButton.gameObject);
        }

    }
    IEnumerator Haste(float effectDuration, float effectModifier)
    {
        yield return new WaitWhile(() => checker.hasteIsOn);
        checker.hasteIsOn = true;
        Player.Instance.playerStats.maxSpeed = effectModifier;
        Player.Instance.projectileStats.projectileSpeed *= 1.5f;
        yield return new WaitForSeconds(effectDuration);
        Player.Instance.playerStats.maxSpeed = Player.Instance.data.maxSpeed;
        Player.Instance.projectileStats.projectileSpeed = Player.Instance.data.projectileSpeed;
        checker.hasteIsOn = false;
    }
    /*------------------------------------------------------------------------------------------------------------------*/

    //**************************************Overcharge**************************************
    public void OverchargeEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            Player.Instance.StartCoroutine(Overcharge(card.EffectDuration, card.EffectModifier));
            CardStackView.Instance.Replace(cardButton.gameObject);
        }
    }
    IEnumerator Overcharge(float effectDuration, float effectModifier)
    {
        yield return new WaitWhile(() => checker.overchargeIsOn);
        checker.overchargeIsOn = true;
        Player.Instance.projectileStats.projectileBaseDamage += effectModifier * Player.Instance.projectileStats.projectileBaseDamage;
        yield return new WaitForSeconds(effectDuration);
        Player.Instance.projectileStats.projectileBaseDamage = Player.Instance.data.projectileBaseDamage;
        checker.overchargeIsOn = false;
    }
    /*------------------------------------------------------------------------------------------------------------------*/

    //**************************************Regenerate**************************************
    public void RegenerateEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            Player.Instance.StartCoroutine(Regenerate(card.EffectDuration, card.EffectModifier));
            CardStackView.Instance.Replace(cardButton.gameObject);
        }

    }
    private IEnumerator Regenerate(float effectDuration, float effectModifier)
    {
        Player.Instance.playerInfo.isRegenHealth = true;
        int i = 0;
        while (i < effectDuration)
        {
            
            i++;
            if (Player.Instance.playerStats.currentHealth < Player.Instance.playerStats.baseHealth )
                Player.Instance.playerStats.currentHealth += effectModifier;
            yield return new WaitForSeconds(1);
        }
        Player.Instance.playerInfo.isRegenHealth = false;
    }
    /*------------------------------------------------------------------------------------------------------------------*/

    //**************************************PowerSkin**************************************
    public void PowerSkinEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            Player.Instance.StartCoroutine(PowerSkin(card.EffectDuration, card.EffectModifier));
            CardStackView.Instance.Replace(cardButton.gameObject);
        }
    }
    IEnumerator PowerSkin(float effectDuration, float effectModifier)
    {
        yield return new WaitWhile(() => checker.powerSkinIsOn);
        checker.powerSkinIsOn = true;
        Player.Instance.playerStats.recievedDamageAmplifier = effectModifier * Player.Instance.playerStats.recievedDamageAmplifier;
        yield return new WaitForSeconds(effectDuration);
        Player.Instance.playerStats.recievedDamageAmplifier = Player.Instance.data.recievedDamageAmplifier;
        checker.powerSkinIsOn = false;
    }
    /*------------------------------------------------------------------------------------------------------------------*/

    //**************************************TechPoints**************************************
    public void TechRushEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentTechPoints < Player.Instance.playerStats.maxTechPoints && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            Player.Instance.playerStats.currentTechPoints = Mathf.Clamp(Player.Instance.playerStats.currentTechPoints + card.EffectModifier, 0, Player.Instance.playerStats.maxTechPoints);
            CardStackView.Instance.Replace(cardButton.gameObject);
        }


    }
    /*------------------------------------------------------------------------------------------------------------------*/

    //**************************************TechUrge**************************************
    public void TechUrgeEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            Player.Instance.StartCoroutine(TechUrge(card.EffectDuration, card.EffectModifier));
            CardStackView.Instance.Replace(cardButton.gameObject);
        }
    }
    IEnumerator TechUrge(float effectDuration, float effectModifier)
    {
        yield return new WaitWhile(() => checker.techUrgeIsOn);
        checker.techUrgeIsOn = true;
        Player.Instance.playerStats.techPointsRegenRate = effectModifier;
        yield return new WaitForSeconds(effectDuration);
        Player.Instance.playerStats.techPointsRegenRate = Player.Instance.data.techPointsRegenRate;
        checker.techUrgeIsOn = false;
    }
    /*------------------------------------------------------------------------------------------------------------------*/

    //**************************************Barrier**************************************
    public void BarrierEffect(Button cardButton, Card card)
    {
        if (Player.Instance.playerStats.currentTechPoints >= card.TechPointCost && Player.Instance.playerStats.currentHealth > 0)
        {
            Player.Instance.playerStats.currentTechPoints -= card.TechPointCost;
            Player.Instance.StartCoroutine(Barrier(card.EffectDuration));
            CardStackView.Instance.Replace(cardButton.gameObject);
        }
    }
    IEnumerator Barrier(float effectDuration)
    {
        yield return new WaitWhile(() => checker.barrierIsOn);
        checker.barrierIsOn = true;
        Player.Instance.barrier.SetActive(true);
        yield return new WaitForSeconds(effectDuration);
        Player.Instance.barrier.SetActive(false);
        checker.barrierIsOn = false;
    }
    /*------------------------------------------------------------------------------------------------------------------*/
    private class EffectsChecker
    {
        public bool rafaleIsOn = false;
        public bool powerJumpIsOn = false;
        public bool hasteIsOn = false;
        public bool overchargeIsOn = false;
        public bool powerSkinIsOn = false;
        public bool techUrgeIsOn = false;
        public bool barrierIsOn = false;
    }
    

}
