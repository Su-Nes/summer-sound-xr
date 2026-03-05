using System;
using System.Globalization;
using UnityEngine;

[RequireComponent(typeof(EventOnRhythm))]
public class RhythmActionManager : MonoBehaviour
{
    [SerializeField] private TextAsset beatMap;
    [SerializeField] private RhythmInput[] rhythmInputs;

    public void ReadBeat()
    {
        string beatString = $"{RhythmManager.Instance.beats.x}.{RhythmManager.Instance.beats.y} "; 
        
        int beatIndex = beatMap.text.IndexOf(beatString, StringComparison.Ordinal) + beatString.Length;
        
        if (!int.TryParse(beatMap.text[beatIndex].ToString(), out int buttonIndex))
            return;
        
        rhythmInputs[buttonIndex].PrepareBeatHit();
    }
}
