using UnityEngine;

public class ScaleDownWithTime : MonoBehaviour
{
    [SerializeField] private bool originGraphic = true;
    [SerializeField] private float graphicScaleMod = 1f;
    private float graphicDuration, t, graphicStartZ;
    
    private void Start()
    {
        if (originGraphic)
            GetComponent<Renderer>().enabled = false;
        else 
            GetComponent<Renderer>().enabled = true;
        
        graphicStartZ = transform.localScale.z; 
    }

    public void InitializeGraphic(float timeUntilBeat)
    {
        ScaleDownWithTime newGraphic = Instantiate(this, transform);
        newGraphic.graphicDuration = timeUntilBeat;
        newGraphic.originGraphic = false;
    }

    private void Update()
    {
        if (originGraphic)
            return;
        
        ScaleRhythmGraphic();
    }
    
    private void ScaleRhythmGraphic()
    {
        t += Time.deltaTime;
        Vector3 graphicScale = new Vector3(1f + (graphicDuration - t) * graphicScaleMod, 1f + (graphicDuration - t) * graphicScaleMod, graphicStartZ);
        transform.localScale = graphicScale;
        
        if (graphicDuration - t <= 0)
            Destroy(gameObject);
    }
}
