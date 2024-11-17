using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private SpikedBall prefab; // Prefab de SpikeBall.
    [SerializeField] private int poolSize = 1; // Tamaño inicial del pool.

    private Queue<SpikedBall> pool = new Queue<SpikedBall>();

    // Inicializar el pool
    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            SpikedBall obj = Instantiate(prefab);
            obj.gameObject.SetActive(false); // Desactivar al principio.
            pool.Enqueue(obj);
        }
    }

    // Obtener un objeto del pool
    public SpikedBall GetObject(Vector3 position, Quaternion rotation)
    {
        if (pool.Count > 0)
        {
            SpikedBall obj = pool.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            // Expandir el pool si está vacío.
            SpikedBall obj = Instantiate(prefab, position, rotation);
            return obj;
        }
    }

    // Devolver un objeto al pool
    public void ReturnObject(SpikedBall obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
