using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public float speed = 2f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Beweeg de hond vooruit
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Zet de loop-animatie aan
        animator.Play("Walk");
    }
}

