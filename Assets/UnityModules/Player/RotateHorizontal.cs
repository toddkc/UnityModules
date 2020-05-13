namespace Utility
{
    using UnityEngine;

    public class RotateHorizontal : MonoBehaviour
    {
        [SerializeField] float speed = 100f;
        private float currentYAngle = 0f;
        private float oldHorizontalInput = 0f;
        private Transform thisTransform;

        private void Awake()
        {
            thisTransform = transform;
            currentYAngle = thisTransform.localRotation.eulerAngles.y;
            Rotate(0f);
        }

        private void Update()
        {
            HandleRotation();
        }

        private void HandleRotation()
        {
            //Get input values;
            float _inputHorizontal = InputBridge.instance.LookHorizontal;
            _inputHorizontal *= Time.deltaTime * speed;
            Rotate(_inputHorizontal);
        }

        private void Rotate(float _newHorizontalInput)
        {
            oldHorizontalInput = _newHorizontalInput;
            currentYAngle += oldHorizontalInput;
            thisTransform.localRotation = Quaternion.Euler(new Vector3(0, currentYAngle, 0));
        }
    }
}