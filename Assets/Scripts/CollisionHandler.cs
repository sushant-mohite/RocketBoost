using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f; 
    [SerializeField] AudioClip levelSuccess;
    [SerializeField] AudioClip crashed;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool disableCollision = false;

    
    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            disableCollision = !disableCollision;   // toggle between collision on or off
        }
        

    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || disableCollision) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
        
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(levelSuccess);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentSceneIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }

    
    void StartCrashSequence ()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashed);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
