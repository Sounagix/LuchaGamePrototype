using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


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
        if(PeripheralsFinder.instance != null)
            PeripheralsFinder.instance.ResetDivice();
        SceneManager.LoadScene(index);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

}
