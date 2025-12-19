using UnityEngine;

public class PlayerTransitionToTutorial : MonoBehaviour
{
    public MonoBehaviour scriptToActivate; // script dat de NPC laat lopen
    public Animator animator; // Animator van de NPC
    public bool isInTransition = false;
    public ScreenFade screenFade; // Referentie naar het ScreenFade-script

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartTransition()
    {
        NPCTransition();
        
    }
    
    private void NPCTransition()
    {
    
        // Script meteen activeren
        if (scriptToActivate != null)
            scriptToActivate.enabled = true;

        // Optioneel: animatie direct starten
        if (animator != null)
            animator.SetTrigger("Walk"); // alleen als je een trigger gebruikt

        isInTransition = true;
        gameObject.SetActive(true);
    }
}
