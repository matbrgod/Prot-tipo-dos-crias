using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class QuestUIRevisado : MonoBehaviour
{
    public Transform questListContent;
    public GameObject questEntryPrefab;

    public List<Quest> currentPlayerQuests = new List<Quest>();
    private List<QuestProgress> currentQuestProgresses = new();//melhorar esses nomes ai rapazeada :O

    void Start()
    {
        InitializeQuestTab();
        UpdateQuestUI();
    }
    public void InitializeQuestTab()
    {
        for (int i = 0; i < currentPlayerQuests.Count; i++)
        {
            currentQuestProgresses.Add(new QuestProgress(currentPlayerQuests[i]));
        }
    }
    public void UpdateQuestUI()
    {
        for (int i = 0; i < currentPlayerQuests.Count; i++)
        {
            //script do prefab
            QuestEntry entry = Instantiate(questEntryPrefab, questListContent).GetComponent<QuestEntry>();//referenciado enquanto cria a copia
            //nome da quest
            entry.questName.text = currentQuestProgresses[i].quest.questName;
            entry.questDescription.text = currentQuestProgresses[i].quest.description;//descricao
            entry.currentObjective.text = currentQuestProgresses[i].quest.objectives[0].description;//objetivo atual
        }
    }
    public void AddQuestToList(Quest novaQuest) //entrega o scriptableObject da quest
    {
        currentPlayerQuests.Add(novaQuest);//Add pede pra lista acrescentar na
                                           //ultima posicao dela o objeto entre parenteses se ele for valido

        //script do prefab
        QuestEntry entry = Instantiate(questEntryPrefab, questListContent).GetComponent<QuestEntry>();//referenciado enquanto cria a copia
                                                                                                      //nome da quest
        entry.questName.text = novaQuest.questName;
        entry.questDescription.text = novaQuest.description;//descricao
        entry.currentObjective.text = novaQuest.objectives[0].description;//objetivo atual
    }
    public void RemoveQuestToList(Quest questVelha) //entrega o scriptableObject da quest
    {
        for (int i = 0; i < currentPlayerQuests.Count; i++) //jeito alternativo de percorrer a lista e procurar uma quest pra remover
        {
            if (currentPlayerQuests[i] == questVelha)
            {
                currentPlayerQuests.Add(questVelha);//Add pede pra lista acrescentar na
                                                    //ultima posicao dela o objeto entre parenteses se ele for valido
                Debug.Log("Quest concluida e removida da lista");
            }
        }
        if (currentPlayerQuests.Contains(questVelha))//pergunta se essa quest que foi concluida esta na lista
        {
            currentPlayerQuests.Add(questVelha);//Add pede pra lista acrescentar na
                                                //ultima posicao dela o objeto entre parenteses se ele for valido
            Debug.Log("Quest concluida e removida da lista");
        }
    }
}