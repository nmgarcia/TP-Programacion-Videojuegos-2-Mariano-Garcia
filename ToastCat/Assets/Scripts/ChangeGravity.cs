using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{ 

    // Variables de uso interno en el script
    private bool puedoSaltar = true;
    private bool saltando = false;
    private Vector2 gravedad = Vector2.zero;
    private bool invertirGravedad = false; 
    private bool gravedadEnX = false;
    [SerializeField] private float velocidadGravedad = -9.8f;
    public bool GetGravedadEnX => gravedadEnX;

    // Variable para referenciar otro componente del objeto
    private Rigidbody2D miRigidbody2D;

    // Codigo ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        miRigidbody2D.gravityScale = 0;
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            puedoSaltar = false;
            invertirGravedad = !invertirGravedad;
        }        
    }

    private void FixedUpdate()
    {
        if (!puedoSaltar && !saltando)
        {
            if (!gravedadEnX)
            {
                gravedad = new Vector2(0, velocidadGravedad * (invertirGravedad?-1f:1f));
                miRigidbody2D.SetRotation(invertirGravedad ? 180 : 0);
            }
            else
            {
                gravedad = new Vector2(velocidadGravedad * (invertirGravedad ? -1f : 1f), 0);
                miRigidbody2D.SetRotation(invertirGravedad ? 90 : 270);
            }

            miRigidbody2D.velocity = gravedad;
            saltando = true;
        }
    }

    private void FlipX()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FlipY()
    {
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }

    // Codigo ejecutado cuando el jugador colisiona con otro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!puedoSaltar)
        {
            puedoSaltar = true;
            saltando = false;
        }
    }

    //Codigo ejecutado cuando colision con un objeto trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {           
            // Cambiar el eje de gravedad
            gravedadEnX = !gravedadEnX;
            saltando = false;
        }
    }
}
