using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum colorAbilities { bridge, jump, ladder, _null };
public enum colorAbilityActions { _null, pickup, use };

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

    void Start() { if (objectToAppear.activeSelf) { objectToAppear.SetActive(false); } }

    void Update() { }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            if(_colorAbilityActions.ToString() == "pickup") 
            {
                collision.gameObject.GetComponent<playerController>().colorsAcquired.Add(_colorAbility.ToString());

                gameObject.SetActive(false);
            }
            else if (_colorAbilityActions.ToString() == "use")
            {
                foreach(string _color in collision.gameObject.GetComponent<playerController>().colorsAcquired)
                {
                    if(_color == _colorAbility.ToString()) 
                    {
                        if (objectToAppear != null) { objectToAppear.SetActive(true); }

                        break;
                    }
                }
            }
        }
    }
}
