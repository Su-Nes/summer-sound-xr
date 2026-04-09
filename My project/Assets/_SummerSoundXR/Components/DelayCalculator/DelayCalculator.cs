using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(RhythmManager))]
public class DelayCalculator : MonoBehaviour
{
    private float beatTime, playerInputTime;
    private int requiredBeatCount = 10, beatCount;
    private Vector3 endPos;

    [SerializeField] private TMP_Text buttonText, delayText;
    [SerializeField] private RhythmManager songRhythmManager;

    private void Start()
    {
        endPos = transform.up * .1f;
        
        StopCalibration();
        delayText.text = "Delay: 0s";
    }

    private void StartCalibration()
    {
        buttonText.text = "Hit me!";
        beatCount = requiredBeatCount;
    }

    private void StopCalibration()
    {
        buttonText.text = "Start calibration";
    }
    
    public void GetBeatOrigin()
    {
        if (requiredBeatCount == 0)
            return;
        
        beatTime = Time.time;
        GetComponent<AudioSource>().Play();

        CalibrateDelay();
    }

    public void PlayerBeat()
    {
        if (beatCount == 0)
            StartCalibration();
        
        playerInputTime = Time.time;
        beatCount--;

        if (beatCount <= 0)
            transform.position = endPos;
    }

    private void CalibrateDelay()
    {
        float delay = playerInputTime - beatTime;
        songRhythmManager.beatDelay = delay;
        
        delayText.text = $"Delay: {delay}s";
    }
}
