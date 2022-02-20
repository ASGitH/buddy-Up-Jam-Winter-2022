using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManagement : MonoBehaviour
{
    private GameObject playerGameObject;

    void Start() { playerGameObject = GameObject.FindGameObjectWithTag("Player"); }

    void Update() { if (playerGameObject != null && playerGameObject.transform.position.y < -25f) { SceneManager.LoadScene(0); } } 
}