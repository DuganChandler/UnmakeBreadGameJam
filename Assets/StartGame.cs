using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string leveName;
    public void LoadLevel() {
        SceneManager.LoadScene(leveName);
    }

    public void Exit() {
        Application.Quit();
    }
}
