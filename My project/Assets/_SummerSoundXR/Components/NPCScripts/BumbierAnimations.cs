using System;
using System.Collections;
using UnityEngine;

public class BumbierAnimations : MonoBehaviour
{
    private static readonly int IsDancing = Animator.StringToHash("IsDancing");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    [SerializeField] private float moveSpeed = 10f, dancingThreshold = .75f, angryThreshold = -.5f;
    public SpriteRenderer faceRenderer;
    [SerializeField] Sprite angryFace;
    private Sprite startFace;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        startFace = faceRenderer.sprite;
    }

    public IEnumerator SetMovementDestination(Vector3[] walkPoints)
    {
        int moveIndex = 0;
        animator.SetBool(IsWalking, true);

        while (Vector3.Distance(walkPoints[^1], transform.position) > .05)
        {
            while (Vector3.Distance(walkPoints[moveIndex], transform.position) > .05)
            {
                HandleMovement(walkPoints[moveIndex]);
                yield return null;
            }
            if (moveIndex < walkPoints.Length - 1)
                moveIndex++;
        }
        
        animator.SetBool(IsWalking, false);
    }

    private void Update()
    {
        if (ScoreManager.instance.Score >= dancingThreshold)
            animator.SetBool(IsDancing, true);
        else 
            animator.SetBool(IsDancing, false);

        if (ScoreManager.instance.Score < angryThreshold)
            faceRenderer.sprite = angryFace;
    }

    private void HandleMovement(Vector3 moveDestination)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDestination, moveSpeed * Time.deltaTime);
    }
}
