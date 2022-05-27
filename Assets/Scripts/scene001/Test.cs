using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject player;
    private Renderer render;
    void Start()
    {
        render = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!render.isVisible) transform.LookAt(player.transform);
    }
}
