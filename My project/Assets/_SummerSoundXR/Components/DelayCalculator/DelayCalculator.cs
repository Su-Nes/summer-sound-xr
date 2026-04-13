using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(RhythmManager))]
public class DelayCalculator : MonoBehaviour
{
    private float beatTime, playerInputTime;
    private int requiredBeatCount = 10, beatCount;
    private Vector3 startPos, endPos;

    [SerializeField] private TMP_Text buttonText, delayText;
    [SerializeField] private RhythmManager songRhythmManager;

    private void Start()
    {
        startPos = transform.position;
        endPos = transform.position + transform.right * .3f;
        transform.position = endPos;
        
        StopCalibration();
        delayText.text = "Delay: 0s";
    }

    private void StartCalibration()
    {
        transform.position = startPos;
        buttonText.text = "Hit me!";
        beatCount = requiredBeatCount;
        GetComponent<RhythmManager>().StartBeat();
    }

    private void StopCalibration()
    {
        buttonText.text = "Start calibration";
        GetComponent<RhythmManager>().StopBeat();
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
        {
            transform.position = endPos;
            StopCalibration();
        }
    }

    private void CalibrateDelay()
    {
        float delay = playerInputTime - beatTime;
        songRhythmManager.beatDelay = delay;
        
        delayText.text = $"Delay: {delay:F2}s";
    }
}
