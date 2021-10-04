using System;
using UnityEngine;

namespace Appalachia.KOC.Character.States
{
    [Serializable]
    public struct HumanMovementState : IEquatable<HumanMovementState>
    {
        [SerializeField] public Vector2 movingSpeed;
        [SerializeField] public float speedScalar;
        [SerializeField] public float jumpSpeedScalar;
        [SerializeField] public float jumpingScalar;
        [SerializeField] public bool swimming;
        [SerializeField] public bool jumping;
        [SerializeField] public bool jumpStart;
        
#region IEquatable

        public bool Equals(HumanMovementState other)
        {
            return movingSpeed.Equals(other.movingSpeed) && speedScalar.Equals(other.speedScalar) && jumpSpeedScalar.Equals(other.jumpSpeedScalar) && jumpingScalar.Equals(other.jumpingScalar) && swimming == other.swimming && jumping == other.jumping && jumpStart == other.jumpStart;
        }

        public override bool Equals(object obj)
        {
            return obj is HumanMovementState other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = movingSpeed.GetHashCode();
                hashCode = (hashCode * 397) ^ speedScalar.GetHashCode();
                hashCode = (hashCode * 397) ^ jumpSpeedScalar.GetHashCode();
                hashCode = (hashCode * 397) ^ jumpingScalar.GetHashCode();
                hashCode = (hashCode * 397) ^ swimming.GetHashCode();
                hashCode = (hashCode * 397) ^ jumping.GetHashCode();
                hashCode = (hashCode * 397) ^ jumpStart.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(HumanMovementState left, HumanMovementState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HumanMovementState left, HumanMovementState right)
        {
            return !left.Equals(right);
        }

#endregion
    }
}