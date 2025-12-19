using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public string levelToLoad;
    public void Restart()
    {
        GameManager.sceneLoaded = true;
        SceneManager.LoadScene(levelToLoad);
    }
}
