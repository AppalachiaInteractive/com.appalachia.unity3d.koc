using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Appalachia.KOC.Character.Settings
{
    [Serializable]
    public struct PlayerLocomotion : IEquatable<PlayerLocomotion>
    {
        [PropertyRange(0f, 10f)]
        [Tooltip("[0, 10]")]
        public float runSpeed;

        [PropertyRange(0f, 10f)]
        [Tooltip("[0, 10]")]
        public float walkSpeed;

        #region IEquatable

        public bool Equals(PlayerLocomotion other)
        {
            return walkSpeed.Equals(other.walkSpeed) && runSpeed.Equals(other.runSpeed);
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerLocomotion other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (walkSpeed.GetHashCode() * 397) ^ runSpeed.GetHashCode();
            }
        }

        public static bool operator ==(PlayerLocomotion left, PlayerLocomotion right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerLocomotion left, PlayerLocomotion right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}
