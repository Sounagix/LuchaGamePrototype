using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GAME_STATE
{
    PLAYER1WIN, PLAYER2WIN, TIE
}


public static class KoActions
{
    public static Action Ko;
}
/// <summary>
/// El roundManager se enga ..... ->
/// </summary>
public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Life player1Life;

    [SerializeField]
    private Life player2Life;

    [SerializeField]
    private Sprite KoSprite;

    [SerializeField]
    private Sprite timeUpSprite;

    [SerializeField]
    private Sprite tieSprite;

    [SerializeField]
    private Sprite player1WinSprite;

    [SerializeField]
    private Sprite player2WinSprite;

    [SerializeField]
    private float timeToShowWinner;

    [SerializeField]
    private AudioClip koSound;

    [SerializeField]
    private AudioClip timeUpSound;

    [SerializeField]
    private AudioClip tieSound;

    [SerializeField]
    private AudioClip round1Sound;

    [SerializeField]
    private AudioClip round2Sound;

    [SerializeField]
    private AudioClip round3Sound;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameObject retryButton;

    private Image spriteRenderer;

    [SerializeField]
    private GameObject guantesRojos;

    [SerializeField]
    private GameObject guantesAzules;

    [SerializeField]
    private Sprite guanteRojoWin;

    [SerializeField]
    private Sprite guanteAzulWin;


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        GameTimeAction.timeUp += OnTimeUp;
        KoActions.Ko += OnKo;
    }

    private void OnDisable()
    {
        GameTimeAction.timeUp -= OnTimeUp;
        KoActions.Ko -= OnKo;
    }

    private void Start()
    {
        switch (GameManager.instance.currentRound)
        {
            case 0:
                audioSource.PlayOneShot(round1Sound);
                break;
            case 1:
                audioSource.PlayOneShot(round2Sound);
                break;
            case 2:
                audioSource.PlayOneShot(round3Sound);
                break;
            default:
                break;
        }
        RenderGloves();
    }

    private void OnTimeUp()
    {
        spriteRenderer.sprite = timeUpSprite;
        spriteRenderer.enabled = true;
        audioSource.PlayOneShot(timeUpSound);
        AddRoundPoints();
        ManageRounds();
    }

    private void OnKo()
    {
        spriteRenderer.sprite = KoSprite;
        spriteRenderer.enabled = true;
        audioSource.PlayOneShot(koSound);
        AddRoundPoints();
        ManageRounds();

    }

    private void AddRoundPoints()
    {
        float player1CurrentLife = player1Life.GetCurrentLife();
        float player2CurrentLife = player2Life.GetCurrentLife();

        if (player1CurrentLife > player2CurrentLife)
        {
           // guantesRojos.transform.GetChild(GameManager.instance.player1RoundWinned).GetComponent<Image>().sprite = guanteRojoWin;
            GameManager.instance.player1RoundWinned++;
        }
        else if (player1CurrentLife < player2CurrentLife)
        {
            //guantesRojos.transform.GetChild(GameManager.instance.player2RoundWinned).GetComponent<Image>().sprite = guanteAzulWin;
            GameManager.instance.player2RoundWinned++;
        }
        else
        {
            //guantesRojos.transform.GetChild(GameManager.instance.player1RoundWinned).GetComponent<Image>().sprite = guanteRojoWin;
            //
            //guantesRojos.transform.GetChild(GameManager.instance.player2RoundWinned).GetComponent<Image>().sprite = guanteAzulWin;
            GameManager.instance.player1RoundWinned++;
            GameManager.instance.player2RoundWinned++;
        }

        RenderGloves();
    }

    private void ManageRounds()
    {
        if (GameManager.instance.player1RoundWinned == GameManager.instance.player2RoundWinned && GameManager.instance.player1RoundWinned > 1)
        {
            ShowPlayerWinPanel(GAME_STATE.TIE);
        }
        else if (GameManager.instance.player1RoundWinned > 1)
        {
            ShowPlayerWinPanel(GAME_STATE.PLAYER1WIN);
        }
        else if (GameManager.instance.player2RoundWinned > 1)
        {
            ShowPlayerWinPanel(GAME_STATE.PLAYER2WIN);
        }
        else
        {
            GameManager.instance.currentRound++;
            StartCoroutine(Wait());
        }
    }

    private void RenderGloves()
    {
        if (GameManager.instance.player1RoundWinned > 0)
        {
            for (int i = 0; i < GameManager.instance.player1RoundWinned; i++)
            {
                guantesRojos.transform.GetChild(i).GetComponent<Image>().sprite = guanteRojoWin;

            }
        }

        if (GameManager.instance.player2RoundWinned > 0)
        {
            for (int i = 0; i < GameManager.instance.player2RoundWinned; i++)
            {
                guantesAzules.transform.GetChild(i).GetComponent<Image>().sprite = guanteAzulWin;

            }
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeToShowWinner);
        GameManager.instance.LoadGameScene();
    }


    private void ShowPlayerWinPanel(GAME_STATE gAME_STATE)
    {
        switch (gAME_STATE)
        {
            case GAME_STATE.PLAYER1WIN:
                spriteRenderer.sprite = player1WinSprite;
                retryButton.SetActive(true);
                break;
            case GAME_STATE.PLAYER2WIN:
                spriteRenderer.sprite = player2WinSprite;
                retryButton.SetActive(true);
                break;
            case GAME_STATE.TIE:
                spriteRenderer.sprite = tieSprite;
                if(GameManager.instance.player1RoundWinned > 1)
                {
                    retryButton.SetActive(true);
                }
                break;
        }
        GameManager.instance.retryActive = true;
    }
}
