using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        startPos = transform.position;
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * frequency + randomOffset) * amplitude;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}