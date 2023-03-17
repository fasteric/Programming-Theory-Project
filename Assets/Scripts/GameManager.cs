using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float deltaTime => 1f / targetFrameRate; // single source of truth delta time
    public static float targetFrameRateFloat => targetFrameRate; // float version of targetFrameRate
    public static int targetFrameRate => Application.targetFrameRate; // single source of truth target frame rate
    public static readonly float TAU = 2f * Mathf.PI; // mathematical constant tau = 2 * pi

    [SerializeField] private int targetFrameRateFixed = 60; // target frame rate, set in editor
    private static GameManager instance = null; // singleton instance

    private void Awake()
    {
        if (instance) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = targetFrameRateFixed;
    }

    private void Update()
    {
        Physics.Simulate(deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);
    }
}
