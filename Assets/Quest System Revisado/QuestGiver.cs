using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest questToGive; 
    public Quest questPraCompletar;
    public QuestUIRevisado questUIReference;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            questUIReference.AddQuestToList(questToGive);
            //se concluir a quest
            questUIReference.RemoveQuestToList(questPraCompletar);
        }
    }
}
