using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 0f;
    [SerializeField] float rotationThrust = 0f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainTrustParticles;
    [SerializeField] ParticleSystem rightSideParticles;
    [SerializeField] ParticleSystem leftSideParticles;
    [SerializeField] ParticleSystem inSideParticles;
    [SerializeField] ParticleSystem outSideParticles;


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
        ProcessTrust();
        ProcessRotate();
    }

    void ProcessTrust() 
    { 
        if (Input.GetKey(KeyCode.Space)) 
        { 
            StartThrusting(); 
        } 
        else 
        { 
            StopThrusting(); 
        }
    }

    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            RotateIn();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            RotateOut();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting() 
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainTrustParticles.isPlaying)
        {
            mainTrustParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainTrustParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(rotationThrust);
        if (!rightSideParticles.isPlaying)
        {
          rightSideParticles.Play();
        }
    }
    
    void RotateLeft()
    {
        ApplyRotation(-rotationThrust);
        if (!leftSideParticles.isPlaying)
        {
          leftSideParticles.Play();
        }
    }

    void RotateIn()
    {
        ApplyZRotation(rotationThrust);
        if (!leftSideParticles.isPlaying)
        {
          leftSideParticles.Play();
        }
    }
    void RotateOut()
    {
        ApplyZRotation(-rotationThrust);
        if (!rightSideParticles.isPlaying)
        {
          rightSideParticles.Play();
        }
    }
    void StopRotating()
    {
        // rb.constraints = RigidbodyConstraints.FreezeRotationX |
        //   RigidbodyConstraints.FreezeRotationY |
        //   RigidbodyConstraints.FreezePositionZ;
        leftSideParticles.Stop();
        rightSideParticles.Stop();
    }

    void ApplyRotation(float rotationTrustThisFrame)
    {
        // rb.constraints = RigidbodyConstraints.FreezeRotationX |
        //                  RigidbodyConstraints.FreezeRotationY |
        //                  RigidbodyConstraints.FreezeRotationZ |
        //                  RigidbodyConstraints.FreezePositionZ;
        transform.Rotate(Vector3.forward * rotationTrustThisFrame * Time.deltaTime);
    }
    void ApplyZRotation(float rotationTrustThisFrame)
    {
        // rb.constraints = RigidbodyConstraints.FreezeRotationX |
        //                  RigidbodyConstraints.FreezeRotationY |
        //                  RigidbodyConstraints.FreezeRotationZ |
        //                  RigidbodyConstraints.FreezePositionZ;
        transform.Rotate(Vector3.right * rotationTrustThisFrame * Time.deltaTime);
    }
}
