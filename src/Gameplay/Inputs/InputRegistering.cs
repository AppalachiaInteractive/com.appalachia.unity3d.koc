using System.Collections.Generic;
using Appalachia.CI.Integration.Assets;
using UnityEditor;

namespace Appalachia.KOC.Gameplay.Inputs
{
#if UNITY_EDITOR

    public class InputRegistering
    {
        private static bool InputAlreadyRegistered(
            string name,
            InputManagerEntry.Kind kind,
            SerializedProperty spAxes)
        {
            for (var i = 0; i < spAxes.arraySize; ++i)
            {
                var spAxis = spAxes.GetArrayElementAtIndex(i);
                var axisName = spAxis.FindPropertyRelative("m_Name").stringValue;
                var kindValue = spAxis.FindPropertyRelative("type").intValue;
                if ((axisName == name) && ((int) kind == kindValue))
                {
                    return true;
                }
            }

            return false;
        }

        private static void WriteEntry(SerializedProperty spAxes, InputManagerEntry entry)
        {
            if (InputAlreadyRegistered(entry.name, entry.kind, spAxes))
            {
                return;
            }

            spAxes.InsertArrayElementAtIndex(spAxes.arraySize);
            var spAxis = spAxes.GetArrayElementAtIndex(spAxes.arraySize - 1);
            spAxis.FindPropertyRelative("m_Name").stringValue = entry.name;
            spAxis.FindPropertyRelative("descriptiveName").stringValue = entry.desc;
            spAxis.FindPropertyRelative("negativeButton").stringValue = entry.btnNegative;
            spAxis.FindPropertyRelative("altNegativeButton").stringValue = entry.altBtnNegative;
            spAxis.FindPropertyRelative("positiveButton").stringValue = entry.btnPositive;
            spAxis.FindPropertyRelative("altPositiveButton").stringValue = entry.altBtnPositive;
            spAxis.FindPropertyRelative("gravity").floatValue = entry.gravity;
            spAxis.FindPropertyRelative("dead").floatValue = entry.deadZone;
            spAxis.FindPropertyRelative("sensitivity").floatValue = entry.sensitivity;
            spAxis.FindPropertyRelative("snap").boolValue = entry.snap;
            spAxis.FindPropertyRelative("invert").boolValue = entry.invert;
            spAxis.FindPropertyRelative("type").intValue = (int) entry.kind;
            spAxis.FindPropertyRelative("axis").intValue = (int) entry.axis;
            spAxis.FindPropertyRelative("joyNum").intValue = (int) entry.joystick;
        }

        public static void RegisterInputs(List<InputManagerEntry> entries)
        {
            // Grab reference to input manager
            var assets = AssetDatabaseManager.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset");

            // Temporary fix. This happens some time with HDRP init when it's called before asset database is initialized (probably related to package load order).
            if (assets.Length == 0)
            {
                return;
            }

            var inputManager = assets[0];

            // Wrap in serialized object
            var soInputManager = new SerializedObject(inputManager);
            var spAxes = soInputManager.FindProperty("m_Axes");

            foreach (var entry in entries)
            {
                WriteEntry(spAxes, entry);
            }

            // Commit
            soInputManager.ApplyModifiedProperties();
        }
    }
#endif
}
