using System;
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
    [SerializeField] private float timeActive = .5f;
    private float timeOnEnable, timeOnHit;
    private bool preparing, beatEnabled;

    [SerializeField] private RhythmManager songRhythmManager;

    public void PrepareBeatHit()
    {
        preparing = true;

        rhythmGraphic.InitializeGraphic(beatsUntilActive - 1 * songRhythmManager.SecondsPerBeat());
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
    }

    private void EnableBeat()
    {
        beatEnabled = true;
        timeOnEnable = Time.time;
        
        Invoke(nameof(DisableBeat), timeActive);
    }

    public void HitBeat()
    {
        if (beatEnabled)
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

        timeOnHit = Time.time;
        
        // effects
        GetComponent<PulseScale>().TriggerPulse();
        
        DisableBeat();
    }

    private void DisableBeat()
    {
        preparing = false;
        beatEnabled = false;
    }
}
