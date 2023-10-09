using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    const float tau = 2 * Mathf.PI;

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] Vector3 rotationVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        // if (period == 0) { return; }
        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1) / 2;

        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
        transform.Rotate(rotationVector);
    }
}
