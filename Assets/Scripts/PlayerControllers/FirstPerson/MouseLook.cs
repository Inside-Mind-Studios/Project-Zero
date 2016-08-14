using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace EntroMinds.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        [Tooltip("Allow/Disallow 360 degree vertical rotation")]
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        [Tooltip("Smooth the look rotation")]
        public bool smooth;
        public float smoothTime = 5f;
        [Tooltip("Set Auto Lock Cursor as enabled (Hint: Enable for editor, disable for production)")]
        public bool lockCursor = true;


        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private bool m_cursorIsLocked = true;

        /// <summary>
        /// Initializes the Character and Camera target rotations based on 
        /// Transform arguments.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="camera"></param>
        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }

        /// <summary>
        /// Uses CrossPlatformInputManager to get corresponding X and Y mouse values
        /// to determine a new rotation.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="camera"></param>
        public void LookRotation(Transform character, Transform camera)
        {
            float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

            m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            if (clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

            if (smooth)
            {
                character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
        }

        /// <summary>
        /// Sets lockCursor value, which will enable or disable the cursor locking
        /// helper function.
        /// <para>passing in true = Cursor is locked, Cursor Locking Helper is enabled</para>
        /// <para>passing in false = Cursor is not locked, Cursor Locking Helper is disabled</para>
        /// </summary>
        /// <param name="value"></param>
        public void SetCursorLock(bool value)
        {
            lockCursor = value;
            if (!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        /// <summary>
        /// Updates the Cursor mode automatically using MouseLook.InternalLockUpdate(), the Cursor 
        /// Locking Helper function.
        /// </summary>
        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (lockCursor)
                InternalLockUpdate();
        }

        /// <summary>
        /// Cursor Locking Helper function. 
        /// <para>Pressing the Esc key sets the Cursor Lock Mode to be None
        /// and makes the cursor visible.</para>
        /// <para>Pressing the Left Mouse Button sets the Cursor Lock Mode to
        /// Locked and makes it invisible.</para>
        /// </summary>
        private void InternalLockUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                m_cursorIsLocked = false;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                m_cursorIsLocked = true;
            }

            if (m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        /// <summary>
        /// Clamps the rotation around a minimum and maximum X angle value
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
    }
}
