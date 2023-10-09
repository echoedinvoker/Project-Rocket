using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem explodeParticle;
    bool isTransition = false;
    bool isdisableCollision = false;

    AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        ResponseToDebugKeys();
    }

    void OnCollisionEnter(Collision other) {

        if (isTransition || isdisableCollision) { return; }

        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("This object is friendly"); 
                break;
            case "Finish":
                StartFinishSequence();
                break;
            case "InvisibleWall":
                break;
            default:
                StartCrashSequence();
                break;

        }
    }

    void StartFinishSequence()
    {
        isTransition = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("CycleNextLevel", levelLoadDelay);
    }

    void ResponseToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CycleNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isdisableCollision = !isdisableCollision;
        }

    }

    void StartCrashSequence()
    {
        isTransition = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        explodeParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadCurrentLevel", levelLoadDelay);
    }

    void CycleNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            LoadFistLevel();
        }
        else
        {
            LoadNextLevel();
        }
    }

    void ReloadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void LoadFistLevel()
    {
        SceneManager.LoadScene(0);
    }
}
