using System;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public delegate void BeatEvent();
    public static event BeatEvent onBeat;

    [SerializeField] private float bpm;
    private float t, bpmInSeconds;
    
    public static RhythmManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else 
            Destroy(gameObject);

        bpmInSeconds = 60f / bpm;
    }
    
    private void Update()
    {
        t += Time.deltaTime;
        
        if (t >= bpmInSeconds)
        {
            InvokeOnBeat();
            t = 0f;
        }
    }

    public void InvokeOnBeat()
    {
        onBeat?.Invoke();;
    }
}
