using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Variables a configurar desde el editor
    [Header("Configuracion")]
    [SerializeField] float velocidad = 5f;

    // Variables de uso interno en el script
    private float moverHorizontal;
    private float moverVertical;
    private Vector2 direccion;

    // Variable para referenciar otro componente del objeto
    private Rigidbody2D miRigidbody2D;
    private ChangeGravity changeGravity;
    private SpriteRenderer spriteRenderer;

    // Codigo ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        changeGravity = GetComponent<ChangeGravity>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable)
    private void Update()
    {
        moverHorizontal = Input.GetAxis("Horizontal");
        moverVertical = Input.GetAxis("Vertical");

        if (moverHorizontal != 0 || moverVertical != 0)            
            FlipX();
    }

    private void FlipX()
    {
       
        // Determinar el input basado en la gravedad
        float input = changeGravity.GetGravedadEnX ? moverVertical : moverHorizontal;

        // Calcular el factor de direcci�n
        float direction = (changeGravity.GetGravedadEnX == changeGravity.GetInvertirGravedad) ? 1f : -1f;

        // Actualizar el scale.x solo si hay movimiento
        if (input != 0)
        {
            spriteRenderer.flipX = (Mathf.Sign(input) * direction)<0;            
        }
    }

    private void FixedUpdate()
    {
        //Verificamos si la gravedad esta en X para definir como realizar el avance
        if (!changeGravity.GetGravedadEnX)
        {
            miRigidbody2D.velocity = new Vector2(moverHorizontal  * (velocidad* Time.deltaTime), miRigidbody2D.velocity.y);
        }
        else
        {
            miRigidbody2D.velocity = new Vector2(miRigidbody2D.velocity.x, moverVertical  * (velocidad * Time.deltaTime));
        }
    }
}
