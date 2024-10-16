using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite openSprite;

    private AudioSource audioSource;
    [SerializeField] private AudioClip winClip;
    private bool win = false;
    public bool GetWincondition => win;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Obtener las rotaciones
            var a = collision.GetComponent<Rigidbody2D>().rotation;
            var b = GetComponent<Rigidbody2D>().rotation;

            // Comparar las rotaciones normalizadas usando un helper comun
            if (CommonHelper.CheckCollisionDirection(a,b))
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = openSprite;
                win = true;

                if (!audioSource.isPlaying && !win)
                    audioSource.PlayOneShot(winClip);
            }         
        }
    }
}
