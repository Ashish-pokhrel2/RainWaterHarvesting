using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class TapControl : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float activationDistance = 3f;
    [SerializeField] private float lookAngleThreshold = 30f;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ParticleSystem waterParticles;
    
    [Header("Materials")]
    [SerializeField] private Material highlightMaterial; // Add this line
    
    [Header("Audio")]
    [SerializeField] private AudioClip tapOnSound;
    [SerializeField] private AudioClip tapOffSound;
    
    private AudioSource audioSource;
    private bool tapOn = false;
    private Material originalMaterial;
    private MeshRenderer tapRenderer;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        tapRenderer = GetComponentInChildren<MeshRenderer>();
        if (tapRenderer != null)
        {
            originalMaterial = tapRenderer.material;
        }
        
        if (waterParticles != null)
            waterParticles.Stop();

        if (buttonText != null)
            buttonText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerTransform || !playerCamera || !tapRenderer) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        bool inRange = distance <= activationDistance;
        
        Vector3 directionToTap = (transform.position - playerCamera.transform.position).normalized;
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToTap);
        bool isLooking = angle <= lookAngleThreshold;

        if (buttonText != null)
            buttonText.gameObject.SetActive(inRange && isLooking);

        // Only apply highlight if highlightMaterial is assigned
        if (highlightMaterial != null)
        {
            tapRenderer.material = (inRange) ? highlightMaterial : originalMaterial;
        }
    }

    public void ToggleTap()
    {
        if (!IsPlayerFacingTap()) return;

        tapOn = !tapOn;
        
        if (buttonText != null)
            buttonText.text = tapOn ? "Turn Tap OFF" : "Turn Tap ON";
        
        if (waterParticles != null)
        {
            if (tapOn) waterParticles.Play();
            else waterParticles.Stop();
        }
        
        if (audioSource != null)
            audioSource.PlayOneShot(tapOn ? tapOnSound : tapOffSound);
    }

    private bool IsPlayerFacingTap()
    {
        if (!playerTransform || !playerCamera) return false;
        
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > activationDistance) return false;
        
        Vector3 directionToTap = (transform.position - playerCamera.transform.position).normalized;
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToTap);
        return angle <= lookAngleThreshold;
    }
}