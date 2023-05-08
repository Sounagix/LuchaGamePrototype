using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField]
    private float maxLife;

    [SerializeField]
    private Scrollbar scrollbar;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject koPanel;

    [SerializeField]
    private AudioClip koSound;

    private float currLife;

    private bool isDeath = false;

    private Sounds sounds;

    private void Awake()
    {
        sounds = GetComponent<Sounds>();
    }


    private void Start()
    {
        currLife = maxLife;
        scrollbar.size = 1.0f;
    }

    public bool DamageLife(float amout)
    {
        sounds.PlaySound(SOUND.HIT);
        animator.SetTrigger("Hit");
        currLife -= amout;
        scrollbar.size = currLife / maxLife;
        isDeath = currLife <= 0.0f;
        if (isDeath)
        {
            sounds.PlaySound(SOUND.DIE);
            animator.SetTrigger("Die");
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
            koPanel.SetActive(true);
            var audioSource = Camera.main.GetComponent<AudioSource>();
            audioSource.clip = koSound;
            audioSource.loop = false;
            audioSource.volume = 1.0f;
            audioSource.Play();
            return true;
        }
        else return false;
    }

    public bool IsDeath()
    {
        return isDeath;
    }
}
