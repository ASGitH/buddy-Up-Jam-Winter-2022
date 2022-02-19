using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class userInterfaceManagement : MonoBehaviour
{
    #region Public
    public dialogueManagement referenceToDialogueManagement;

    public GameObject dialogueUI;

    public TMP_Text dialogueText;
    #endregion

    void Start() { }

    void Update() { }

    public void displayDialogueUserInterface(dialogueScript _dialogue)
    {
        dialogueUI.SetActive(true);

        // GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = false;

        StartCoroutine(referenceToDialogueManagement.stepThroughDialogue(_dialogue));
    }
}