using Appalachia.KOC.Character;
using UnityEngine;

namespace Appalachia.KOC.Gameplay
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]

//[RequireComponent(typeof(PlayerFoley))]
    public class PlayerController : GameAgent
    {
        public CharacterController characterController { get; private set; }
        public PlayerCharacter playerCharacter { get; private set; }

        public PlayerCamera playerCamera { get; set; }

        protected new void Awake()
        {
            base.Awake();

            characterController = GetComponent<CharacterController>();
            playerCharacter = GetComponent<PlayerCharacter>();

            //playerFoley = GetComponent<PlayerFoley>();
        }

        protected void Update()
        {
            var transform = this.transform;

            BOTDPlayerInput.Update(out var input);

            if (input.move.y < 0f)
            {
                input.look.y = -input.look.y;
            }

            if (playerCharacter)
            {
                playerCharacter.Simulate(characterController, input);
            }
        }

        protected void LateUpdate()
        {
            var firstPersonCamera = playerCamera as FirstPersonCamera;

            if (firstPersonCamera && playerCharacter)
            {
                float pitch, yaw;
                playerCharacter.GetLookPitchAndYaw(out pitch, out yaw);
                firstPersonCamera.pitch = pitch;
                firstPersonCamera.yaw = yaw;
            }

            var transform = this.transform;
            var position = transform.localPosition;
            var rotation = transform.localEulerAngles;
            var deltaTime = Time.deltaTime;

            playerCamera.Simulate(position, rotation, deltaTime);
        }

        public override void OnSpawn(SpawnPoint spawnPoint, bool reset)
        {
            base.OnSpawn(spawnPoint, reset);

            var position = transform.localPosition;
            var rotation = transform.localEulerAngles;
            var angles = spawnPoint.transform.localEulerAngles;

            if (playerCharacter)
            {
                playerCharacter.OnSpawn(angles, reset, characterController);
            }

            playerCamera.OnSpawn(spawnPoint);

            position = transform.localPosition;
            playerCamera.Warp(position, angles);

            for (var i = 0; i < 4; ++i)
            {
                playerCamera.Simulate(position, rotation, 0.1f);
            }
        }
    }
} // Gameplay
