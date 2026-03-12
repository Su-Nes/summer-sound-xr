using System;
using UnityEngine;
using UnityEngine.Events;

public class EventOnRhythm : MonoBehaviour
{
    [SerializeField] private UnityEvent onBeatEvent;
    private void OnEnable()
    {
        RhythmManager.onBeat += OnBeatInvoke;
    }

    private void OnDisable()
    {
        RhythmManager.onBeat -= OnBeatInvoke;
    }

    public void OnBeatInvoke()
    {
        onBeatEvent.Invoke();
    }
}
