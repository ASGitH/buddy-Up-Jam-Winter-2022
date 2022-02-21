using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum actions { _null, dialogue, director };

public class interactable : MonoBehaviour
{
    #region Private
    private GameObject interactableIcon;

    private userInterfaceManagement referenceToUserInterfaceManagement;
    #endregion

    #region Public
    public actions _action = new actions();

    public dialogueScript _dialogue;

    public PlayableDirector director;
    #endregion

    void Start() 
    {
        interactableIcon = transform.GetChild(0).gameObject;

        referenceToUserInterfaceManagement = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<userInterfaceManagement>();
    }

    void Update() { }

    private void OnTriggerEnter2D(Collider2D collision) { if(collision.tag == "Player") { interactableIcon.SetActive(true); } }
    private void OnTriggerExit2D(Collider2D collision) { if (collision.tag == "Player") { interactableIcon.SetActive(false); } }
    private void OnTriggerStay2D(Collider2D collision) 
    { 
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E)) 
        { 
            switch (_action.ToString()) 
            { 
                case "dialogue":
                    collision.GetComponent<playerController>().isAbleToJump = false; collision.GetComponent<playerController>().isAbleToMove = false;

                    referenceToUserInterfaceManagement.displayDialogueUserInterface(_dialogue); 
                    break;
                case "director": 
                    collision.GetComponent<playerController>().isAbleToJump = false; collision.GetComponent<playerController>().isAbleToMove = false;

                    director.Play(); 
                    break;
            } 
        } 
    }
}