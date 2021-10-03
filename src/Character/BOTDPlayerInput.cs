using System;
using UnityEngine;

namespace Appalachia.Core.Character
{
    namespace BOTDPlayerInputMappings
    {
    } // BOTDPlayerInputMappings

    public struct BOTDPlayerInput
    {
        private static readonly IBOTDPlayerInputMapping[] _mappings;

        public static BOTDPlayerInputMapping mapping { get; private set; }
        public static bool forceMapping { get; private set; }

        public static bool ignore { get; set; }

        static BOTDPlayerInput()
        {
            _mappings = new IBOTDPlayerInputMapping[Enum.GetNames(typeof(BOTDPlayerInputMapping)).Length];

            foreach (var type in BOTDPlayerInputMappingAttribute.GetTypes())
            {
                var mapping = Activator.CreateInstance(type) as IBOTDPlayerInputMapping;

                for (var index = 0; index < type.GetCustomAttributes(typeof(BOTDPlayerInputMappingAttribute), false).Length; index++)
                {
                    var attribute = type.GetCustomAttributes(typeof(BOTDPlayerInputMappingAttribute), false)[index];
                    _mappings[((BOTDPlayerInputMappingAttribute) attribute).index] = mapping;
                }
            }
        }

        public static void SelectInputMapping(BOTDPlayerInputMapping? @override = null, bool force = false)
        {
            var selected = mapping;

            if (@override.HasValue)
            {
                selected = @override.Value;
            }
            else if (Application.platform == RuntimePlatform.PS4)
            {
                selected = BOTDPlayerInputMapping.PlayStation;
            }
            else if (Application.platform == RuntimePlatform.XboxOne)
            {
                selected = BOTDPlayerInputMapping.Xbox;
            }
            else
            {
                var ignoreCase = StringComparison.OrdinalIgnoreCase;
                selected = BOTDPlayerInputMapping.MouseAndKeyboard;

                var @is = Input.GetJoystickNames();
                for (var index = 0; index < @is.Length; index++)
                {
                    var i = @is[index];
                    if (i.StartsWith("openvr", ignoreCase) && (i.IndexOf("vive", ignoreCase) >= 0))
                    {
                        selected = BOTDPlayerInputMapping.Vive;
                    }
                    else if ((i.IndexOf("xbox", ignoreCase) >= 0) || (i.IndexOf("360", ignoreCase) >= 0) || (i.IndexOf("gpx", ignoreCase) >= 0))
                    {
                        if ((Application.platform == RuntimePlatform.WindowsPlayer) || (Application.platform == RuntimePlatform.WindowsEditor))
                        {
                            selected = BOTDPlayerInputMapping.XboxForWindows;
                        }
                        else if ((Application.platform == RuntimePlatform.OSXPlayer) || (Application.platform == RuntimePlatform.OSXEditor))
                        {
                            selected = BOTDPlayerInputMapping.XboxForMac;
                        }
                    }
                    else if ((i.IndexOf("sony", ignoreCase) >= 0) || (i.IndexOf("wireless", ignoreCase) >= 0))
                    {
                        if ((Application.platform == RuntimePlatform.WindowsPlayer) || (Application.platform == RuntimePlatform.WindowsEditor))
                        {
                            selected = BOTDPlayerInputMapping.PlayStationForWindows;
                        }
                        else if ((Application.platform == RuntimePlatform.OSXPlayer) || (Application.platform == RuntimePlatform.OSXEditor))
                        {
                            selected = BOTDPlayerInputMapping.PlayStationForMac;
                        }
                    }
                }
            }

            if (mapping != selected)
            {
                mapping = selected;

                Debug.Log("SetInputMapping: " + selected);
                Input.ResetInputAxes();
            }

            forceMapping = force;
        }

        public static void Update(out BOTDPlayerInput input)
        {
            if (!ignore)
            {
                if (mapping == BOTDPlayerInputMapping.MouseAndKeyboard)
                {
                    input = _mappings[(int) mapping].GetKeyboardInput();
                }
                else
                {
                    input = _mappings[(int) mapping].GetControllerInput();
                }
            }
            else
            {
                input = default;
            }
        }

        public Vector2 look;
        public Vector2 move;
        public float zoom;
        public float run;
        public bool jump;
    }
} // Gameplay
