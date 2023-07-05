using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private uint scene;

    public int sceneIndex => (int)scene;

    public void LoadScene() { SceneManager.LoadScene((int)scene); }
}
