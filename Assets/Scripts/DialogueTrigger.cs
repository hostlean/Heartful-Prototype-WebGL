using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject dialogueManager;


    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogue);
    }
}
