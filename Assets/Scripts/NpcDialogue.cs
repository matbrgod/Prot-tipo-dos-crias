using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NpcDialogue : MonoBehaviour
{
    public string[] dialogueNpc;
    public string npcName;
    public int dialogueIndex;

    public GameObject dialoguePanel;
    public Text dialogueText;

    public Text nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;

    public bool readyToSpeak;
    public bool startDialogue;
    public GameObject hudArma;
    public GameObject hudVida;
    public GameObject armaPlayer;
    public GameObject questOnHud;
    public GameObject questOffHud;
    public Animator painelAnimator;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && readyToSpeak)
        {
            if (!startDialogue)
            {
                FindObjectOfType<Player>().moveSpeed = 0f;
                StartDialogue();
            }
            else if (dialogueText.text == dialogueNpc[dialogueIndex])
            {
                NextDialogue();
            }
            if (painelAnimator != null)
            {
                painelAnimator.SetBool("Ativar", true);
            }
        }
        void NextDialogue()
        {
            dialogueIndex++;
            if (dialogueIndex < dialogueNpc.Length)
            {
                StartCoroutine(ShowDialogue());
            }
            else
            {
                dialoguePanel.SetActive(false);
                startDialogue = false;
                dialogueIndex = 0;
                FindObjectOfType<Player>().moveSpeed = 5f;

            }
        }

        void StartDialogue()
        {
            nameNpc.text = npcName;
            imageNpc.sprite = spriteNpc;
            startDialogue = true;
            dialogueIndex = 0;
            dialoguePanel.SetActive(true);
            FindObjectOfType<Player>().moveSpeed = 0f;

            StartCoroutine(ShowDialogue());
        }

        IEnumerator ShowDialogue()
        {
            dialogueText.text = "";
            foreach (char letter in dialogueNpc[dialogueIndex])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = true;

            if(hudArma != null)
               hudArma.SetActive(false);

            if (armaPlayer != null)
                armaPlayer.SetActive(false);

            hudVida.SetActive(false);

            if (questOffHud != null)
                questOffHud.SetActive(false);
            
            var player = FindObjectOfType<Player>();
            if (player != null) player.canAttack = false;
                        
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = false;

            if(hudArma != null)
               hudArma.SetActive(true);

            if (armaPlayer != null)
                armaPlayer.SetActive(true);

            hudVida.SetActive(true);

            if (questOnHud != null)
                questOnHud.SetActive(true);

            if(painelAnimator != null)
            {
                painelAnimator.SetBool("Ativar", false);
                painelAnimator.SetBool("Desativar", true);
            }
            
            //var player = FindObjectOfType<Player>();
            //if (player != null) player.canAttack = true;
        }
    }
}
