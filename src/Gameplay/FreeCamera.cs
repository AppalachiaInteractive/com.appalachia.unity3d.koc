using System.Collections.Generic;
using Appalachia.KOC.Gameplay.Inputs;
using UnityEngine;

namespace Appalachia.KOC.Gameplay
{
    [ExecuteAlways]
    public class FreeCamera : MonoBehaviour
    {
        private static readonly string kMouseX = "Mouse X";
        private static readonly string kMouseY = "Mouse Y";
        private static readonly string kRightStickX = "Controller Right Stick X";
        private static readonly string kRightStickY = "Controller Right Stick Y";
        private static readonly string kVertical = "Vertical";
        private static readonly string kHorizontal = "Horizontal";

        private static readonly string kYAxis = "YAxis";
        private static readonly string kSpeedAxis = "Speed Axis";
        public float m_LookSpeedController = 120f;
        public float m_LookSpeedMouse = 10.0f;
        public float m_MoveSpeed = 10.0f;
        public float m_MoveSpeedIncrement = 2.5f;
        public float m_Turbo = 10.0f;

        private void Update()
        {
            // If the debug menu is running, we don't want to conflict with its inputs.
            /*if (DebugManager.instance.displayRuntimeUI)
                return;*/

            var inputRotateAxisX = 0.0f;
            var inputRotateAxisY = 0.0f;
            if (Input.GetMouseButton(1))
            {
                inputRotateAxisX = Input.GetAxis(kMouseX) * m_LookSpeedMouse;
                inputRotateAxisY = Input.GetAxis(kMouseY) * m_LookSpeedMouse;
            }

            inputRotateAxisX +=
                Input.GetAxis(kRightStickX) * m_LookSpeedController * Time.deltaTime;
            inputRotateAxisY +=
                Input.GetAxis(kRightStickY) * m_LookSpeedController * Time.deltaTime;

            var inputChangeSpeed = Input.GetAxis(kSpeedAxis);
            if (inputChangeSpeed != 0.0f)
            {
                m_MoveSpeed += inputChangeSpeed * m_MoveSpeedIncrement;
                if (m_MoveSpeed < m_MoveSpeedIncrement)
                {
                    m_MoveSpeed = m_MoveSpeedIncrement;
                }
            }

            var inputVertical = Input.GetAxis(kVertical);
            var inputHorizontal = Input.GetAxis(kHorizontal);
            var inputYAxis = Input.GetAxis(kYAxis);

            var moved = (inputRotateAxisX != 0.0f) ||
                        (inputRotateAxisY != 0.0f) ||
                        (inputVertical != 0.0f) ||
                        (inputHorizontal != 0.0f) ||
                        (inputYAxis != 0.0f);
            if (moved)
            {
                var rotationX = transform.localEulerAngles.x;
                var newRotationY = transform.localEulerAngles.y + inputRotateAxisX;

                // Weird clamping code due to weird Euler angle mapping...
                var newRotationX = rotationX - inputRotateAxisY;
                if ((rotationX <= 90.0f) && (newRotationX >= 0.0f))
                {
                    newRotationX = Mathf.Clamp(newRotationX, 0.0f, 90.0f);
                }

                if (rotationX >= 270.0f)
                {
                    newRotationX = Mathf.Clamp(newRotationX, 270.0f, 360.0f);
                }

                transform.localRotation = Quaternion.Euler(
                    newRotationX,
                    newRotationY,
                    transform.localEulerAngles.z
                );

                var moveSpeed = Time.deltaTime * m_MoveSpeed;
                if (Input.GetMouseButton(1))
                {
                    moveSpeed *= Input.GetKey(KeyCode.LeftShift) ? m_Turbo : 1.0f;
                }
                else
                {
                    moveSpeed *= Input.GetAxis("Fire1") > 0.0f ? m_Turbo : 1.0f;
                }

                transform.position += transform.forward * moveSpeed * inputVertical;
                transform.position += transform.right * moveSpeed * inputHorizontal;
                transform.position += Vector3.up * moveSpeed * inputYAxis;
            }
        }

        private void OnEnable()
        {
            RegisterInputs();
        }

        private void RegisterInputs()
        {
#if UNITY_EDITOR
            var inputEntries = new List<InputManagerEntry>();

            // Add new bindings
            inputEntries.Add(
                new InputManagerEntry
                {
                    name = kRightStickX,
                    kind = InputManagerEntry.Kind.Axis,
                    axis = InputManagerEntry.Axis.Fourth,
                    sensitivity = 1.0f,
                    gravity = 1.0f,
                    deadZone = 0.2f
                }
            );
            inputEntries.Add(
                new InputManagerEntry
                {
                    name = kRightStickY,
                    kind = InputManagerEntry.Kind.Axis,
                    axis = InputManagerEntry.Axis.Fifth,
                    sensitivity = 1.0f,
                    gravity = 1.0f,
                    deadZone = 0.2f,
                    invert = true
                }
            );

            inputEntries.Add(
                new InputManagerEntry
                {
                    name = kYAxis,
                    kind = InputManagerEntry.Kind.KeyOrButton,
                    btnPositive = "page up",
                    altBtnPositive = "joystick button 5",
                    btnNegative = "page down",
                    altBtnNegative = "joystick button 4",
                    gravity = 1000.0f,
                    deadZone = 0.001f,
                    sensitivity = 1000.0f
                }
            );

            inputEntries.Add(
                new InputManagerEntry
                {
                    name = kSpeedAxis,
                    kind = InputManagerEntry.Kind.KeyOrButton,
                    btnPositive = "home",
                    btnNegative = "end",
                    gravity = 1000.0f,
                    deadZone = 0.001f,
                    sensitivity = 1000.0f
                }
            );
            inputEntries.Add(
                new InputManagerEntry
                {
                    name = kSpeedAxis,
                    kind = InputManagerEntry.Kind.Axis,
                    axis = InputManagerEntry.Axis.Seventh,
                    gravity = 1000.0f,
                    deadZone = 0.001f,
                    sensitivity = 1000.0f
                }
            );

            InputRegistering.RegisterInputs(inputEntries);
#endif
        }
    }
}
