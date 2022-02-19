using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class dialogueResponse
{
    public dialogueScript _dialogueScript;

    public string responseText;
}

public class dialogueScript : MonoBehaviour
{
    [SerializeField] [TextArea] public string[] dialogue;

    public bool automaticallyPlay = false, hasResponses = false, isAffectedByDirector = false;

    public dialogueResponse[] responses;

    public float[] timeBetweenDialogueStrings;

    public PlayableDirector _playableDirector;
}