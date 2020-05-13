namespace Utility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] List<Transform> waypoints = new List<Transform>();

        [SerializeField] bool moveOnStart = true;
        [SerializeField] bool loop = true;
        [SerializeField] bool reverseDirection = false;
        [SerializeField] float movementSpeed = 10f;
        [SerializeField] float waitTime = 1f;

        private List<Rigidbody> bodiesToMove = new List<Rigidbody>();
        private bool isWaiting = false;
        private int currentWaypointIndex = 0;
        private Transform currentWaypoint;
        private Rigidbody rig;

        private void Start()
        {
            rig = GetComponent<Rigidbody>();
            rig.freezeRotation = true;
            rig.useGravity = false;
            rig.isKinematic = true;

            if (waypoints.Count <= 0)
            {
                Debug.LogWarning("No waypoints have been assigned to 'MovingPlatform'!");
            }
            else
            {
                currentWaypoint = waypoints[currentWaypointIndex];
            }

            if (moveOnStart)
            {
                StartCoroutine(WaitRoutine());
                StartCoroutine(LateFixedUpdate());
            }
        }

        public void StartMovement()
        {
            StartCoroutine(WaitRoutine());
            StartCoroutine(LateFixedUpdate());
        }

        private IEnumerator WaitRoutine()
        {
            WaitForSeconds _waitInstruction = new WaitForSeconds(waitTime);
            while (true)
            {
                if (isWaiting)
                {
                    yield return _waitInstruction;
                    isWaiting = false;
                }
                yield return null;
            }
        }

        private IEnumerator LateFixedUpdate()
        {
            WaitForFixedUpdate _instruction = new WaitForFixedUpdate();
            while (true)
            {
                yield return _instruction;
                MovePlatform();
            }
        }

        private void MovePlatform()
        {
            if (waypoints.Count <= 0 || isWaiting) return;
            Vector3 _toCurrentWaypoint = currentWaypoint.position - transform.position;
            Vector3 _movement = _toCurrentWaypoint.normalized;
            _movement *= movementSpeed * Time.deltaTime;
            if (_movement.magnitude >= _toCurrentWaypoint.magnitude || _movement.magnitude == 0f)
            {
                rig.transform.position = currentWaypoint.position;
                UpdateWaypoint();
            }
            else
            {
                rig.transform.position += _movement;
            }

            for (int i = 0; i < bodiesToMove.Count; i++)
            {
                bodiesToMove[i].MovePosition(bodiesToMove[i].position + _movement);
            }
        }

        private void UpdateWaypoint()
        {
            if (reverseDirection) currentWaypointIndex--;
            else currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Count)
            {
                if (loop)
                {
                    currentWaypointIndex = 0;
                }
                else
                {
                    StopAllCoroutines();
                    return;
                }
            }

            if (currentWaypointIndex < 0)
                currentWaypointIndex = waypoints.Count - 1;
            currentWaypoint = waypoints[currentWaypointIndex];
            isWaiting = true;
        }
    }
}