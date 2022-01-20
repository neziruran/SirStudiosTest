using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    public enum Scenes
    {
        Start,
        GameScene,
        End,
    }

    public Scenes sceneToLoad;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

}
