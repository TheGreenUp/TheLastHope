using UnityEngine;
using System.Collections;


public class StartMonsterRunaway : MonoBehaviour
{
    public GameObject monster;

    private bool isDestroyed = false;
    public void OnTriggerEnter(Collider other)
    {
        if (!isDestroyed) StartCoroutine(MonsterRunaway());
    }
    IEnumerator MonsterRunaway()
    {
        monster.SetActive(true);
        yield return new WaitForSeconds(10f);
        Destroy(monster);
        isDestroyed = true;
    }
}
