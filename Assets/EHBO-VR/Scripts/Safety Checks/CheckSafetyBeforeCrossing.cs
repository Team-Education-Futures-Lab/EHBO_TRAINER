using UnityEngine;

public class CheckSafetyBeforeCrossing : MonoBehaviour
{
    public Transform playerHead; // Camera (VR headset)
    public float leftAngle = -60f; // graden naar links
    public float rightAngle = 60f; // graden naar rechts
    public float centerThreshold = 20f; // hoeveel speling we hebben voor "midden"

    private bool lookedLeft = false;
    private bool lookedRight = false;
    private bool readyToCross = false;
    private float startYRotation;

    public Animator carAnimator; // Animator van de auto
    private bool hasTriggered;

    void Start()
    {
        // We onthouden de startrotatie van de speler
        startYRotation = playerHead.eulerAngles.y;
        
    }

    void Update()
    {
        float currentY = playerHead.eulerAngles.y;
        float delta = Mathf.DeltaAngle(startYRotation, currentY);

        // Check of speler naar links kijkt
        if (delta < leftAngle)
            lookedLeft = true;

        // Check of speler naar rechts kijkt
        if (delta > rightAngle)
            lookedRight = true;

        // Als beide kanten bekeken zijn → mag oversteken
        if (lookedLeft && lookedRight && !readyToCross)
        {
            readyToCross = true;
            Debug.Log("Je hebt naar links en rechts gekeken! Je mag nu veilig oversteken.");
            // Hier kun je bv. een event aanroepen of de weg vrijgeven
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !readyToCross && !hasTriggered)
        {
            Debug.Log("Speler heeft de trigger geraakt zonder te kijken!");
            SetCarAnimationTo21Seconds();
            hasTriggered = true;
        }
    }


    public void SetCarAnimationTo21Seconds()
    {
        if (carAnimator != null)
        {
            float normalizedTime = 21.3f / 27f;
            carAnimator.Play(carAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, normalizedTime);
            carAnimator.Update(0f);
            Debug.Log("Auto animatie gezet op 21 seconden van 27 seconden.");
        }
    }

    public bool HasLookedBothWays()
    {
        return readyToCross;
    }

    
}
