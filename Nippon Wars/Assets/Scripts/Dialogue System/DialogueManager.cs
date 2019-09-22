using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialog Text Settings")]
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;
    private bool isEnemy;
    public bool dialogAble;


    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;
        isEnemy = dialogue.isCombat;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            dialogAble = false;
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        dialogAble = true;
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        if (isEnemy)
        {
            FWmanager.Instance.enemyDialog = true;
        }
        else
        {
            FWmanager.Instance.DialogStart = false;
            dialogAble = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogAble)
        {
            DisplayNextSentence();
        }
    }
}