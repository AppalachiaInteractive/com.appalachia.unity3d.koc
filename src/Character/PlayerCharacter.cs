using Appalachia.Core.Extensions;
using Appalachia.KOC.Character.Settings;
using Appalachia.KOC.Character.States;
using UnityEngine;

namespace Appalachia.KOC.Character
{
    [DisallowMultipleComponent]
    public class PlayerCharacter : MonoBehaviour
    {
        public PlayerSettings settings;
        public PlayerState state;        
        public PlayerParts parts;
        
        public void OnSpawn(Vector3 angles, bool reset, CharacterController characterController)
        {
            var position = transform.position;
            
            if (settings == default) settings.Initialize();

            state.looking.lookingAngles.x = angles.x;
            state.looking.lookingAngles.y = angles.y;
            
            state.movement.movingSpeed = Vector2.zero;
            state.movement.jumpingScalar = settings.jumping.gravityFactor;
            state.movement.jumpSpeedScalar = 0f;

            state.positioning.leftFoot.position = new Vector3(position.x, 0f, position.z);
            state.positioning.leftFoot.grounded = true;
            
            state.positioning.rightFoot.position = new Vector3(position.x, 0f, position.z);
            state.positioning.rightFoot.grounded = true;
            
            state.positioning.feetPlanted = HumanFeet.Both;
            state.positioning.lastVegetationPosition = state.positioning.rightFoot.position;
            
            UpdateLooking(transform, 0.1f);
            UpdateMoving(transform, 0.1f, characterController);
        }

        public void GetLookPitchAndYaw(out float pitch, out float yaw)
        {
            pitch = state.looking.lookingAngles.x;
            yaw = state.looking.lookingAngles.y;
        }

        public void Simulate(CharacterController characterController, BOTDPlayerInput input)
        {
            var transform = this.transform;
            var deltaTime = Time.deltaTime;

            state.movement.jumping = !characterController.isGrounded;
            
            state.movement.jumpStart = false;
            if (!state.movement.jumping && input.jump)
            {
                state.movement.jumpingScalar = -settings.jumping.force;
                state.movement.jumpStart = true;
            }
            else if ((state.movement.jumpingScalar += deltaTime * settings.jumping.dampSpeed) >= settings.jumping.gravityFactor)
            {
                state.movement.jumpingScalar = settings.jumping.gravityFactor;
            }

            var frictionFactor = state.movement.jumping ? 0.99f : 0.1f;
            var absMove = new Vector2(Mathf.Abs(input.move.x),   Mathf.Abs(input.move.y));
            var signMove = new Vector2(Mathf.Sign(input.move.x), Mathf.Sign(input.move.y));

            var wantSpeed = Vector2.one;
            wantSpeed *= Mathf.Lerp(settings.locomotion.walkSpeed, settings.locomotion.runSpeed, input.run);
            wantSpeed = Vector2.Scale(wantSpeed, signMove);
            wantSpeed = Vector2.Lerp(state.movement.movingSpeed, wantSpeed, state.movement.jumping ? 0f : deltaTime * 10f);
            wantSpeed = Vector2.Scale(
                wantSpeed,
                Vector2.Lerp(
                    absMove,
                    new Vector2(Mathf.Approximately(input.move.x, 0f) ? 0f : 1f, Mathf.Approximately(input.move.y, 0f) ? 0f : 1f),
                    input.run
                )
            );
            state.movement.movingSpeed = Vector2.Lerp(wantSpeed, wantSpeed * frictionFactor, !state.movement.jumping ? 0f : deltaTime);

            var angularSpeed = 360f * deltaTime * Mathf.Lerp(settings.looking.lookSpeed, settings.looking.runLookSpeed, input.run);
            var lookX = input.look.x;
            var lookY = input.look.y * Mathf.Sign(input.move.y);
            state.looking.lookingAngles.x = state.looking.lookingAngles.x + (lookX * angularSpeed);
            state.looking.lookingAngles.y = state.looking.lookingAngles.y + (lookY * angularSpeed);

            state.looking.lookingAngles.x = Angles.Unwind(Mathf.Clamp(Angles.ToRelative(state.looking.lookingAngles.x), settings.looking.pitchLimitMin, settings.looking.pitchLimitMax));

            UpdateLooking(transform, deltaTime);
            UpdateMoving(transform, deltaTime, characterController);

            var walkSpeedSqr = settings.locomotion.walkSpeed * settings.locomotion.walkSpeed;
            var runSpeedSqr = settings.locomotion.runSpeed * settings.locomotion.runSpeed;
            var speedSqr = state.movement.movingSpeed.sqrMagnitude;
            
            state.movement.speedScalar = (speedSqr - walkSpeedSqr) / (runSpeedSqr - walkSpeedSqr);

            if (state.movement.speedScalar < Mathf.Epsilon)
            {
                state.movement.speedScalar = 0f;
            }

            var hadAnyFootPlanted = state.positioning.hasAnyFootPlanted;
            state.movement.jumping = !characterController.isGrounded; // check again after moving

            if (state.movement.jumpStart)
            {
                state.movement.jumpSpeedScalar = state.movement.speedScalar;
            }

            UpdateFootPlanting(transform, speedSqr);

            if (state.movement.jumpStart)
            {
                OnJump?.Invoke(this);
            }
            else if (!hadAnyFootPlanted && state.positioning.hasAnyFootPlanted)
            {
                OnLand?.Invoke(this);
            }
        }

        private void UpdateLooking(Transform transform, float deltaTime)
        {
            transform.localRotation = Quaternion.Euler(0f, state.looking.lookingAngles.y, 0f);
        }

        private void UpdateMoving(Transform transform, float deltaTime, CharacterController characterController)
        {
            var moveVector = state.movement.jumpingScalar * Physics.gravity;
            moveVector.x += state.movement.movingSpeed.x;
            moveVector.z += state.movement.movingSpeed.y;

            characterController.Move(transform.TransformVector(moveVector * deltaTime));
        }
        
        private void UpdateFootPlanting(Transform transform, float speedSqr)
        {
            if (state.movement.jumping || !state.positioning.hasAnyFootPlanted)
            {
                state.positioning.feetPlanted = HumanFeet.Neither;
                state.positioning.leftFoot.grounded = false;
                state.positioning.rightFoot.grounded = false;
                return;
            }

            var position = transform.position;
                
            state.positioning.leftFoot.position = parts.leftFoot.position;
            state.positioning.rightFoot.position = parts.rightFoot.position;                

            var walkStepSqr = settings.footPlanting.walkStepDistance * settings.footPlanting.walkStepDistance;
            var runStepSqr = settings.footPlanting.runStepDistance * settings.footPlanting.runStepDistance;
            var needStepSqr = Mathf.Lerp(walkStepSqr, runStepSqr, state.movement.speedScalar);
                
            var leftStepSqr = (state.positioning.leftFoot.position - state.positioning.leftFoot.lastPlantedPosition).sqrMagnitude;
            var rightStepSqr = (state.positioning.rightFoot.position - state.positioning.rightFoot.lastPlantedPosition).sqrMagnitude;

            var shouldStepLeft = leftStepSqr >= needStepSqr;
            var shouldStepRight = rightStepSqr >= needStepSqr;
            var shouldStop = speedSqr <= (settings.footPlanting.stopSpeedThreshold * settings.footPlanting.stopSpeedThreshold);
                
            if (state.positioning.feetPlanted == HumanFeet.Neither)
            {
                state.positioning.feetPlanted = HumanFeet.Both;
                state.positioning.leftFoot.grounded = true;
                state.positioning.rightFoot.grounded = true;
                
                FindFootPlacement(ref state.positioning.leftFoot);
                FindFootPlacement(ref state.positioning.rightFoot);
                
                state.positioning.lastVegetationPosition = state.positioning.leftFoot.position;

                OnStep?.Invoke(this);
            }
            else if (shouldStepLeft)
            {
                state.positioning.feetPlanted = HumanFeet.Left;
                state.positioning.leftFoot.grounded = true;
                state.positioning.rightFoot.grounded = false;
                
                FindFootPlacement(ref state.positioning.leftFoot);

                state.positioning.lastVegetationPosition = state.positioning.leftFoot.position;

                OnStep?.Invoke(this);
            }
            else if (shouldStepRight)
            {                
                state.positioning.feetPlanted = HumanFeet.Right;
                state.positioning.leftFoot.grounded = false;
                state.positioning.rightFoot.grounded = true;
                
                FindFootPlacement(ref state.positioning.rightFoot);

                state.positioning.lastVegetationPosition = state.positioning.rightFoot.position;

                OnStep?.Invoke(this);
            }
            else if (shouldStop)
            {
                if (state.positioning.hasBothFeetPlanted)
                {
                    return;
                }
                
                if (state.positioning.feetPlanted == HumanFeet.Left)
                {
                    FindFootPlacement(ref state.positioning.rightFoot);
                    state.positioning.lastVegetationPosition = position;
                }
                else
                {
                    FindFootPlacement(ref state.positioning.leftFoot);
                    state.positioning.lastVegetationPosition = position;
                }

                state.positioning.feetPlanted = HumanFeet.Both;

                OnStep?.Invoke(this);
            }
            /*else if (playerFoley.intersectingVegetation)
            {
                var vegDistSqr = (footPlantPosition - state.lastVegetationPosition).sqrMagnitude;

                if (vegDistSqr >= (needStepSqr * 0.3f))
                {
                    state.lastVegetationPosition = footPlantPosition;

                    FindFootPlacement(ref position, out var normal, out var physicalMaterial);
                    playerFoley.PlayVegetation(transform, position, speedScalar);
                }
            }*/
        }

        private bool FindFootPlacement(ref FootPlacementState footState)
        {            
            if (Physics.Raycast(footState.position + Vector3.up, Vector3.down, out var hit, 2f, settings.footPlanting.floorLayers))
            {
                footState.position = hit.point;
                footState.lastPlantedPosition = hit.point;
                footState.normal = hit.normal;
                footState.physicalMaterial = hit.collider.sharedMaterial;
                footState.audioStale = true;
                return true;
            }

            return false;
        }
        
        public event PlayerCharacterEvent OnStep;     
        public event PlayerCharacterEvent OnJump;        
        public event PlayerCharacterEvent OnLand;
        
#pragma warning disable 67
        public event PlayerCharacterEvent OnVocalize_Start;
        public event PlayerCharacterEvent OnVocalize_End;
        
        public event PlayerCharacterEvent OnInWater_Start;
        public event PlayerCharacterEvent OnInWater_End;
        
        public event PlayerCharacterEvent OnSwimming_Start;
        public event PlayerCharacterEvent OnSwimming_End;
        
        public event PlayerCharacterEvent OnUnderWater_Start;
        public event PlayerCharacterEvent OnUnderWater_End;
        
        public event PlayerCharacterEvent OnSleeping_Start;
        public event PlayerCharacterEvent OnSleeping_End;

        public event PlayerCharacterEvent OnDie;
#pragma warning restore 67
    }
}