namespace Utility
{
    using UnityEngine;

    public abstract class InputBridge : MonoBehaviour
    {
        private void Start()
        {
            MoveHorizontal = 0;
            MoveVertical = 0;
            LookHorizontal = 0;
            LookVertical = 0;
            RotateAxis = Vector2.zero;
            SwitchButton = false;
            SnapLeft = false;
            SnapRight = false;
            Jump = false;
        }

        public static InputBridge instance { get; protected set; }
        public float MoveHorizontal { get; protected set; }
        public float MoveVertical { get; protected set; }
        public float LookHorizontal { get; protected set; }
        public float LookVertical { get; protected set; }
        public Vector2 RotateAxis { get; protected set; }
        public bool SwitchButton { get; protected set; }
        public bool SnapLeft { get; protected set; }
        public bool SnapRight { get; protected set; }
        public bool Jump { get; protected set; }

        public abstract void Haptic();
        protected abstract void Update();
    }
}