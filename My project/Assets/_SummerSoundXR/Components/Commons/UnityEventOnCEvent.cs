using System;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnCEvent : MonoBehaviour
{
    private enum EventType
    {
        OnGoodScore,
        OnBadScore
    }
    
    [SerializeField] private EventType eventType;
    [SerializeField] private UnityEvent onInvoke;
    

    public void OnEnable()
    {
        switch (eventType)
        {
            case EventType.OnGoodScore:
                ScoreManager.onGoodScore += InvokeEvent;
                break;
            
            case EventType.OnBadScore:
                ScoreManager.onBadScore += InvokeEvent;
                break;
        }
    }

    private void OnDisable()
    {
        switch (eventType)
        {
            case EventType.OnGoodScore:
                ScoreManager.onGoodScore -= InvokeEvent;
                break;
            
            case EventType.OnBadScore:
                ScoreManager.onBadScore -= InvokeEvent;
                break;
        }
    }

    public void InvokeEvent()
    {
        onInvoke.Invoke();
    }
}
