using System;
using System.Globalization;
using UnityEngine;

[RequireComponent(typeof(EventOnRhythm))]
public class RhythmActionManager : MonoBehaviour
{
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
        string beatString = $"{RhythmManager.Instance.beats.x}.{RhythmManager.Instance.beats.y};";
        Debug.LogError($"{currentLine} and {beatString}");
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
