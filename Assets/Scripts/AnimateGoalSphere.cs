using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateGoalSphere : MonoBehaviour
{
    public Material material;

    public float glowSpeed;
    public float glowIntensity;
    private Color color;

    private void Start()
    {
        color = material.color;
    }

    void Update()
    {
        float alpha = (Mathf.Sin(Time.time * glowSpeed) + 1) * glowIntensity;
        material.color = new Color(color.r, color.g, color.b, alpha);
    }
}
