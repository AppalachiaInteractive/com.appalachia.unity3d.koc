using System;
using UnityEngine;

namespace Appalachia.KOC.Character.States
{
    [Serializable]
    public struct PlayerParts : IEquatable<PlayerParts>
    {
        public Transform leftFoot;
        public Transform rightFoot;
        public Transform leftHand;
        public Transform rightHand;
        public Transform mouth;
        
#region IEquatable

        public bool Equals(PlayerParts other)
        {
            return Equals(leftFoot, other.leftFoot) && Equals(rightFoot, other.rightFoot) && Equals(leftHand, other.leftHand) && Equals(rightHand, other.rightHand) && Equals(mouth, other.mouth);
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerParts other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (leftFoot != null ? leftFoot.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (rightFoot != null ? rightFoot.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (leftHand != null ? leftHand.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (rightHand != null ? rightHand.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (mouth != null ? mouth.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(PlayerParts left, PlayerParts right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerParts left, PlayerParts right)
        {
            return !left.Equals(right);
        }

#endregion
    }
}