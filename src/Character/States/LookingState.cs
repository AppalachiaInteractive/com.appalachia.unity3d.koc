using System;
using UnityEngine;

namespace Appalachia.KOC.Character.States
{
    [Serializable]
    public struct LookingState : IEquatable<LookingState>
    {
        [SerializeField] public Vector2 lookingAngles;

        #region IEquatable

        public bool Equals(LookingState other)
        {
            return lookingAngles.Equals(other.lookingAngles);
        }

        public override bool Equals(object obj)
        {
            return obj is LookingState other && Equals(other);
        }

        public override int GetHashCode()
        {
            return lookingAngles.GetHashCode();
        }

        public static bool operator ==(LookingState left, LookingState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LookingState left, LookingState right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}
