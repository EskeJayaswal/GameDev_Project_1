using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public bool isActive;
    public bool isComplete;

    public string title;
    public string description;

    public QuestGoal goal;
    public QuestReward reward;
}
