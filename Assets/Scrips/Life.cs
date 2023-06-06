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
    private AudioClip koSound;

    [SerializeField]
    private Color tint;

    private float currLife;

    private bool isDeath = false;

    private Sounds sounds;

    private SkinnedMeshRenderer spriteRenderer;



    private void Awake()
    {
        sounds = GetComponent<Sounds>();
        spriteRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        currLife = maxLife;
        scrollbar.size = 1.0f;
    }


    public bool DamageLife(float amout)
    {
        if (!animator.GetBool("Block"))
        {
            spriteRenderer.material.color = tint;
            Invoke(nameof(BackFromTint), 0.25f);
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
                KoActions.Ko();
                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }

    }

    public bool IsDeath()
    {
        return isDeath;
    }

    public float GetCurrentLife()
    {
        return currLife;
    }

    private void BackFromTint()
    {
        spriteRenderer.material.color = Color.white;
    }
}
