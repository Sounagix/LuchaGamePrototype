using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SOUND : int
{
    NULL,
    HIT, ATTACK, DIE, STEP,
}

public class Sounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip hit;

    [SerializeField]
    private AudioClip attack;

    [SerializeField]
    private AudioClip die;

    [SerializeField]
    private AudioClip step;

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
            case SOUND.ATTACK:
                cURRENT_SOUND = SOUND.ATTACK;
                audioSource.clip = attack;
                break;
            case SOUND.DIE:
                cURRENT_SOUND = SOUND.DIE;
                audioSource.clip = die;
                break;
            case SOUND.STEP:
                cURRENT_SOUND = SOUND.STEP;
                audioSource.clip = step;
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
