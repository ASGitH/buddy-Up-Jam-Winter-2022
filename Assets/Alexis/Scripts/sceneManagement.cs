using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagement : MonoBehaviour
{
    private GameObject playerGameObject;

    void Start() { playerGameObject = GameObject.FindGameObjectWithTag("Player"); }

    void Update() { if (playerGameObject != null && playerGameObject.transform.position.y < -25f) { SceneManager.GetActiveScene(); } }

    public void hideGameObject(GameObject _gameObject) { _gameObject.SetActive(false); }

    public void loadScene(string _sceneName) { SceneManager.LoadScene(_sceneName); }

    public void quitApplication() { Application.Quit(); }

    public void showGameObject(GameObject _gameObject) { _gameObject.SetActive(true); }
}