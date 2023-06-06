using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public static class GameTimeAction
{
    public static Action timeUp;
}

public class GameTime : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gameTextTime;

    [SerializeField]
    private int gameTotalTime;

    private float startTime;

    private bool timeUpMsgSent = false;

    private bool koActive = false;

    private void OnEnable()
    {
        KoActions.Ko += delegate () { koActive = true; };
    }

    private void OnDisable()
    {
        KoActions.Ko -= delegate () { koActive = true; };
    }

    private void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }

    private void LateUpdate()
    {
        if (!koActive)
        {
            float currentTime = Time.realtimeSinceStartup - startTime;
            int recursiveTime = gameTotalTime - (int)currentTime;
            if (recursiveTime >= 0)
                gameTextTime.text = (recursiveTime).ToString();
            if (!timeUpMsgSent && recursiveTime <= 0)
            {
                timeUpMsgSent = true;
                GameTimeAction.timeUp();
            }
        }
    }
}
