using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject; //El objeto debe tener su propio comportamiento de movimiento y desaparicion
    
    [SerializeField]
    [Range(0.5f, 5.0f)]
    private float tiempoEspera;

    [SerializeField]
    [Range(0.5f, 5.0f)]
    private float tiempoIntervalo;

    [SerializeField] private bool habilitarInstantiate = true;

    void SpawnGameObject()
    {
        if(habilitarInstantiate)
            Instantiate(spawnObject,transform.position,Quaternion.identity);
    }

    private void OnBecameInvisible()
    {
        CancelInvoke(nameof(SpawnGameObject));
    }

    private void OnBecameVisible()
    {
        InvokeRepeating(nameof(SpawnGameObject), tiempoEspera, tiempoIntervalo);
    }
}
