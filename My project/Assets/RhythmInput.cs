using System;
using UnityEngine;

[RequireComponent(typeof(EventOnRhythm))]
[RequireComponent(typeof(PulseScale))]
public class RhythmInput : MonoBehaviour
{
    [SerializeField] private int beatsUntilActive = 3;
    private int beatCounter;
    [SerializeField] private float timeActive = .5f;
    private float timeOnEnable, timeOnHit;
    private bool preparing, beatEnabled;

    private PulseScale pulseScale;
    
    private void Awake()
    {
        pulseScale = GetComponent<PulseScale>();
    }

    public void PrepareBeatHit()
    {
        preparing = true;
    }

    public void CountDownBeat()
    {
        if (!preparing)
            return;
        
        beatCounter++;
        if (beatCounter > beatsUntilActive)
        {
            EnableBeat();
            beatCounter = 0;
        }
        else
        {
            pulseScale.TriggerPulse();
        }
    }

    private void EnableBeat()
    {
        beatEnabled = true;
        timeOnEnable = Time.time;
        Invoke(nameof(DisableBeat), timeActive);
    }

    public void HitBeat()
    {
        if (!beatEnabled)
            return;
        
        timeOnHit = Time.time;
        
        pulseScale.TriggerPulse(-.5f);
    }

    private void DisableBeat()
    {
        preparing = false;
        beatEnabled = false;
    }
}
