using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public Stat health;
    //public Stat energy

    // Singleton
    public static PlayerStats instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health.curValue = health.startValue;
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
        Debug.Log("Player is dead");
        //SceneManager.LoadScene("Menu");
    }

}


[System.Serializable]
public class Stat
{
    [HideInInspector]
    public float curValue;
    public float maxValues;
    public float startValue;
    public float regenRate;  // Maybe implement a small regen per sec
    public Image uiBar;


    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValues);
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
