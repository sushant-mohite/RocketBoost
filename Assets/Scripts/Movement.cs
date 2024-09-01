using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationForce = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoostParticles;
    [SerializeField] ParticleSystem leftBoostParticles;
    [SerializeField] ParticleSystem rightBoostParticles;
    
    Rigidbody rb;
    AudioSource audioSource; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            NoThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoostParticles.isPlaying)
        {
            mainBoostParticles.Play();
        }
    }
    
    void NoThrusting()
    {
        audioSource.Stop();
        mainBoostParticles.Stop();
    }

    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationForce);
        if (!leftBoostParticles.isPlaying)
        {
            leftBoostParticles.Play();
        }
    }
    
    void RotateRight()
    {
        ApplyRotation(-rotationForce);
        if (!rightBoostParticles.isPlaying)
        {
            rightBoostParticles.Play();
        }
    }

    void ApplyRotation(float rotationForce)
    {
        rb.freezeRotation = true; // Freezing physics rotation so player can rotate manually.
        transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over.
    }
    
    void StopRotating()
    {
        leftBoostParticles.Stop();
        rightBoostParticles.Stop();
    }

}



