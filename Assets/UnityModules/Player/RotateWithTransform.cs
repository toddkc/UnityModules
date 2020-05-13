namespace Utility
{
    using UnityEngine;

    public class RotateWithTransform : MonoBehaviour
    {
        [SerializeField] Transform followTransform;
        [SerializeField] Transform playerTransform;
        [SerializeField] Vector3 offset = new Vector3(0, -0.25f, 0);
        [SerializeField] float rotateSpeed = 5f;
        private Transform thisTrans;

        private void Awake()
        {
            thisTrans = transform;

            if (followTransform == null) followTransform = Camera.main.transform;
            if (playerTransform == null) playerTransform = FindObjectOfType<CustomCharacterController>().transform;
        }

        private void LateUpdate()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            thisTrans.position = followTransform.position;
            if (playerTransform != null)
            {
                thisTrans.localPosition -= playerTransform.TransformVector(offset);
            }
            thisTrans.rotation = Quaternion.Lerp(thisTrans.rotation, Quaternion.Euler(0.0f, followTransform.rotation.eulerAngles.y, 0.0f), Time.deltaTime * rotateSpeed);
        }
    }
}