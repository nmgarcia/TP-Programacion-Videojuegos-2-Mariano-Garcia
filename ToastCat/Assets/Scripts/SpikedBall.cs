using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private Vector3 direction = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
            Destroy(gameObject);
    }

    public void SetDirection(Vector3 vector)
    {
        direction = vector;
    }
}
