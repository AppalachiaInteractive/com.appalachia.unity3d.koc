using Appalachia.Rendering.PostProcessing.AutoFocus;
using UnityEngine;

namespace Appalachia.KOC.Gameplay
{
    public abstract class PlayerCamera : MonoBehaviour
    {
        public Transform eyeTransform;

        private Camera _camera;

        public new Camera camera
        {
            get => _camera;
            set => _camera = value;

            //autoFocus = value.GetComponent<DepthOfFieldAutoFocus>();
        }

        public DepthOfFieldAutoFocus autoFocus { get; private set; }

        public virtual void OnSpawn(SpawnPoint spawnPoint)
        {
        }

        public virtual void Simulate(Vector3 playerPosition, Vector3 playerAngles, float deltaTime)
        {
        }

        public virtual void Warp(Vector3 position, Vector3 angles)
        {
        }
    }
} // Gameplay
