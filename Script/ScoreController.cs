using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public void Initialize()
    {
        GameController.gameScore = 0.0f;
    }

    public void SetScore(float time)
    {
        // Getting maxTime from <TimeController>
        float maxTime = GetComponent<TimeController>().maxTime;
        GameController.gameScore = maxTime - time;
    }
}
