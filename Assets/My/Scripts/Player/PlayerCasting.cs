
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    public static float DistanceFromTarget;
    public float toTarget;  
    void Update()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit)) {
            toTarget = Hit.distance;
            DistanceFromTarget = toTarget;
        }
    }
}
