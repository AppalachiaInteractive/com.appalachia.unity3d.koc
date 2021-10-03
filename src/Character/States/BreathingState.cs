using System;
using UnityEngine;

namespace Appalachia.Core.Character.States
{
    [Serializable]
    public struct BreathingState
    {
        [SerializeField] public float time;
        [SerializeField] public float period;

        [SerializeField] public float nextPace;
        [SerializeField] public float nextIntensity;

        [SerializeField] public float currentPace;
        [SerializeField] public float currentIntensity;

        [SerializeField] public RespirationSpeed speed;
        [SerializeField] public RespirationStyle style;
        [SerializeField] public BreathDirection state;

        public bool inhaling => state == BreathDirection.Inhale;
        public bool exhaling => state == BreathDirection.Exhale;

    }
}