using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogueTxt;
    private Queue<string> sentences;
    private Queue<string> noHelping;
    public GameObject yesNo;
    public GameObject nextBtn;
    public Animator animator;
    public Animator ratonAnim;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        noHelping = new Queue<string>();

    }

    public void StartDialogue (Dialogue dialogue)
    {
        
        animator.SetBool("isOpen", true);
        nameTxt.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences) 
        { 
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //Debug.Log("Aquest es el resultat " + sentences.Count - 2);
        if(sentences.Count == 0) 
        {
            EndDialogue();
            return;
        } else if (sentences.Count == 4)
        {
            yesNo.SetActive(true);
            nextBtn.SetActive(false);
        } else
        {
            yesNo.SetActive(false);
            nextBtn.SetActive(true);
        }

        string sentence = sentences.Dequeue();
        dialogueTxt.text = sentence;
    }

    public void noHelp(Dialogue dialogue2)
    {
        noHelping.Clear();

        foreach (string noHelp in dialogue2.badSentences)
        {
            noHelping.Enqueue(noHelp);
        }

        nextNoHelp();
    }

    public void nextNoHelp()
    {
        yesNo.SetActive(false);
        nextBtn.SetActive(true);
        ratonAnim.SetBool("death", true);

        string sentence = noHelping.Dequeue();
        dialogueTxt.text = sentence;

        Invoke("EndDialogue", 5);
    }
    public void EndDialogue()
    {
        ratonAnim.SetBool("death", false);
        ratonAnim.SetBool("talking", false);
        animator.SetBool("isOpen", false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}