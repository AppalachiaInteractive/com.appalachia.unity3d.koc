using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Appalachia.Core.Character.Settings
{
    [Serializable]
    public struct Breathing
    {
        [Range(0, 5)] public float initialDelay;
        [Range(0, 1)] public float initialDelayVariance;

        public float GetInitialDelay()
        {
            return initialDelay + Random.Range(-initialDelayVariance, initialDelayVariance);
        }

        [Range(0, 5)] public float inhalePeriod;
        [Range(0, 1)] public float inhalePeriodVariance;
        [Range(0, 1)] public float inhalePeriodPacingFactor;

        public float GetInhalePeriod()
        {
            return inhalePeriod + Random.Range(-inhalePeriodVariance, inhalePeriodVariance);
        }

        [Range(0, 5)] public float exhalePeriod;
        [Range(0, 1)] public float exhalePeriodVariance;

        public float GetExhalePeriod()
        {
            return exhalePeriod + Random.Range(-exhalePeriodVariance, exhalePeriodVariance);
        }

        [Range(0, 10)] public float breathingPeriod;
        [Range(0, 1)] public float breathingPeriodVariance;
        [Range(0, 10)] public float breathingPeriodIntensityFactor;

        public float GetBreathingPeriod()
        {
            return breathingPeriod + Random.Range(-breathingPeriodVariance, breathingPeriodVariance);
        }

        [Range(0, 1)] public float intensityDampening;
        [Range(0, 1)] public float intensityTransference;

        [Range(0, 1)] public float volumeOverPace;
        [Range(0, 1)] public float volumeOverPaceVariance;

        public float GetVolumeOverPace()
        {
            return volumeOverPace + Random.Range(-volumeOverPaceVariance, volumeOverPaceVariance);
        }
        
#region IEquatable
        
        
        
#endregion
    }
}