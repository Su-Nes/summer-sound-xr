using System;
using Mono.Cecil;
using Oculus.Interaction;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PinchSlider : MonoBehaviour
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
        zAmplitude = Mathf.Abs(grabbableObject.transform.localPosition.z);
        rb = grabbableObject.GetComponent<Rigidbody>();
        
        InvokeValue();
    }

    public void Update()
    {
        if (!rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
            rb.position = handleTransform.position;
            return;
        }
        
        if (Vector3.Distance(rb.position, handleTransform.position) < maxPinchDistance)
            HandleTransforms();

        // output float
        if (Vector3.Distance(rb.position, handleTransform.position) > .01f)
        {
            InvokeValue();
        }
    }

    private void InvokeValue()
    {
        float outputFloat = math.remap(zAmplitude, -zAmplitude, remapRange.x, remapRange.y, handleTransform.localPosition.z);

        onValueChanged.Invoke(outputFloat);
    }

    private void HandleTransforms()
    {
        Vector3 handlePosition = handleTransform.localPosition;
        handlePosition.z = grabbableObject.transform.localPosition.z;
        handlePosition.z = Mathf.Clamp(handlePosition.z, -zAmplitude, zAmplitude);
        
        handleTransform.localPosition = handlePosition;
    }
}