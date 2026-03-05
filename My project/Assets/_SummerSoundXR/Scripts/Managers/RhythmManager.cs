using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance;
    public delegate void BeatEvent();
    public static event BeatEvent onBeat;
    
    [SerializeField] private float timeBeforeStart, bpm;
    private float t, bpmInSeconds;
    [Tooltip("x - measure; y - beat;")]
    public Vector2 beats;
    [SerializeField] private int beatsPerMeasure = 4;
    private bool pause = true;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else 
            Destroy(gameObject);

        bpmInSeconds = 60f / bpm;
        
        Invoke(nameof(StartSong), timeBeforeStart);
    }

    private void StartSong()
    {
        pause = false;
        GetComponent<AudioSource>().Play();
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
