using UnityEngine;

public class SetYPosition : MonoBehaviour
{
    [SerializeField] private Transform targetTf;
    
    public void SetY(float value)
    {
        Vector3 newPos = targetTf.localPosition;
        newPos.y = value;
        targetTf.localPosition = newPos;
    }
}
