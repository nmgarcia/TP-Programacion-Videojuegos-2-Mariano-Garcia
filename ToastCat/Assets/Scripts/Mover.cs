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
    private Vector2 direccion;

    // Variable para referenciar otro componente del objeto
    private Rigidbody2D miRigidbody2D;
    private ChangeGravity changeGravity;

    // Codigo ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        changeGravity = GetComponent<ChangeGravity>();
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable)
    private void Update()
    {
        moverHorizontal = Input.GetAxis("Horizontal");
       
    }
    private void FixedUpdate()
    {
        if (!changeGravity.GetGravedadEnX)
        {
            miRigidbody2D.velocity = new Vector2(moverHorizontal * (velocidad* Time.deltaTime), miRigidbody2D.velocity.y);
        }
        else
        {
            miRigidbody2D.velocity = new Vector2(miRigidbody2D.velocity.x, moverHorizontal * (velocidad * Time.deltaTime));
        }
    }
}
