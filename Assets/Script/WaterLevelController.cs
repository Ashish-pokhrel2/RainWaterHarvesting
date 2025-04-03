using UnityEngine;

public class WaterLevelController : MonoBehaviour
{
    public Transform waterObject;  // Reference to the water object
    public float targetHeight = 5f;  // Desired final water height
    public float fillSpeed = 0.5f;  // Speed at which water rises

    private float currentHeight = 0f;  // Current water height

    void Update()
    {
        // Gradually increase the water height until it reaches the target
        if (currentHeight < targetHeight)
        {
            currentHeight += fillSpeed * Time.deltaTime;
            waterObject.localScale = new Vector3(waterObject.localScale.x, currentHeight, waterObject.localScale.z);
        }
    }
}
