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
            GameManager.instance.player1RoundWinned++;

        }
        else if (player1CurrentLife < player2CurrentLife)
        {
            GameManager.instance.player2RoundWinned++;
        }
        else
        {
            GameManager.instance.player1RoundWinned++;
            GameManager.instance.player2RoundWinned++;
        }
    }

    private void ManageRounds()
    {
        if (GameManager.instance.player1RoundWinned > 1)
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
                break;
            case GAME_STATE.PLAYER2WIN:
                spriteRenderer.sprite = player2WinSprite;
                break;
            case GAME_STATE.TIE:
                spriteRenderer.sprite = tieSprite;
                break;
        }
        retryButton.SetActive(true);
    }

}
