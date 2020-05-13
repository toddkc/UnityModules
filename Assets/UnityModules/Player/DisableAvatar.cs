namespace Utility
{
    using System.Collections;
    using UnityEngine;

    public class DisableAvatar : MonoBehaviour
    {
        [SerializeField] Transform cameraTransform;
        [SerializeField] Transform cameraTransformParent;
        [SerializeField] Transform godCamTransform;
        [SerializeField] CustomCharacterController controller;

        private bool isAvatarActive = true;
        private bool canSwitch = true;
        private WaitForSecondsRealtime delayTimer;
        private Vector3 cameraOffset;
        private Quaternion savedRotation;

        public static DisableAvatar instance;

        private void Awake()
        {
            instance = this;
            delayTimer = new WaitForSecondsRealtime(0.2f);
            cameraOffset = cameraTransform.localPosition;
        }

        private void Update()
        {
            if (InputBridge.instance.SwitchButton && canSwitch)
            {
                canSwitch = false;
                StartCoroutine(ResetSwitch());
                //if (isAvatarActive) GameStateManager.ChangeState(GameState.Pause);
                //else GameStateManager.ChangeState(GameState.Running);
            }
        }

        private IEnumerator ResetSwitch()
        {
            yield return delayTimer;
            canSwitch = true;
        }

        public void SetAvatar()
        {
            cameraTransform.parent = cameraTransformParent;
            cameraTransform.localPosition = cameraOffset;
            cameraTransform.localRotation = savedRotation;
            //controller.Activate();
            isAvatarActive = true;
        }

        public void SetGod()
        {
            savedRotation = cameraTransform.localRotation;
            //controller.Deactivate();
            cameraTransform.parent = godCamTransform;
            cameraTransform.localPosition = Vector3.zero;
            cameraTransform.localRotation = Quaternion.identity;
            isAvatarActive = false;
        }

        public void DisablePlayer()
        {
            //controller.DisableMovement();
            StartCoroutine(RespawnDelay());
        }
        private IEnumerator RespawnDelay()
        {
            yield return new WaitForSeconds(2);
            //GameStateManager.ChangeState(GameState.PlayerLost);
        }

        public void DisableSwitching()
        {
            canSwitch = false;
        }
    }
}