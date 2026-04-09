using System;
using UnityEngine;

public class PulseScale : MonoBehaviour
{
    [SerializeField] private float scaleAdd = .3f, scaleLerp = .5f;
    [SerializeField] private bool disableColliderOnPulse;
    private float scaleMult;
    private Vector3 originalScale;
    
    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void TriggerPulse()
    {
        scaleMult += scaleAdd;
        if (disableColliderOnPulse)
            GetComponent<Collider>().enabled = false;
    }

    public void TriggerPulse(float scaleValue)
    {
        scaleMult += scaleValue;
        if (disableColliderOnPulse)
            GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        scaleMult = Mathf.Lerp(scaleMult, 1f, scaleLerp);
        
        transform.localScale = originalScale * scaleMult;
        
        if (Mathf.Approximately(scaleMult, 1f) && disableColliderOnPulse)
            GetComponent<Collider>().enabled = true;
    }
}
