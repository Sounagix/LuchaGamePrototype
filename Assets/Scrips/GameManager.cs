using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    public int currentRound = 0;
    public int player1RoundWinned = 0;
    public int player2RoundWinned = 0;
    public bool retryActive = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(int index)
    {
        retryActive = false;
        instance.currentRound = 0;
        instance.player1RoundWinned = 0;
        instance.player2RoundWinned = 0;
        if (PeripheralsFinder.instance != null)
            PeripheralsFinder.instance.ResetDivice();
        SceneManager.LoadScene(index);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void LoadGameScene()
    {
        if (PeripheralsFinder.instance != null)
            PeripheralsFinder.instance.ResetDivice();
        SceneManager.LoadScene(1);
    }

}
