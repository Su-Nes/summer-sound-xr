using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RhythmManager : MonoBehaviour
{
    public delegate void BeatEvent();
    public event BeatEvent onBeat;
    
    [SerializeField] private float bpm, beatOffset;
    public float beatDelay;
    private float t, bpmInSeconds;
    [Tooltip("x - measure; y - beat;")]
    public Vector2 beats;
    [SerializeField] private int beatsPerMeasure = 4;
    private bool pause = true;

    
    private void Awake()
    {
        bpmInSeconds = 60f / bpm;
    }
    
    public float SecondsPerBeat()
    {
        return bpmInSeconds;
    }

    public void StartBeat()
    {
        pause = false;
        t += -beatDelay + beatOffset * SecondsPerBeat();
        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();
    }

    public void StopBeat()
    {
        pause = true;
    }
    
    private void Update()
    {
        if (pause)
            return;
        
        t += Time.deltaTime;
        
        if (t >= bpmInSeconds)
        {
            InvokeOnBeat();
            t = 0f;
        }
    }

    public void InvokeOnBeat()
    {
        beats.y++;

        if (beats.y > beatsPerMeasure)
        {
            beats.x++;
            beats.y = 1;
        }
        
        onBeat?.Invoke();
    }
}
