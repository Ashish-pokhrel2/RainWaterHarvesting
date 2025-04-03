using UnityEngine;

public class WaterFilter : MonoBehaviour
{
    public Transform dirtyWater;  // Reference to the dirty water
    public Transform cleanWater;  // Reference to clean water at the tap
    public float filterSpeed = 0.5f;  // Speed of filtration

    private float filterProgress = 0f;  // Track filtration progress

    void Update()
    {
        // Simulate filtration process
        if (dirtyWater.localScale.y > 0)
        {
            float filterAmount = filterSpeed * Time.deltaTime;

            // Decrease dirty water level
            dirtyWater.localScale -= new Vector3(0, filterAmount, 0);

            // Increase clean water level
            cleanWater.localScale += new Vector3(0, filterAmount, 0);
        }
    }
}
