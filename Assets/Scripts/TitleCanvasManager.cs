using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCanvasManager : MonoBehaviour
{
    [SerializeField] private int gameSceneBuildIndex = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneBuildIndex);
    }
}
