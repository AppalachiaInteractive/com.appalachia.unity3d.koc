using System;
using UnityEngine;

namespace Appalachia.Core.Character.States
{
    [Serializable]
    public struct FootPlacementState : IEquatable<FootPlacementState>
    {
        [SerializeField] public Vector3 position;            
        
        [SerializeField] public Vector3 lastPlantedPosition;
        
        [SerializeField] public Vector3 normal;
            
        [SerializeField] public PhysicMaterial physicalMaterial;
            
        [SerializeField] public float speedScalar;
            
        [SerializeField] public bool grounded;
            
        [SerializeField] public float waterDepth;
            
        [SerializeField] public bool audioStale;
        
#region IEquatable

        public bool Equals(FootPlacementState other)
        {
            return position.Equals(other.position) && lastPlantedPosition.Equals(other.lastPlantedPosition) && normal.Equals(other.normal) && Equals(physicalMaterial, other.physicalMaterial) && speedScalar.Equals(other.speedScalar) && grounded == other.grounded && waterDepth.Equals(other.waterDepth) && audioStale == other.audioStale;
        }

        public override bool Equals(object obj)
        {
            return obj is FootPlacementState other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = position.GetHashCode();
                hashCode = (hashCode * 397) ^ lastPlantedPosition.GetHashCode();
                hashCode = (hashCode * 397) ^ normal.GetHashCode();
                hashCode = (hashCode * 397) ^ (physicalMaterial != null ? physicalMaterial.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ speedScalar.GetHashCode();
                hashCode = (hashCode * 397) ^ grounded.GetHashCode();
                hashCode = (hashCode * 397) ^ waterDepth.GetHashCode();
                hashCode = (hashCode * 397) ^ audioStale.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(FootPlacementState left, FootPlacementState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FootPlacementState left, FootPlacementState right)
        {
            return !left.Equals(right);
        }

#endregion
    }
}