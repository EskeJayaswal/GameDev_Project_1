using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Stat health;
    [SerializeField]
    private Stat shotsFired;
    [SerializeField]
    private Stat hits;
    [SerializeField]
    private Stat kills;

    // QuestHandler
    [SerializeField]
    private QuestHandler questHandler;

    // Singleton
    public static PlayerStats instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health.curValue = health.startValue;
        shotsFired.curValue = shotsFired.startValue;
        hits.curValue = hits.startValue;
        kills.curValue = kills.startValue;

        questHandler = GameObject.Find("QuestHandler").GetComponent<QuestHandler>();
    }

    void Update()
    {
        // check if player is dead
        if(health.curValue == 0.0f)
        {
            Die();
        }

        // Update UI bars
        health.uiBar.fillAmount = health.GetPercentage();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    
    public void TakePhysicalDamage(float amount)
    {
        health.Substract(amount);
        //onTakeDamage?.Invoke();
    }

    public void Die()
    {
        //Debug.Log("Player is dead");
        SceneManager.LoadScene("Menu");
    }

    public void AddToStat(string stat, float amount)
    {
        switch(stat) 
        {
        case "shot":
            shotsFired.Add(amount);
            shotsFired.counter.text = string.Format("Shots: {0}", shotsFired.curValue.ToString());
            break;
        case "hit":
            hits.Add(amount);
            hits.counter.text = string.Format("Hits: {0}", hits.curValue.ToString());
            break;
        case "kill":
            kills.Add(amount);
            kills.counter.text = string.Format("Kills: {0}", kills.curValue.ToString());
            if(questHandler.GetCurrentQuest().goal.goalType == GoalType.Kill && !questHandler.GetCurrentQuest().goal.isReached())
            {
                questHandler.GetCurrentQuest().goal.currentAmount += 1;
            }
            break;
        }
    }
}


[System.Serializable]
public class Stat
{
    [HideInInspector]
    public float curValue;
    public float maxValues;
    public float startValue;
    public Image uiBar;
    public TextMeshProUGUI  counter;


    public void Add(float amount)
    {
        if(maxValues != 0)
            curValue = Mathf.Min(curValue + amount, maxValues);
        else
            curValue = curValue + amount;
    }

    public void Substract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValues;
    }

}

public interface IDamagable
{
    void TakePhysicalDamage(float damageAmount);
}
