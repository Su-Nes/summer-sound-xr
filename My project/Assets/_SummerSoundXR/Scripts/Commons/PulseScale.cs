using System;
using UnityEngine;

public class PulseScale : MonoBehaviour
{
    [SerializeField] private float scaleAdd, scaleLerp;
    private float scaleMult;
    private Vector3 originalScale;
    
    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void TriggerPulse()
    {
        scaleMult += scaleAdd;
    }

    private void Update()
    {
        scaleMult = Mathf.Lerp(scaleMult, 1f, scaleLerp);
        
        transform.localScale = originalScale * scaleMult;
    }
}
