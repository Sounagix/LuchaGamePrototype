using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SOUND : int
{
    NULL,
    HIT, PUNCH,KICK, DIE, STEP, JUMP
}

public class Sounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip hit;

    [SerializeField]
    private AudioClip punchSound;

    [SerializeField]
    private AudioClip kickSound;

    [SerializeField]
    private AudioClip die;

    [SerializeField]
    private AudioClip step;

    [SerializeField]
    private AudioClip jumpSound;

    private AudioSource audioSource;

    [SerializeField]
    private SOUND cURRENT_SOUND = SOUND.NULL;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(SOUND sOUND)
    {
        switch (sOUND)
        {
            case SOUND.HIT:
                cURRENT_SOUND = SOUND.HIT;
                audioSource.clip = hit;
                break;
            case SOUND.PUNCH:
                cURRENT_SOUND = SOUND.PUNCH;
                audioSource.clip = punchSound;
                break;
            case SOUND.KICK:
                cURRENT_SOUND = SOUND.KICK;
                audioSource.clip = kickSound;
                break;
            case SOUND.DIE:
                cURRENT_SOUND = SOUND.DIE;
                audioSource.clip = die;
                break;
            case SOUND.STEP:
                cURRENT_SOUND = SOUND.STEP;
                audioSource.clip = step;
                break;
            case SOUND.JUMP:
                cURRENT_SOUND = SOUND.JUMP;
                audioSource.clip = jumpSound;
                break;
        }
        audioSource.Play();
    }

    public bool IsPlaying(SOUND sOUND)
    {
        return audioSource.isPlaying && cURRENT_SOUND.Equals(sOUND);
    }

    private void FixedUpdate()
    {
        if (!audioSource.isPlaying && !cURRENT_SOUND.Equals(SOUND.NULL))
        {
            cURRENT_SOUND = SOUND.NULL;
        }
    }
}
