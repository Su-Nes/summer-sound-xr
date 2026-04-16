using Oculus.Interaction;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PinchKnob : MonoBehaviour
{
    [SerializeField] private Grabbable grabbableObject;
    private Rigidbody rb;
    [SerializeField] private Transform handleTransform;
    [SerializeField] private float maxPinchDistance = .4f;
    [SerializeField] private Vector2 remapRange;
    [SerializeField] private UnityEvent<float> onValueChanged;
    
    private float zAmplitude;
    
    private void Start()
    {
        zAmplitude = Mathf.Abs(grabbableObject.transform.localRotation.z);
        rb = grabbableObject.GetComponent<Rigidbody>();
        
        InvokeValue();
    }
    
    public void Update()
    {
        if (!rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
            rb.position = handleTransform.position;
            rb.rotation = handleTransform.rotation;
            return;
        }
        
        if (Vector3.Distance(rb.position, handleTransform.position) < maxPinchDistance)
            HandleTransforms();
        
        InvokeValue();
    }
    
    private void HandleTransforms()
    {
        Vector3 handleRotation = handleTransform.localRotation.eulerAngles;
        handleRotation.z = grabbableObject.transform.localRotation.eulerAngles.z;
        handleRotation.z = Mathf.Clamp(handleRotation.z, -zAmplitude, zAmplitude);
        Debug.LogError(handleRotation);
        handleTransform.localRotation = Quaternion.Euler(handleRotation);
    }
    
    private void InvokeValue()
    {
        float outputFloat = math.remap(zAmplitude, -zAmplitude, remapRange.x, remapRange.y, handleTransform.localRotation.eulerAngles.z);
        //Debug.LogError(outputFloat);
        onValueChanged.Invoke(outputFloat);
    }
}
