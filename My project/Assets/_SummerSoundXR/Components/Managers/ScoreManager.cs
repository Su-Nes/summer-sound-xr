using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    public delegate void GoodScore();
    public static event GoodScore onGoodScore;
    
    public delegate void BadScore();
    public static event BadScore onBadScore;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }
    
    public void OnGoodScore()
    {
        onGoodScore?.Invoke();
    }

    public void OnBadScore()
    {
        onBadScore?.Invoke();
    }
}
