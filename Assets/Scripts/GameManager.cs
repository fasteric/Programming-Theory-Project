using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // single source of truth target frame rate
    public static int targetFrameRate => Application.targetFrameRate;

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
