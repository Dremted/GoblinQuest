using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStartCamera : MonoBehaviour
{
    [SerializeField] private float SpeedCamera = 7;

    private bool endCinema;

    private void Update()
    {
        if (!endCinema)
        {
            Vector3 lastPos = transform.position;
            lastPos.x += SpeedCamera * Time.deltaTime;
            transform.position = lastPos;
        }
    }

    public void StopCamera()
    {
        endCinema = true;
    }
}
