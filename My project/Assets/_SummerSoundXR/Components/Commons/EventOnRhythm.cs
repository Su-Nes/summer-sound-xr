using System;
using UnityEngine;
using UnityEngine.Events;

public class EventOnRhythm : MonoBehaviour
{
    [SerializeField] private UnityEvent onBeatEvent;
    [SerializeField] private RhythmManager targetRhythmManager;
    private void OnEnable()
    {
        targetRhythmManager.onBeat += OnBeatInvoke;
    }

    private void OnDisable()
    {
        targetRhythmManager.onBeat -= OnBeatInvoke;
    }

    public void OnBeatInvoke()
    {
        onBeatEvent.Invoke();
    }
}
