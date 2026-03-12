using System;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvents : MonoBehaviour
{
    private enum TriggerType
    {
        onEnter,
        onStay,
        onExit
    }

    [SerializeField] private bool destroyOnTrigger;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private UnityEvent onTriggerEvent;
    [SerializeField] private TriggerType triggerType;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (triggerType != TriggerType.onEnter)
            return;
        
        InvokeEvent(other.gameObject.tag);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (triggerType != TriggerType.onStay)
            return;
        
        InvokeEvent(other.gameObject.tag);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (triggerType != TriggerType.onExit)
            return;
        
        InvokeEvent(other.gameObject.tag);
    }
    
    private void InvokeEvent(string tag)
    {
        if (targetTag != tag)
            return;
        
        onTriggerEvent.Invoke();
        
        if (destroyOnTrigger)
            Destroy(gameObject);
    }
}
