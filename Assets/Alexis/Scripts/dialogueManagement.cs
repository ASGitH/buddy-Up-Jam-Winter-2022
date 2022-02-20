using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dialogueManagement : MonoBehaviour
{
    #region Private
    private dialogueResponseHandler referenceToDialogueResponseHandler;

    private userInterfaceManagement referenceToUserInterfaceManagement;
    #endregion

    #region Public
    // public dialogueScript _dialogueScript;

    public float charBufferSpeed = 0f;
    #endregion

    void Start()
    {
        charBufferSpeed = 50f;

        referenceToDialogueResponseHandler = GetComponent<dialogueResponseHandler>();

        referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>();
    }

    private IEnumerator typeText(string _textToType, TMP_Text _textLabel)
    {
        _textLabel.text = string.Empty;

        float t = 0f;
        int charIndex = 0;

        while (charIndex < _textToType.Length)
        {
            t += Time.deltaTime * charBufferSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, _textToType.Length);

            if (Input.GetKeyDown(KeyCode.Space)) { break; }

            _textLabel.text = _textToType.Substring(0, charIndex);

            yield return null;
        }

        _textLabel.text = _textToType;
    }

    private void closeDialogueScript(dialogueScript _dialogue)
    {
        referenceToUserInterfaceManagement.dialogueUI.SetActive(false);

        referenceToUserInterfaceManagement.dialogueText.text = string.Empty;

        //GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().isAbleToMove = true;
    }

    public Coroutine run(string _textToType, TMP_Text _textLabel) { return StartCoroutine(typeText(_textToType, _textLabel)); }

    public IEnumerator stepThroughDialogue(dialogueScript _dialogue)
    {
        referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>();

        for (int indexCounter = /*0*/_dialogue.previousLine + 1; indexCounter < _dialogue.dialogue.Length; indexCounter++)
        {
            string dialogue = _dialogue.dialogue[indexCounter];

            _dialogue.currentLine = indexCounter;

            yield return run(dialogue, referenceToUserInterfaceManagement.dialogueText);

            if (indexCounter == _dialogue.dialogue.Length - 1) { break; }

            if (!_dialogue.automaticallyPlay) { yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); }
            else { yield return new WaitForSeconds(_dialogue.timeBetweenDialogueStrings[indexCounter]); }

            if (_dialogue.isAffectedByDirector) { foreach (int _line in _dialogue.exitDirectorAtLineIndex) { if (_line == _dialogue.currentLine) { indexCounter = _dialogue.dialogue.Length; } } }
        }

        _dialogue.previousLine = _dialogue.currentLine;

        if (!_dialogue.hasResponses) 
        {
            if (!_dialogue.automaticallyPlay) { yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); closeDialogueScript(_dialogue); } 
            else { yield return new WaitForSeconds(_dialogue.timeBetweenDialogueStrings[_dialogue.timeBetweenDialogueStrings.Length - 1]); closeDialogueScript(_dialogue); }

            if (_dialogue.isAffectedByDirector) { _dialogue._playableDirector.Play(); }
        }
        else { if (!referenceToDialogueResponseHandler.hasShownResponses) { referenceToDialogueResponseHandler.showResponses(_dialogue.responses); } }

        if (!_dialogue.automaticallyPlay) { yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); }
    }
}