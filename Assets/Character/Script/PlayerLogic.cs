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

    private Coroutine walkRoutine;
    private bool isWaving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerOrientation = GetComponent<Transform>();

        walkRoutine = StartCoroutine(RandomWalkRoutine());
    }

    void FixedUpdate()
    {
        if (grounded && isWalking && !isWaving)
        {
            rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
        }

        // Rotasi hanya saat bergerak
        if (moveDirection != Vector3.zero && isWalking && !isWaving)
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

    void OnMouseDown()
    {
            Debug.Log("Karakter diklik!");

        if (!isWaving)
        {
            isWaving = true;

            //menghentikan jalan random
            if(walkRoutine != null)
            StopCoroutine(walkRoutine);
            walkRoutine = null; // <- Ini wajib agar bisa jalan lagi nanti


            //set semua ke status idle
            isWalking = false;
            anim.SetBool("Walk", false);
            moveDirection = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            //set wave
            anim.SetTrigger("Wave");
            //kembali idle
            Invoke(nameof(BackToIdle),3.1f);
        }
    }

    void BackToIdle()
    {
        isWaving = false;
        anim.ResetTrigger("Wave");
        rb.isKinematic = false;
        if (walkRoutine == null)
        {
            walkRoutine = StartCoroutine(RandomWalkRoutine());
        }
    }


    public void groundedchanger()
    {
        grounded = true;
    }
}