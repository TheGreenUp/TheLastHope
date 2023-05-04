using UnityEngine;

public class MimicFootsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public float footstepInterval = 0.5f;
    public float footstepVolume = 1.0f;

    private AudioSource audioSource;
    private float footstepTimer = 0.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ѕроигрываем звук шагов, если персонаж движетс€
        if ( footstepTimer > footstepInterval)
        {
            // ¬ыбираем случайный звук шагов из массива
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(footstepSound, footstepVolume);
            footstepTimer = 0.0f;
        }

        footstepTimer += Time.deltaTime;
    }
}