using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace MimicSpace
{
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.3f;
        public float speed = 15f;
        public float velocityLerpCoef = 8f;
        public float timeToChase = 5f;
        public float chaseDistance = 10f;
        public float losePlayerDistance = 15f;

        private Transform playerTransform;
        private float remainingChaseTime;
        private NavMeshAgent agent;
        private Mimic myMimic;

        private float regularSpeed = 0f;
        private float chasingSpeed = 10f;

        private float regularAngularSpeed = 0f;
        private float chasingAngularSpeed = 2080f;

        public GameObject[] points;
        private int currentPoint = 0;

        public AudioSource audioSource;


        private void Start()
        {
            myMimic = GetComponent<Mimic>();
            agent = GetComponent<NavMeshAgent>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            regularSpeed = agent.speed;
            regularAngularSpeed = agent.angularSpeed;
        }

        void Update()
        {
            if (remainingChaseTime > 0)
            {
                ChasePlayer();
            }
            else
            {
                Wander();
                CheckPlayerVisibility();
            }

            // �������� �������� ������
            myMimic.velocity = agent.velocity;

            // ��������� ������� �������
            transform.position = agent.nextPosition;
            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }

        private void Wander()
        {
            audioSource.Stop();
            agent.speed = regularSpeed;
            agent.angularSpeed = regularAngularSpeed;

            // ���� �������� ������� �����, ��������� � ���������
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                currentPoint = (currentPoint + 1) % points.Length;
                Debug.Log(currentPoint);
                agent.SetDestination(points[currentPoint].transform.position);
            }
        }

        private void ChasePlayer()
        {
            if(agent.remainingDistance < 3f)
            {
                killPlayer();
            }
            //TODO �������, ������� � ������ ����� ������
            if (!audioSource.isPlaying)
            {
            audioSource.Play();
            }
            // ������ ������� ������ ��� ������� ����� ��� ���������
            agent.SetDestination(playerTransform.position);
            agent.speed = chasingSpeed;
            agent.angularSpeed = chasingAngularSpeed;
            // ��������� ����� ������������� ������
            remainingChaseTime -= Time.deltaTime;
        }

        private void CheckPlayerVisibility()
        {
            RaycastHit hit;
            Debug.DrawLine(transform.position, playerTransform.position, color:Color.red);
         
            if (Physics.Linecast(transform.position, playerTransform.position, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    // ������ ������� ������ ��� ������� ����� ��� ���������
                    agent.SetDestination(playerTransform.position);

                    // �������� ������������� ������
                    remainingChaseTime = timeToChase;

                }
            }
        }
        private void killPlayer()
        {
            //TODO �������� ������, ����� ������ � �������� (����� �� ���� - restart)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}