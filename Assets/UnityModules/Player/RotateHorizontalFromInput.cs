namespace Utility
{
    using UnityEngine;

    public class RotateHorizonotalFromInput : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [SerializeField] float rotationSpeed = 60.0f;
        [SerializeField] float rotationAmount = 1.5f;
        [SerializeField] float rotationRatchet = 45.0f;
        [SerializeField] float rotationMultiplier = 1.0f;
        public bool useSnapRotation = true;
        public bool allowRotation = true;

        private Transform trToRotate;
        private bool readyToSnapTurn = true;

        private void Awake()
        {
            trToRotate = transform;
        }

        private void Update()
        {
            ProcessRotation();
        }

        /// <summary>
        /// rotate transform based on user input
        /// </summary>
        private void ProcessRotation()
        {
            if (!allowRotation) return;
            Vector3 euler = trToRotate.localRotation.eulerAngles;
            float rotateInfluence = rotationSpeed * Time.deltaTime * rotationAmount * rotationMultiplier;

            if (useSnapRotation)
            {
                if (InputBridge.instance.SnapLeft)
                {
                    if (readyToSnapTurn)
                    {
                        euler.y -= rotationRatchet;
                        readyToSnapTurn = false;
                    }
                }
                else if (InputBridge.instance.SnapRight)
                {
                    if (readyToSnapTurn)
                    {
                        euler.y += rotationRatchet;
                        readyToSnapTurn = false;
                    }
                }
                else
                {
                    readyToSnapTurn = true;
                }
            }
            else
            {
                Vector2 secondaryAxis = InputBridge.instance.RotateAxis;
                euler.y += secondaryAxis.x * rotateInfluence;
            }
            trToRotate.localRotation = Quaternion.Euler(euler);
        }
    }
}
