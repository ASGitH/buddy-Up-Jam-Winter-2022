using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum dialogueActions { _null, sceneTransition };

[System.Serializable]
public class dialogueResponse
{
    public dialogueActions _action = new dialogueActions();

    public dialogueScript _dialogueScript;

    public string responseText;
    public string transitionToScene;
}

public class dialogueScript : MonoBehaviour
{
    [SerializeField] [TextArea] public string[] dialogue;

    public bool automaticallyPlay = false, hasResponses = false, isAffectedByDirector = false;

    public dialogueResponse[] responses;

    public float[] timeBetweenDialogueStrings;

    public int currentLine = 0, previousLine = -1;
    public int[] exitDirectorAtLineIndex;

    public PlayableDirector _playableDirector;
}