using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonHelper
{
    public static bool CheckCollisionDirection(float rotationA, float rotationB)
    {
        // Normalizar los angulos dentro de los 360
        rotationA = Mathf.Repeat(rotationA, 360f);
        rotationB = Mathf.Repeat(rotationB, 360f);

        float deltaAngle = Mathf.DeltaAngle(rotationA, rotationB);

        // Verificamos angulos equivalentes si la diferencia es cercana a 0 o a 180 grados
        return Mathf.Abs(deltaAngle) < 0.01f || Mathf.Abs(Mathf.Abs(deltaAngle) - 180f) < 0.01f;
    }  
    
}
