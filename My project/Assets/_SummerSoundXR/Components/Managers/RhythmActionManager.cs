using System;
using System.Globalization;
using UnityEngine;

[RequireComponent(typeof(EventOnRhythm))]
public class RhythmActionManager : MonoBehaviour
{
    [SerializeField] private RhythmManager rhythmManager;
    [SerializeField] private TextAsset beatMap;
    [SerializeField] private RhythmInput[] rhythmInputs;

    private string[] beatMapLines;
    private int beatLine;

    private void Start()
    {
        beatMapLines = beatMap.text.Split('\n');
    }

    public void ReadBeat()
    {
        string currentLine = beatMapLines[beatLine];
        string beatString = $"{rhythmManager.beats.x}.{rhythmManager.beats.y};";

        if (!currentLine.Contains(beatString))
            return;
        //Debug.LogError(beatString);
        string beatIndexes = currentLine.Substring(beatString.Length);
        
        // trigger all beat indexes
        foreach (string beatIndex in beatIndexes.Split(' '))
        {
            rhythmInputs[int.Parse(beatIndex)].PrepareBeatHit();
        }

        beatLine++;
    }
}
