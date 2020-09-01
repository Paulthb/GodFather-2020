using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoulinAVent : MonoBehaviour
{
    public enum DIRECTION
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        DEFAULT
    }

    public DIRECTION direction = DIRECTION.DEFAULT;
    public float rotationStrength = 0.0f;

    void Update()
    {
    }
}
