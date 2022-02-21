using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagement : MonoBehaviour
{
    void Start() { }

    void Update() { }

    public void loadScene(string _sceneName) { SceneManager.LoadScene(_sceneName); }
}