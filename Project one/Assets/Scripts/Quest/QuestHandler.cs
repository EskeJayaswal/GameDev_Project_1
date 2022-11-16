using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    public Quest quest;

    private GameObject activeQuestHUD;
    private TextMeshProUGUI questName;
    private TextMeshProUGUI questGoal;
    private TextMeshProUGUI goalCounter;

    void Start()
    {
        activeQuestHUD = GameObject.Find("ActiveQuest");
        activeQuestHUD.SetActive(true);

        questName = activeQuestHUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        questGoal = activeQuestHUD.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        goalCounter = activeQuestHUD.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(quest.isActive)
        {
            
            GetQuestInstructions();

            if(quest.goal.isReached())
                FinishQuest();
        }
        else
        {
            activeQuestHUD.SetActive(false);
        }

    }

    void GetQuestInstructions()
    {
        // Quest display in the top left corner
        questName.text = quest.title;
        questGoal.text = quest.description;
            
        switch(quest.goal.goalType) 
        {
        case GoalType.Kill:
            goalCounter.text = "Kill: " + quest.goal.currentAmount + " | " + quest.goal.requiredAmount;
            break;
        case GoalType.Gathering:
            goalCounter.text = "NAN";
            break;
        case GoalType.CheckPoint:
            goalCounter.text = "NAN";
            break;
        }
    }

    void FinishQuest()
    {
        quest.isActive = false;
        // Pause the game and show next quest / objective
        Debug.Log("ADD MORE QUESTS!");
    }



}
