using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public Material waterMaterial;  // Assign the water material in the Inspector
    public float speed = 0.1f;      // Speed of water movement

    void Update()
    {
        if (waterMaterial != null) // Avoid errors if the material is missing
        {
            float offset = Time.time * speed;
waterMaterial.SetTextureOffset("_BaseMap", new Vector2(0, offset));
        }
    }
}
