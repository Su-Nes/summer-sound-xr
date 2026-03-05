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
    
    [SerializeField] private UnityEvent onTriggerEvent;
    [SerializeField] private TriggerType triggerType;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (triggerType != TriggerType.onEnter)
            return;
        
        onTriggerEvent.Invoke();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (triggerType != TriggerType.onStay)
            return;
        
        onTriggerEvent.Invoke();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (triggerType != TriggerType.onExit)
            return;
        
        onTriggerEvent.Invoke();
    }
}
