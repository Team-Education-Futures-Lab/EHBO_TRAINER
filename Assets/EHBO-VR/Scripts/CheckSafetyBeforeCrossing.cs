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

    public bool HasLookedBothWays()
    {
        return readyToCross;
    }
}
