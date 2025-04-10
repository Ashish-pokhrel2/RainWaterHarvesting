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
    [SerializeField] private Material highlightMaterial;

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

        // UI text display
        if (buttonText != null)
        {
            buttonText.gameObject.SetActive(inRange && isLooking);
            buttonText.text = tapOn ? "Press E to Turn OFF" : "Press E to Turn ON";
        }

        // Highlight material
        if (highlightMaterial != null && originalMaterial != null)
        {
            tapRenderer.material = (inRange && isLooking) ? highlightMaterial : originalMaterial;
        }

        // Input to toggle tap
        if (inRange && isLooking && Input.GetKeyDown(KeyCode.E))
        {
            ToggleTap();
        }
    }

    public void ToggleTap()
    {
        tapOn = !tapOn;

        if (waterParticles != null)
        {
            if (tapOn) waterParticles.Play();
            else waterParticles.Stop();
        }

        if (audioSource != null)
        {
            audioSource.PlayOneShot(tapOn ? tapOnSound : tapOffSound);
        }

        Debug.Log("Tap is now " + (tapOn ? "ON" : "OFF"));
    }
}
