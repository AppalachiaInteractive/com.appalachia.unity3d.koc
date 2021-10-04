using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Appalachia.KOC.Character.Settings
{
    [Serializable]
    public struct PlayerFootPlanting : IEquatable<PlayerFootPlanting>
    {
        public LayerMask floorLayers;

        [PropertyRange(0, 10f), Tooltip("[0, 10]")]
        public float walkStepDistance;

        [PropertyRange(0, 10f), Tooltip("[0, 10]")]
        public float runStepDistance;

        [PropertyRange(0, 10f), Tooltip("[0, 10]")]
        public float stopSpeedThreshold;
        
#region IEquatable

        public bool Equals(PlayerFootPlanting other)
        {
            return floorLayers.Equals(other.floorLayers) && walkStepDistance.Equals(other.walkStepDistance) && runStepDistance.Equals(other.runStepDistance) && stopSpeedThreshold.Equals(other.stopSpeedThreshold);
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerFootPlanting other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = floorLayers.GetHashCode();
                hashCode = (hashCode * 397) ^ walkStepDistance.GetHashCode();
                hashCode = (hashCode * 397) ^ runStepDistance.GetHashCode();
                hashCode = (hashCode * 397) ^ stopSpeedThreshold.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(PlayerFootPlanting left, PlayerFootPlanting right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerFootPlanting left, PlayerFootPlanting right)
        {
            return !left.Equals(right);
        }

#endregion
    }
}