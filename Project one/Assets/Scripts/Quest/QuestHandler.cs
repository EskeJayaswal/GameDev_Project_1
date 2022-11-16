using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    public Quest quest;

    public GameObject activeQuestHUD;
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questGoal;
    public TextMeshProUGUI goalCounter;

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
            questName.text = quest.title;
            questGoal.text = quest.description;
            goalCounter.text = GetCounterText();
        }
        else
        {
            activeQuestHUD.SetActive(false);
        }

    }

    private string GetCounterText()
    {
        return "Kill: " + quest.goal.currentAmount + " | " + quest.goal.requiredAmount;
    }



}
