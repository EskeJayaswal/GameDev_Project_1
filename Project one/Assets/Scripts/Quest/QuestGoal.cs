using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool isReached()
    {
        if(goalType == GoalType.Kill)
        {
            return (currentAmount >= requiredAmount);
        }
        else if(goalType == GoalType.Gathering)
        {
            // TODO: Not implemented yet
            return false;
        }
        else if(goalType == GoalType.CheckPoint)
        {
            // TODO: Not implemented yet
            // Maybe add a sphear or a Gizmo to determind if we are inside the Check Point area.
            return false;
        }
        else if(goalType == GoalType.CountDown)
        {

            return (currentAmount >= requiredAmount);
        }
        else
            return false;
    }
    
}

public enum GoalType
{
    Kill,
    Gathering,
    CheckPoint,
    CountDown
}
