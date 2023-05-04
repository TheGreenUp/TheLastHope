using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public enum WalkParamets
    {
        walk = 1,
        sprint = 2,
        crouch = 3
    }

    [Header("Movemnt parametrs")] public WalkParamets walkpar;

    [Header("Entity")]
    [SerializeField] private CharacterController entity;
    [SerializeField] private GameObject obj;

    [Header("Footsteps Parametrs")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private AudioClip[] bedClips = default;
    private float footstepTimer = 0;


    private float currentOffset;
    private Vector3 currentPosition;

    // Update is called once per frame
    void Update()
    {
        switch (walkpar)
        {
            case WalkParamets.walk: { currentOffset = baseStepSpeed; break; }
            case WalkParamets.sprint: { currentOffset = baseStepSpeed * sprintStepMultiplier; break; }
            case WalkParamets.crouch: { currentOffset = baseStepSpeed * crouchStepMultiplier; break; }
        }
        HandleFootsteps(currentOffset);
    }

    private void HandleFootsteps(float GetCurrentOffset)
    {
        if (!entity.isGrounded) return;

        if (obj.transform.position == currentPosition) return;

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            if (Physics.Raycast(entity.transform.position, Vector3.down, out RaycastHit hit, 2))
            {
                switch (hit.collider.tag)
                {
                    case "footsteps/WOOD":
                        footstepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                        break;
                    case "footsteps/GRASS":
                        footstepAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);
                        break;
                    case "footsteps/METAL":
                        footstepAudioSource.PlayOneShot(metalClips[Random.Range(0, metalClips.Length - 1)]);
                        break;
                    case "footsteps/BED":
                        footstepAudioSource.PlayOneShot(bedClips[Random.Range(0, bedClips.Length - 1)]);
                        break;
                    default:
                        footstepAudioSource.volume += 0.5f;
                        footstepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);//если нет никакой поверхности, пускай будет дерево
                        footstepAudioSource.volume -= 0.5f;

                        break;
                }
            }
            currentPosition = obj.transform.position;
            footstepTimer = GetCurrentOffset;
        }
    }

}
