using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{ 

    // Variables de uso interno en el script
    private bool puedoSaltar = true;
    [SerializeField] private bool saltando = false;
    private Vector2 gravedad = Vector2.zero;
    private bool invertirGravedad = false; 
    private bool gravedadEnX = false;
    [SerializeField] private float velocidadGravedad = -5f;
    public bool GetGravedadEnX => gravedadEnX;
    public bool GetInvertirGravedad => invertirGravedad;

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

    // Codigo ejecutado cuando el jugador colisiona con otro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            // Obtener las rotaciones para verificar que solo colisione con environment en la misma direccion
            var a = collision.gameObject.GetComponent<Rigidbody2D>().rotation;
            var b = GetComponent<Rigidbody2D>().rotation;

            if (CommonHelper.CheckCollisionDirection(a, b) && !puedoSaltar)
            {
                puedoSaltar = true;
                saltando = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        miRigidbody2D.velocity = gravedad;
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
