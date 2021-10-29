using System;
using UnityEngine;

namespace Appalachia.KOC.Character.States
{
    [Serializable]
    public struct PlayerState : IEquatable<PlayerState>
    {
        [SerializeField] public BreathingState breathing;
        [SerializeField] public HumanMovementState movement;
        [SerializeField] public HumanPositioningState positioning;
        [SerializeField] public LookingState looking;

        #region IEquatable

        public bool Equals(PlayerState other)
        {
            return positioning.Equals(other.positioning) &&
                   looking.Equals(other.looking) &&
                   movement.Equals(other.movement);
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerState other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = positioning.GetHashCode();
                hashCode = (hashCode * 397) ^ looking.GetHashCode();
                hashCode = (hashCode * 397) ^ movement.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(PlayerState left, PlayerState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerState left, PlayerState right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}
