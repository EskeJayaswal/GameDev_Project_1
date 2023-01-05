using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    public Quest[] quest;
    public int currentQuest = 0;
    public GameObject activeQuestHUD;
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

        if(quest[currentQuest].isActive)
        {
            
            GetQuestInstructions();

            if(quest[currentQuest].goal.isReached())
            {
                FinishQuest();
            }
        }
        else
        {
            activeQuestHUD.SetActive(false);
        }

    }

    void GetQuestInstructions()
    {
        // Quest display in the top left corner
        questName.text = quest[currentQuest].title;
        questGoal.text = quest[currentQuest].description;
            
        switch(quest[currentQuest].goal.goalType) 
        {
        case GoalType.Kill:
            goalCounter.text = "Kill: " + quest[currentQuest].goal.currentAmount + " | " + quest[currentQuest].goal.requiredAmount;
            break;
        case GoalType.Gathering:
            goalCounter.text = "NAN";
            break;
        case GoalType.CheckPoint:
            goalCounter.text = "NAN";
            break;
        }
    }

    public Quest GetCurrentQuest()
    {
        return quest[currentQuest];
    }

    void FinishQuest()
    {
        quest[currentQuest].isActive = false;
        quest[currentQuest].isComplete = true;
        // Pause the game and show next quest / objective
        
        if(quest[currentQuest].reward != null)
        {
            quest[currentQuest].reward.UnlockWeapon();
        }



        if (quest.Length > currentQuest + 1)
        {
            currentQuest++;
            quest[currentQuest].isActive = true;
        }

        Debug.Log("Quest complete");
    }
}
