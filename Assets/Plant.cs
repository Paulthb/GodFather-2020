using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("ça marche");
    }
}
