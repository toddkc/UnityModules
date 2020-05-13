namespace Utility
{
    using System.Collections;
    using UnityEngine;

    public class GravityFlip : MonoBehaviour
    {
        [SerializeField] float delayTime = 0.5f;
        [SerializeField] float rotationSpeed = 5f;
        private WaitForSeconds delay;
        private Transform tr;
        private Transform player;
        private Quaternion rotationTarget = Quaternion.identity;
        private bool isRotating = false;

        private void Awake()
        {
            tr = transform;
            delay = new WaitForSeconds(delayTime);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.GetComponent<Rigidbody>() == null || isRotating) return;
            SwitchDirection(tr.forward, col.GetComponent<Rigidbody>());
        }

        private void SwitchDirection(Vector3 _newUpDirection, Rigidbody _controller)
        {
            if (isRotating) return;
            float _angleThreshold = 0.001f;
            float _angleBetweenUpDirections = Vector3.Angle(_newUpDirection, _controller.transform.up);
            if (_angleBetweenUpDirections < _angleThreshold) return;
            AudioPlayer.PlayFlipSound();
            Transform _transform = _controller.transform;
            if (player == null) player = _transform;
            Quaternion _rotationDifference = Quaternion.FromToRotation(_transform.up, _newUpDirection);
            rotationTarget = _rotationDifference * _transform.rotation;
            StartCoroutine(FlipDelay());
        }

        private IEnumerator FlipDelay()
        {
            yield return delay;
            isRotating = true;
        }

        private void FixedUpdate()
        {
            if (!isRotating || rotationTarget == Quaternion.identity) return;
            if (player.rotation != rotationTarget)
            {
                player.rotation = Quaternion.Lerp(player.rotation, rotationTarget, Time.deltaTime * rotationSpeed);
                return;
            }
            ResetRotate();
        }

        private void ResetRotate()
        {
            Debug.Log("RESET ROTATE");
            player.rotation = rotationTarget;
            rotationTarget = Quaternion.identity;
            isRotating = false;
        }
    }
}