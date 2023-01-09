using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    public Quest[] quest;
    public int currentQuest = 0;
    
    [Header("Quest Container")]
    [SerializeField]
    private GameObject activeQuestHUD;
    [SerializeField]
    private TextMeshProUGUI questName, questGoal, goalCounter;

    // Window that pops up when we finish a quest.
    [Header("Quest Conclusion")]
    [SerializeField]
    private GameObject questScreen;
    [SerializeField]
    private TextMeshProUGUI questReward, rewardInstructions;    
    [SerializeField]
    private float secondsShowingUI;
    private bool UIHidden;

    // Quests with a timer
    private float countDown;
    private bool timerStarted;

    void Start()
    {
        activeQuestHUD.SetActive(true);
    }

    void Update()
    {
        if(quest[currentQuest].isActive)
        {
            // Updates the QuestContainer
            GetQuestInstructions();

            if(quest[currentQuest].goal.isReached())
                FinishQuest();
        }
        else
            // Deactivate if there is no active quests
            activeQuestHUD.SetActive(false);


        if(quest[currentQuest].goal.goalType == GoalType.CountDown)
           CountDownQuestGoal();

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
        case GoalType.CountDown:
            goalCounter.text = "Time left: " + GetConvertedTime(); // 120 seconds = 2:00 etc
            break;
        case GoalType.Gathering:
            goalCounter.text = "NAN";
            break;
        case GoalType.CheckPoint:
            goalCounter.text = "NAN";
            break;
        }
    }

    void CountDownQuestGoal()
    {
        // Starts the countdown with required amount of seconds from the quest handler.
        if(!timerStarted)
        {
            // Sets the count down float to the 
            countDown = quest[currentQuest].goal.requiredAmount;
            timerStarted = true;
        }

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        } 
        else
        {
            // This triggers the quest completion.
            quest[currentQuest].goal.requiredAmount = 0;
            timerStarted = false;
        }
    }


    void FinishQuest()
    {
        quest[currentQuest].isActive = false;
        quest[currentQuest].isComplete = true;
        
        // Checks for rewards
        if(quest[currentQuest].reward.GetWeapon() != null)
        {
            quest[currentQuest].reward.GetWeapon().UnlockWeapon();
        }

        // Small UI screen that pops up for 5 seconds.
        ShowQuestReward();

        if (quest.Length > currentQuest + 1)
        {
            currentQuest++;
            quest[currentQuest].isActive = true;
        }
    }


    void ShowQuestReward()
    {
        if(quest[currentQuest].reward.GetWeapon() != null)
        {

        string weaponName = quest[currentQuest].reward.GetWeapon().name;

        questReward.text =  "The " + weaponName + " is now unlocked";
        rewardInstructions.text = quest[currentQuest].reward.instructions;
        }
        else
        {
            questReward.text = "Good job!";
            rewardInstructions.text = "";
        }
        
        HideUI();
        Invoke("HideUI", secondsShowingUI);
        
    }


    void HideUI()
    {
        UIHidden = !UIHidden;
        if(UIHidden)
        {
            questScreen.SetActive(true);
        }
        else 
        {
            questScreen.SetActive(false);
        }
    }

    private string GetConvertedTime()
    {
        string minituesLeft = Mathf.FloorToInt(countDown / 60).ToString();
        string seconds = (countDown % 60).ToString("F0");
        seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;

        return minituesLeft + ":" + seconds;
    } 

    public Quest GetCurrentQuest()
    {
        return quest[currentQuest];
    }
}
