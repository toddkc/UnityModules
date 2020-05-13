namespace Utility
{
    using UnityEngine;

    public class CustomCharacterController : MonoBehaviour
    {
        [Header("Controller Settings")]
        [Tooltip("Use either controller or HMD for forward direction")]
        [SerializeField] Transform forwardTransform = default;
        [SerializeField] float moveSpeed = 7f;
        [SerializeField] float jumpSpeed = 12f;
        [SerializeField] float gravity = 30f;
        [SerializeField] LayerMask GroundLayers = default;

        /// <summary>
        /// these settings will override the actual collider settings, so use this NOT the settings on the collider component
        /// </summary>
        [Header("Collider Settings")]
        [SerializeField] float colliderHeight = 2f;
        [SerializeField] float colliderRadius = 0.5f;
        [Range(0f, 1f)] [SerializeField] float stepHeight = 0.25f;
        [SerializeField] Vector3 colliderOffset = new Vector3(0f, 0.5f, 0f);

        private Transform tr;
        private CapsuleCollider col;
        private Rigidbody rig;
        private float verticalSpeed = 0f;
        private float baseLength = 0f;
        private float castLength = 0f;
        private bool isGrounded = false;
        private Vector3 velocityAdjustment = Vector3.zero;
        private Vector3 origin = Vector3.zero;

        private enum Direction
        {
            Forward,
            Right,
            Up,
            Backward,
            Left,
            Down
        }
        private Direction direction;

        private void Awake()
        {
            IsActive = true;
            tr = transform;
            rig = GetComponent<Rigidbody>();
            col = GetComponent<CapsuleCollider>();
            rig.freezeRotation = true;
            rig.useGravity = false;
            SetupCollider();
            CalibrateRaycast();
        }

        /// <summary>
        /// This is used to show the updated Collider in the editor
        /// </summary>
        private void OnValidate()
        {
            if (gameObject.activeInHierarchy) SetupCollider();
        }

        private void FixedUpdate()
        {
            GroundCheck();
            Vector3 velocity = Vector3.zero;
            if (!IsActive) return;
            velocity += GetMoveDirection() * moveSpeed;
            if (!isGrounded)
            {
                verticalSpeed -= gravity * Time.deltaTime;
            }
            else
            {
                if (verticalSpeed <= 0f) verticalSpeed = 0f;
            }
            if (isGrounded && InputBridge.instance.Jump)
            {
                verticalSpeed = jumpSpeed;
                isGrounded = false;
            }
            velocity += tr.up * verticalSpeed;
            SetVelocity(velocity);
        }

        /// <summary>
        /// Update if player is grounded
        /// </summary>
        private void GroundCheck()
        {
            velocityAdjustment = Vector3.zero;
            if (isGrounded)
            {
                castLength = baseLength + colliderHeight * stepHeight;
            }
            else
            {
                castLength = baseLength;
            }
            RaycastHit hit;
            if (Physics.Raycast(tr.TransformPoint(origin), GetDirection(), out hit, castLength, GroundLayers, QueryTriggerInteraction.Ignore))
            {
                isGrounded = true;
                float upper = (colliderHeight * (1.0f - stepHeight)) * 0.5f;
                float middle = upper + colliderHeight * stepHeight;
                float distance = middle - hit.distance;
                velocityAdjustment = tr.up * (distance / Time.deltaTime);
            }
            else
            {
                isGrounded = false;
            }
        }

        /// <summary>
        /// Translate the enum into a transform direction
        /// </summary>
        private Vector3 GetDirection()
        {
            switch (direction)
            {
                case Direction.Forward:
                    return tr.forward;
                case Direction.Backward:
                    return -tr.forward;
                case Direction.Right:
                    return tr.right;
                case Direction.Left:
                    return -tr.right;
                case Direction.Up:
                    return tr.up;
                case Direction.Down:
                    return -tr.up;
                default:
                    return Vector3.one;
            }
        }

        /// <summary>
        /// Use input to calculate direction to move
        /// </summary>
        private Vector3 GetMoveDirection()
        {
            Vector3 direction = Vector3.zero;
            direction += Vector3.ProjectOnPlane(forwardTransform.right, tr.up).normalized * InputBridge.instance.MoveHorizontal;
            direction += Vector3.ProjectOnPlane(forwardTransform.forward, tr.up).normalized * InputBridge.instance.MoveVertical;
            direction.Normalize();
            return direction;
        }

        /// <summary>
        /// Set the attached collider based on the controller variables
        /// </summary>
        private void SetupCollider()
        {
            if (col == null) col = GetComponent<CapsuleCollider>();
            col.height = colliderHeight;
            col.center = colliderOffset * colliderHeight;
            col.radius = colliderRadius * 0.5f;
            col.center = col.center + new Vector3(0.0f, (stepHeight * col.height) * 0.5f, 0.0f);
            col.height *= (1.0f - stepHeight);
        }

        /// <summary>
        /// Set the base raycast variables
        /// </summary>
        private void CalibrateRaycast()
        {
            origin = tr.InverseTransformPoint(col.bounds.center);
            direction = Direction.Down;
            float length = 0.0f;
            length += (colliderHeight * (1.0f - stepHeight)) * 0.5f;
            length += colliderHeight * stepHeight;
            baseLength = length * 1.001f;
        }

        /// <summary>
        /// Set the velocity of the attached rigidbody
        /// </summary>
        public void SetVelocity(Vector3 velocity)
        {
            rig.velocity = velocity + velocityAdjustment;
        }

        public void Activate()
        {
            rig.isKinematic = false;
            IsActive = true;
        }

        public void Deactivate()
        {
            rig.isKinematic = true;
            IsActive = false;
        }

        /// <summary>
        /// Is the controller active?
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///  Get/Set the controller gravity value
        /// </summary>
        public float Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }

        /// <summary>
        /// Get the calculated collider height value
        /// </summary>
        public float Height
        {
            get { return colliderHeight; }
        }
    }
}