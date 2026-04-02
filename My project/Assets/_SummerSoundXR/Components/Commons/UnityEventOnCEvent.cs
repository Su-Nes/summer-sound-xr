using System;
using UnityEngine;

public class UnityEventOnCEvent : MonoBehaviour
{
    private enum EventType
    {
        OnGoodScore,
        OnBadScore
    }
    
    [SerializeField] private EventType eventType;
    

    public void OnEnable()
    {
        ScoreManager.onGoodScore += InvokeEvent;
        ScoreManager.onBadScore += InvokeEvent;
    }

    private void OnDisable()
    {
        ScoreManager.onGoodScore -= InvokeEvent;
        ScoreManager.onBadScore -= InvokeEvent;
    }

    public void InvokeEvent()
    {
        switch (eventType)
        {
            case EventType.OnGoodScore:
                ScoreManager.instance.OnGoodScore();
                break;
            
            case EventType.OnBadScore:
                ScoreManager.instance.OnBadScore();
                break;
        }
    }
}
