using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private SpikedBall spawnObject; //El objeto debe tener su propio comportamiento de movimiento y desaparicion
    
    [SerializeField]
    [Range(0.5f, 5.0f)]
    private float tiempoEspera;

    [SerializeField]
    [Range(0.5f, 5.0f)]
    private float tiempoIntervalo;

    [SerializeField] private bool habilitarInstantiate = true; //Para controlar el invoke
    [SerializeField] private Vector3 direccionObjeto = Vector3.up;

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnGameObject), tiempoEspera, tiempoIntervalo);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnGameObject));
    }

    void SpawnGameObject()
    {
        if (habilitarInstantiate)
        {
            SpikedBall nuevoObjeto = Instantiate(spawnObject, transform.position, Quaternion.identity);
            nuevoObjeto.SetDirection(direccionObjeto);
        }
           
    }
}
