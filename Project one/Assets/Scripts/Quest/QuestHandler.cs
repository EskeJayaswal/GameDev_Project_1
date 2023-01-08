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


    void FinishQuest()
    {
        quest[currentQuest].isActive = false;
        quest[currentQuest].isComplete = true;
        // Pause the game and show next quest / objective
        
        if(quest[currentQuest].reward != null)
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
        string weaponName = quest[currentQuest].reward.GetWeapon().name;

        questReward.text =  "The " + weaponName + " is now unlocked";
        rewardInstructions.text = quest[currentQuest].reward.instructions;
        
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





    public Quest GetCurrentQuest()
    {
        return quest[currentQuest];
    }
}
