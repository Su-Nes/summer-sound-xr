using System;
using UnityEngine;

[RequireComponent(typeof(PulseScale))]
[RequireComponent(typeof(EventOnRhythm))]
public class RhythmInput : MonoBehaviour
{
    [SerializeField] private int beatsUntilActive = 3;
    [SerializeField] private Transform rhythmGraphicTf;
    [SerializeField] private GameObject hitGraphicPrefab;
    private int beatCounter;
    [SerializeField] private float timeActive = .5f, graphicScaleMod;
    private float timeOnEnable, timeOnHit, timeUntilBeat, t, graphicStartZ;
    private bool preparing, beatEnabled;
    
    private void Start()
    {
        timeUntilBeat = beatsUntilActive - 1 * RhythmManager.Instance.SecondsPerBeat();
        graphicStartZ =  rhythmGraphicTf.localScale.z;
    }

    public void PrepareBeatHit()
    {
        preparing = true;
        t = 0f;
        
        rhythmGraphicTf.gameObject.SetActive(true);
    }

    private void Update()
    {
        ScaleRhythmGraphic();
    }

    private void ScaleRhythmGraphic()
    {
        if (!preparing)
            return;
        
        t += Time.deltaTime;
        Vector3 graphicScale = new Vector3(1f + (timeUntilBeat - t) * graphicScaleMod, 1f + (timeUntilBeat - t) * graphicScaleMod, graphicStartZ);
        rhythmGraphicTf.localScale = graphicScale;
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
        
        rhythmGraphicTf.gameObject.SetActive(false);
        
        Invoke(nameof(DisableBeat), timeActive);
    }

    public void HitBeat()
    {
        if (!beatEnabled)
            return;
        
        timeOnHit = Time.time;
        
        // effects
        GetComponent<PulseScale>().TriggerPulse();
        GameObject hitEffect = Instantiate(hitGraphicPrefab, transform.position, Quaternion.identity);
        Destroy(hitEffect, 1f);
        
        DisableBeat();
    }

    private void DisableBeat()
    {
        preparing = false;
        beatEnabled = false;
    }
}
