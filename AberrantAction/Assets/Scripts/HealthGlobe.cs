using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGlobe : MonoBehaviour {

    [SerializeField]
    private float healthRestored = 10f;

    public void SetHealthRestored(float healthRestored)
    {
        this.healthRestored = healthRestored;
    }

    public float GetHealthRestored()
    {
        return healthRestored;
    }
}
