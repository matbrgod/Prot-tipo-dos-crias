using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public string description;
    public List<QuestObjective> objectives;

    private void OnValidate()
    {
        if(string.IsNullOrEmpty(questID))
        {
            questID = questName+ Guid.NewGuid().ToString();
        }
    }
}
[Serializable]
public class QuestObjective
{
    public string objectiveID;
    public string description;
    public ObjectiveType type;
    public int requiredAmount;
    public int currentAmount;

    public bool IsCompleted => currentAmount >= requiredAmount;
}

public enum ObjectiveType { CollectItem, DefeatEnemy, ReachLocation, TalkNPC, Custom }

[Serializable]
public class QuestProgress //classe que acompanha o progresso da quest a qual ele pertence
{
    public Quest quest;
    public List<QuestObjective> objectives;

    public QuestProgress(Quest quest)
    {
        this.quest = quest;
        objectives = new List<QuestObjective>();

        foreach (var obj in quest.objectives)
        {
            objectives.Add(new QuestObjective
            {
                objectiveID = obj.objectiveID,
                description = obj.description,
                type = obj.type,
                requiredAmount = obj.requiredAmount,
                currentAmount = 0
            });
        }
    }

    public bool IsCompleted => objectives.TrueForAll(o => o.IsCompleted);

    public string QuestID => quest.questID;

    /*
     objectiveID = 0 -> madeira
     objectiveID = 1 -> pedra
     objectiveID = 2 -> suco de maca
     */
}
