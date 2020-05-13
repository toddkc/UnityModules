namespace Utility
{
    using UnityEngine;

    public class RotateVertical : MonoBehaviour
    {
        [SerializeField] float speed = 100f;
        [Range(0f, 90f)]
        [SerializeField] float upperVerticalLimit = 60f;
        [Range(0f, 90f)]
        [SerializeField] float lowerVerticalLimit = 60f;
		private float currentXAngle = 0f;
		private float oldVerticalInput = 0f;
        private Transform thisTransform;

		private void Awake()
		{
			thisTransform = transform;
			currentXAngle = thisTransform.localRotation.eulerAngles.x;
			Rotate(0f);
		}

        private void Update()
        {
            HandleRotation();
        }

		private void HandleRotation()
		{
			float _inputVertical = InputBridge.instance.LookVertical;
            _inputVertical *= Time.deltaTime * speed;
            Rotate(_inputVertical);
		}

		protected void Rotate(float _newVerticalInput)
		{
			oldVerticalInput = _newVerticalInput;
			currentXAngle += oldVerticalInput;
			currentXAngle = Mathf.Clamp(currentXAngle, -upperVerticalLimit, lowerVerticalLimit);
			thisTransform.localRotation = Quaternion.Euler(new Vector3(currentXAngle, 0, 0));
		}
	}
}