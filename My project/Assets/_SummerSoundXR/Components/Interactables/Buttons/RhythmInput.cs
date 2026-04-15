using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PulseScale))]
[RequireComponent(typeof(EventOnRhythm))]
public class RhythmInput : MonoBehaviour
{
    [SerializeField] private int beatsUntilActive = 3;
    [SerializeField] private ScaleDownWithTime rhythmGraphic;
    [SerializeField] private GameObject positiveHitGraphicPrefab, negativeHitGraphicPrefab;
    private int beatCounter;
    [SerializeField] private float goodTimingRange = .33f;
    private float timeOnEnable;

    [SerializeField] private RhythmManager songRhythmManager;
    
    public void PrepareBeatHit()
    {
        float timeUntilBeat = (beatsUntilActive - 1) * songRhythmManager.SecondsPerBeat();
        timeOnEnable = Time.time + timeUntilBeat;

        rhythmGraphic.InitializeGraphic(timeUntilBeat);
    }

    public void HitBeat()
    {
        if (CalculateHit(Time.time))
        {
            ScoreManager.OnGoodScore();
            GameObject hitEffect = Instantiate(positiveHitGraphicPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, 1f);
        }
        else
        {
            ScoreManager.OnBadScore();
            GameObject hitEffect = Instantiate(negativeHitGraphicPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, 1f);
        }
        
        // effects
        GetComponent<PulseScale>().TriggerPulse();
    }

    private bool CalculateHit(float timeOnHit)
    {
        return MathF.Abs(timeOnEnable - timeOnHit) < goodTimingRange;
    }
}
