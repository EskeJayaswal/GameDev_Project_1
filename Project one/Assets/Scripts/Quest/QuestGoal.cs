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
        if(goalType == GoalType.CheckPoint)
        {
            // TODO: Not implemented yet
            // Maybe add a sphear or a Gizmo to determind if we are inside the Check Point area.
            return false;
        }

        return (currentAmount >= requiredAmount);
    }
    
}

public enum GoalType
{
    Kill,
    Gathering,
    CheckPoint
    

}
