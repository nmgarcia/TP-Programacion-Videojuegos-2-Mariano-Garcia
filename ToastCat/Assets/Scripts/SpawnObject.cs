using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField]
    [Range(0.5f, 5.0f)]
    private float tiempoEspera;

    [SerializeField]
    [Range(0.5f, 5.0f)]
    private float tiempoIntervalo;

    [SerializeField] private bool habilitarSpawn = true; 

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
        if (habilitarSpawn)
        {
            GameObject obj = objectPool.GetPooledObject();
            if (obj != null)
            {
                // Reutilizamos el objeto del pool
                obj.transform.position = transform.position;
                obj.transform.rotation = Quaternion.identity;
                obj.SetActive(true);
            }
        }
    }
}
