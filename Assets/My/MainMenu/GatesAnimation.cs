using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesAnimation : MonoBehaviour
{
 
    public int animNumber = 0;
    private int soundNumber = 0;

    public GameObject leftGates;
    public GameObject rightGates;
    public AudioClip[] gatesClips;

    private Animation leftGatesAnimation;
    private Animation rightGatesAnimation;

    private AudioSource leftGatesAudio;
    private AudioSource rightGatesAudio;

    private void Awake()
    {
        leftGatesAnimation = leftGates.GetComponent<Animation>();
        leftGatesAudio = leftGates.GetComponent<AudioSource>();

        rightGatesAnimation = rightGates.GetComponent<Animation>();
        rightGatesAudio = rightGates.GetComponent<AudioSource>();

    }
    void Update()
    {
        if (animNumber == 0)
        {
            StartCoroutine(playGatesAnim());
        }
    }
    IEnumerator playGatesAnim()
    {
        animNumber = Random.Range(1, 3); //рандомную циферку генерим и от нее пл€шем
        soundNumber = Random.Range(0, gatesClips.Length - 1);
        if (animNumber == 1)
        {
            Debug.Log("Play 1");
            leftGatesAnimation.Play("GatesAnimLeft1");
            rightGatesAnimation.Play("GatesAnimRight1");
            leftGatesAudio.PlayOneShot(gatesClips[soundNumber]);
            rightGatesAudio.PlayOneShot(gatesClips[soundNumber]);
            
        }
        if (animNumber == 2)
        {
            Debug.Log("PLay 2");
            leftGatesAnimation.Play("GatesAnimLeft2");
            rightGatesAnimation.Play("GatesAnimRight2");
            leftGatesAudio.PlayOneShot(gatesClips[soundNumber]);
            rightGatesAudio.PlayOneShot(gatesClips[soundNumber]);


        }
        if (animNumber == 3)
        {
            Debug.Log("Play3");
            leftGatesAnimation.Play("GatesAnimLeft3");
            rightGatesAnimation.Play("GatesAnimRight3");
            leftGatesAudio.PlayOneShot(gatesClips[soundNumber]);
            rightGatesAudio.PlayOneShot(gatesClips[soundNumber]);

        }
        yield return new WaitForSeconds(Random.Range(6f, 15f));
        animNumber = 0; //запускаем заново 
    }
}
