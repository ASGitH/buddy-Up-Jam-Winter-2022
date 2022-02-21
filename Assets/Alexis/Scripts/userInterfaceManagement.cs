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
        if(_dialogue != null)
        {
            dialogueUI.SetActive(true);

            StartCoroutine(referenceToDialogueManagement.stepThroughDialogue(_dialogue));
        }
        else { if (dialogueUI.activeSelf) { dialogueUI.SetActive(false); } }
    }
}