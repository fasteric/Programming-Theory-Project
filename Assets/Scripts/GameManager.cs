using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // mathematical constant tau = 2 * pi
    public static readonly float TAU = 2f * Mathf.PI;

    // single source of truth target frame rate
    public static int targetFrameRate => Application.targetFrameRate;
    public static float targetFrameRateFloat => targetFrameRate;

    // single source of truth delta time
    public static float deltaTime => 1f / targetFrameRate;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Simulate(deltaTime);
    }
}
