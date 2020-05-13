namespace Utility
{
    using UnityEngine;

    public class Input_KBM : InputBridge
    {
        [SerializeField] KeyCode switchKey = KeyCode.Tab;
        [SerializeField] KeyCode jumpKey = KeyCode.Space;

        private void Awake()
        {
            instance = this;
        }

        protected override void Update()
        {
            MoveHorizontal = Input.GetAxis("Horizontal");
            MoveVertical = Input.GetAxis("Vertical");
            LookHorizontal = Input.GetAxis("Mouse X");
            LookVertical = Input.GetAxis("Mouse Y");
            SwitchButton = Input.GetKey(switchKey);
            Jump = Input.GetKey(jumpKey);
        }

        public override void Haptic()
        {
            // nothing yet
        }
    }
}