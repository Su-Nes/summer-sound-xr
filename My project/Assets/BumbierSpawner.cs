using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BumbierSpawner : MonoBehaviour
{
    [SerializeField] private RhythmManager rhythmManager;
    [SerializeField] private Transform spawnPoint, firstWalkPoint;
    [SerializeField] private Transform[] standPoints;
    private int standPointIndex;
    [SerializeField] private BumbierAnimations[] bumbiers;
    [SerializeField] private Sprite[] randomFaces;

    [SerializeField] private float spawnInterval = 5f, heightOffset;
    private float t;
    
    private void Update()
    {
        if (rhythmManager.Pause || standPointIndex >= standPoints.Length)
            return;
        
        t += Time.deltaTime;

        if (t >= spawnInterval)
        {
            SpawnBumbier();
            standPointIndex++;
            t = 0f;
        }
    }

    private void SpawnBumbier()
    {
        BumbierAnimations bumbier = Instantiate(bumbiers[Random.Range(0, bumbiers.Length)], spawnPoint);
        bumbier.faceRenderer.sprite = randomFaces[Random.Range(0, randomFaces.Length)];
        
        StartCoroutine(bumbier.SetMovementDestination(new []{firstWalkPoint.position + Vector3.up * heightOffset, standPoints[standPointIndex].position + Vector3.up * heightOffset}));
    }
}
