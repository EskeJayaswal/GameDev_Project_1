using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestHandler : MonoBehaviour
{


    public Quest[] quest;
    public int currentQuest = 0;
    [SerializeField]
    private GameObject activeQuestHUD;
    private TextMeshProUGUI questName;
    private TextMeshProUGUI questGoal;
    private TextMeshProUGUI goalCounter;


    // Window that pops up when we finish a quest.
    [SerializeField]
    private GameObject questScreen;
    [SerializeField]
    private TextMeshProUGUI questReward;
    [SerializeField]
    private TextMeshProUGUI rewardInstructions;
    [SerializeField]
    private float secondsShowingUI;
    private bool UIHidden;

    // Quests with a timer
    private float countDown;
    private bool timerStarted;

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

        if(quest[currentQuest].goal.goalType == GoalType.CountDown)
        {
            // Starts the countdown with required amount of seconds from the quest handler.
            if(!timerStarted)
            {
                countDown = quest[currentQuest].goal.requiredAmount;
                timerStarted = true;
            }

            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
                quest[currentQuest].goal.requiredAmount = 0;

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
        case GoalType.CountDown:
            goalCounter.text = "Time left: " + GetConvertedTime();
            break;
        }
    }


    void FinishQuest()
    {
        quest[currentQuest].isActive = false;
        quest[currentQuest].isComplete = true;
        // Pause the game and show next quest / objective
        
        if(quest[currentQuest].reward.GetWeapon() != null)
        {
            quest[currentQuest].reward.GetWeapon().UnlockWeapon();
        }

        // Small UI screen that pops up for 3 seconds.
        ShowQuestReward();

        if (quest.Length > currentQuest + 1)
        {
            currentQuest++;
            quest[currentQuest].isActive = true;
        }

        Debug.Log("Quest complete");
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
