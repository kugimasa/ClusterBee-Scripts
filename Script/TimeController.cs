using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float maxTime = 10.0f;

    public void InitializeTime(ref float time)
    {
        time = maxTime;
        GameController.timeOver = false;
    }

    public void UpdateTime(ref float time)
    {
        if (time > 0.0f)
        {
            time -= Time.deltaTime;
        }
        else {
            time = 0.0f;
            GameController.gameOver = true;
            GameController.timeOver = true;
        }
        
    }

    public void UpdateTimeForBeeHunter(ref float time)
    {
        if (time > 0.0f)
        {
            time -= Time.deltaTime;
        }
        else {
            time = 0.0f;
            BeeHunterGameController.gameOver = true;
        }
        
    }
}
