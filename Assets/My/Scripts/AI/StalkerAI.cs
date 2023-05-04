using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class StalkerAI : MonoBehaviour
{
    [Range(0, 360)] public float ViewAngle = 90f;
    public float ViewDistance = 15f;
    public float DetectionDistance = 3f;
    public Transform EnemyEye;
    public Transform Target;

    private NavMeshAgent agent;
    private float rotationSpeed;
    private Transform agentTransform;

    public GameObject clown;
    // ������ ��������� ����� ����������
    public Transform[] goals;
    // ���������� �� ������� ���������� ������������ � �����
    public float distanceToChangeGoal;
    // ����� ������� ������� �����
    int currentGoal = 0;

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        rotationSpeed = agent.angularSpeed;
        agentTransform = agent.transform;
        agent.destination = goals[0].position;
    }
    private void Update()
    {
        Potrul();
        float distanceToPlayer = Vector3.Distance(Target.transform.position, agent.transform.position);
        if (distanceToPlayer <= DetectionDistance || IsInView())
        {
            RotateToTarget();
            MoveToTarget();
        }
        DrawViewState();
    }

    private bool IsInView() // true ���� ���� �����
    {

        /*  float realAngle = Vector3.Angle(EnemyEye.forward, Target.position - EnemyEye.position);
          RaycastHit hit;
          if (Physics.Raycast(EnemyEye.transform.position, Target.position - EnemyEye.position, out hit, ViewDistance))
          {
              if (realAngle < ViewAngle / 2f && Vector3.Distance(EnemyEye.position, Target.position) <= ViewDistance && hit.transform == Target.transform)
              {
                  return true;
              }
          }
          return false;
  */
        RaycastHit hit;
        Debug.DrawLine(transform.position, Target.position, color: Color.red);

        if (Physics.Linecast(transform.position, Target.position, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                // ������ ������� ������ ��� ������� ����� ��� ���������
                return true;
            }
        }
        return false;


    }


    private void RotateToTarget() // ������������ � ������� ���� �� ��������� rotationSpeed
    {
        Vector3 lookVector = Target.position - agentTransform.position;
        lookVector.y = 0;
        if (lookVector == Vector3.zero) return;
        agentTransform.rotation = Quaternion.RotateTowards
            (
                agentTransform.rotation,
                Quaternion.LookRotation(lookVector, Vector3.up),
                rotationSpeed * Time.deltaTime
            );

    }
    private void MoveToTarget() // ������������ ����� �������� � ����
    {
        clown.GetComponent<Animator>().SetBool("crouch", false);
        clown.GetComponent<Animator>().SetBool("run", true);
        agent.SetDestination(Target.position);
    }


    private void DrawViewState()
    {
        Vector3 left = EnemyEye.position + Quaternion.Euler(new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
        Vector3 right = EnemyEye.position + Quaternion.Euler(-new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
        Debug.DrawLine(EnemyEye.position, left, Color.yellow);
        Debug.DrawLine(EnemyEye.position, right, Color.yellow);
    }

    public void Potrul()
    {
        clown.GetComponent<Animator>().SetBool("run", false);
        clown.GetComponent<Animator>().SetBool("crouch", true);
        // �������� �� ��, ���������� �� ������ ����� � ����
        if (agent.remainingDistance < distanceToChangeGoal)
        {
            // ����� ����� �� ���������
            currentGoal++;
            if (currentGoal == goals.Length) currentGoal = 0;
            agent.destination = goals[currentGoal].position;


        }
    }
}
