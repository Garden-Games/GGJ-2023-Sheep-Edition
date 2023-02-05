using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    public TMP_Text timeRemaining;

    void Update()
    {
        if (!TimeManager.isOutOfTime)
        {
            timeRemaining.text = $"Time Remaining: {TimeManager.timeRemainingSeconds:0.}";
        } else
        {
            timeRemaining.text = "Time's up!";
        }
    }
}
