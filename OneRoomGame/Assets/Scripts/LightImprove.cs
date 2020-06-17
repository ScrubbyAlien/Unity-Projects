using UnityEngine;

public class LightImprove : MonoBehaviour
{
    Light thisLight;
    public float bias;

    void Start()
    {
        thisLight = GetComponent<Light>();
    }

    void Update()
    {
        thisLight.shadowBias = bias;
    }
}
