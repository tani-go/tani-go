using System.Collections;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    private Rigidbody rb;
    public Transform PlayerOrientation;
    public float walkspeed;
    public Animator anim;

    private Vector3 moveDirection;
    private bool grounded = true;
    private bool isWalking = false;

    public float walkDuration;
    public float idleDuration;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerOrientation = GetComponent<Transform>();

        StartCoroutine(RandomWalkRoutine());
    }

    void FixedUpdate()
    {
        if (grounded && isWalking)
        {
            rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
        }

        // Rotasi hanya saat bergerak
        if (moveDirection != Vector3.zero && isWalking)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime);
        }
    }

    IEnumerator RandomWalkRoutine()
    {
        while (true)
        {
            // Gerak random di sumbu XZ
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            moveDirection = PlayerOrientation.forward * randomDir.y + PlayerOrientation.right * randomDir.x;

            if (grounded)
            {
                isWalking = true;
                anim.SetBool("Walk", true);
            }

            yield return new WaitForSeconds(walkDuration);

            // Berhenti jalan
            isWalking = false;
            anim.SetBool("Walk", false);
            moveDirection = Vector3.zero;

            // Hentikan gerak fisik juga
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            yield return new WaitForSeconds(idleDuration);
        }
    }

    public void groundedchanger()
    {
        grounded = true;
    }
}