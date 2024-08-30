using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite openSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Obtener las rotaciones
            var a = collision.GetComponent<Rigidbody2D>().rotation;
            var b = GetComponent<Rigidbody2D>().rotation;

            // Normalizar los ángulos
            a = Mathf.Repeat(a, 360f);
            b = Mathf.Repeat(b, 360f);

            // Comparar las rotaciones normalizadas usando DeltaAngle
            if (Mathf.Abs(Mathf.DeltaAngle(a, b)) < 0.01f)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = openSprite;
            }
            

        }
    }
}
