using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;
    public AudioClip doonClip;
    public AudioClip harpClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayAudio(string soundName)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is missing on SoundManager!");
            return;
        }

        AudioClip clipToPlay = null;

        switch (soundName)
        {
            case "Doon":
                clipToPlay = doonClip;
                break;
            case "Harp":
                clipToPlay = harpClip;
                break;
            default:
                Debug.LogWarning("Sound name not found: " + soundName);
                return;
        }

        audioSource.PlayOneShot(clipToPlay);
    }
}
