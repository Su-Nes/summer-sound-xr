using System;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    public delegate void GoodScore();
    public static event GoodScore onGoodScore;
    
    public delegate void BadScore();
    public static event BadScore onBadScore;


    [SerializeField] private Transform scoreRepresentative;
    [SerializeField] private float scoreRepresentativeDistanceAmplitude, addScoreValue, removeScoreValue, scoreDecay;
    [SerializeField] private RhythmManager songRhythmManager;
    
    private float score;
    private bool scoreIsCounting;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        onGoodScore += AddScore;
        onBadScore += RemoveScore;
    }
    
    private void OnDisable()
    {
        onGoodScore -= AddScore;
        onBadScore -= RemoveScore;
    }

    public static void OnGoodScore()
    {
        onGoodScore?.Invoke();
    }

    public static void OnBadScore()
    {
        onBadScore?.Invoke();
    }

    private void Update()
    {
        if (songRhythmManager.Pause)
            return;
        
        
        if (score > 0f)
            score -= scoreDecay * Time.deltaTime;
        else
            score = 0f;
        
        
        score = Mathf.Clamp(score, -1f, 1f);
        
        Vector3 scoreRepresentativePos = Vector3.zero;
        scoreRepresentativePos.x = math.remap(-1f, 1f, -scoreRepresentativeDistanceAmplitude, scoreRepresentativeDistanceAmplitude, score);
        scoreRepresentative.localPosition = scoreRepresentativePos;
    }

    public void AddScore()
    {
        score += addScoreValue;
    }

    public void RemoveScore()
    {
        score += removeScoreValue;
    }
}
