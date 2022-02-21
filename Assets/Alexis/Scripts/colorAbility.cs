using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum colorAbilities { bridge, jump, ladder, _null };
public enum colorAbilityActions { climb, _null, pickup, use };

public class colorAbility : MonoBehaviour
{
    // Color will be set in the inspector
    // What type of ability should it give (dropdown of strings), color to display (see note above), when the player is in the area and picks it up.
    #region Private
    #endregion

    #region Public
    public colorAbilities _colorAbility = new colorAbilities();

    public colorAbilityActions _colorAbilityActions = new colorAbilityActions();

    public GameObject objectToAppear;
    #endregion

    void Start() 
    {
        if (_colorAbilityActions.ToString() == "use")
        {
            GetComponent<SpriteRenderer>().enabled = false;

            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }

        if (objectToAppear != null && objectToAppear.activeSelf) { objectToAppear.SetActive(false); }
    }

    void Update() { }

    private void OnTriggerExit2D(Collider2D collision) 
    { 
        if (collision.tag == "Player") 
        { 
            if (_colorAbilityActions.ToString() == "climb" && Input.GetAxisRaw("Horizontal") == 0) 
            {
                collision.gameObject.GetComponent<playerController>().changeMovementState("move");

                GetComponent<EdgeCollider2D>().enabled = true; 
            }
            if (_colorAbilityActions.ToString() == "use") { transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false; }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (_colorAbilityActions.ToString() == "use") { transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true; }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_colorAbilityActions.ToString() == "pickup")
                {
                    collision.gameObject.GetComponent<playerController>().colorsAcquired.Add(_colorAbility.ToString());

                    gameObject.SetActive(false);
                }
                else if (_colorAbilityActions.ToString() == "use")
                {
                    foreach (string _color in collision.gameObject.GetComponent<playerController>().colorsAcquired)
                    {
                        if (_color == _colorAbility.ToString())
                        {
                            if (objectToAppear != null) { objectToAppear.SetActive(true); }

                            break;
                        }
                    }
                }
            }
            else if (Input.GetAxisRaw("Vertical") != 0) 
            { 
                if (_colorAbilityActions.ToString() == "climb") 
                { 
                    collision.gameObject.GetComponent<playerController>().changeMovementState("climb"); 
                
                    if(GetComponent<EdgeCollider2D>().enabled /*&& Input.GetAxisRaw("Vertical") < 0*/) { GetComponent<EdgeCollider2D>().enabled = false; }
                } 
            }
        }
    }
}