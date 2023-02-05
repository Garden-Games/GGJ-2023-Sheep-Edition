using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float durationOfLevelSeconds;
    private static float _timeRemainingSeconds;
    public static float timeRemainingSeconds
    {
        get
        {
            return _timeRemainingSeconds;
        }
        private set
        {
            _timeRemainingSeconds = value;
        }
    }
    private static bool _isOutOfTime;
    public static bool isOutOfTime
    {
        get
        {
            return _isOutOfTime;
        }
        private set
        {
            _isOutOfTime = value;
        }
    }

    private void Start()
    {
        timeRemainingSeconds = durationOfLevelSeconds;
        isOutOfTime = false;
    }

    void Update()
    {
        if (!isOutOfTime)
        {
            timeRemainingSeconds -= Time.deltaTime;
            if (timeRemainingSeconds < 0)
            {
                isOutOfTime = true;
            }
        }
    }
}
