using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private List<string> sentences;
    public TextMeshProUGUI dialogueText;

    void Start()
    {
        sentences = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue...");
        sentences.Clear();

        foreach(var sentence in dialogue.sentences)
            sentences.Add(sentence);

        DisplaySentence(0);
        //DisplayNextSentence();

    }

    public void DisplaySentence(int index)
    {
        sentences.Clear();
        string sentence = sentences[index];
        dialogueText.text = sentence;
    }


    //public void DisplayNextSentence()
    //{
    //    if(sentences.Count == 0)
    //    {
    //        EndDialogue();
    //        return;
    //    }

    //    string sentence = sentences.Dequeue();
    //    dialogueText.text = sentence;

    //}

    private void EndDialogue()
    {
        Debug.Log("end conversation");
    }

}
