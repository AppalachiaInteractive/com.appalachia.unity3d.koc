using System;
using System.Collections;
using System.Linq;
using Appalachia.Core.Behaviours;
using Appalachia.KOC.Character;
using Appalachia.Utility.Logging;
using UnityEngine;
using UnityEngine.Profiling;
using Object = UnityEngine.Object;

namespace Appalachia.KOC.Gameplay
{
    public class GameController : AppalachiaBehaviour
    {
        public delegate void AudioTransformsUpdater(Transform root, Transform eye);

        public AudioTransformsUpdater audioTransformsUpdater;
        public PlayerCamera playerCameraPrefab;

        public PlayerController playerPrefab;

        public SpawnPoint startPoint;
        public Camera defaultCamera { get; set; }

        public Object debugContext { get; set; }
        public PlayerCamera playerCamera { get; set; }

        public PlayerController playerController { get; set; }
        public SpawnPoint lastPlayerSpawnPoint { get; private set; }

        public void DespawnPlayer()
        {
            if (playerController)
            {
                Destroy(playerController.gameObject);
            }

            if (playerCamera)
            {
                Destroy(playerCamera.gameObject);
            }

            playerController = null;
            playerCamera = null;
        }

        public SpawnPoint[] GetAllSpawnPoints()
        {
            return (from a in GameAgent.GetAgents() where a is SpawnPoint select a as SpawnPoint).ToArray();
        }

        public SpawnPoint GetNextPlayerSpawnPoint()
        {
            var spawnPoints = GetAllSpawnPoints();
            Array.Sort(spawnPoints, (x, y) => string.Compare(x.agentIdentifier, y.agentIdentifier));
            var index = Array.IndexOf(spawnPoints, lastPlayerSpawnPoint);
            return spawnPoints[(index + 1) % spawnPoints.Length];
        }

        public SpawnPoint GetSpawnPoint(string name)
        {
            return SpawnPoint.Find(name);
        }

        public void SpawnPlayer(bool reset = true, bool nextFrame = true)
        {
            SpawnPlayer(startPoint, reset, nextFrame);
        }

        public void SpawnPlayer(SpawnPoint spawnPoint, bool reset = true, bool nextFrame = true)
        {
            StartCoroutine(SpawnPlayerCo(spawnPoint, reset, nextFrame));
        }

        protected void Start()
        {
            defaultCamera = Camera.main;

            var cameraTransform = defaultCamera.transform;
            cameraTransform.localPosition = Vector3.zero;
            cameraTransform.localRotation = Quaternion.identity;
            cameraTransform.localScale = Vector3.one;

            BOTDPlayerInput.SelectInputMapping();
        }

        private IEnumerator SpawnPlayerCo(SpawnPoint spawnPoint, bool reset, bool nextFrame)
        {
            if (nextFrame)
            {
                yield return null;
            }

            Profiler.BeginSample("SpawnPlayerCo");

            lastPlayerSpawnPoint = spawnPoint;

            if (!playerPrefab)
            {
                AppaLog.Error("Missing player prefab");
                yield break;
            }

            if (!playerController)
            {
                playerController = Instantiate(playerPrefab);
            }

            if (!playerCamera)
            {
                playerCamera = Instantiate(playerCameraPrefab);
            }

            using (var enumerator = GameAgent.GetEnumerator())
            {
                for (var i = enumerator; i.MoveNext();)
                {
                    if (i.Current)
                    {
                        i.Current.OnBeforeSpawnPlayer(reset);
                    }
                }
            }


            var cam = spawnPoint.camera ? spawnPoint.camera : defaultCamera;
            var cameraTransform = cam.transform;
            cameraTransform.parent = playerCamera.eyeTransform
                ? playerCamera.eyeTransform
                : playerCamera.transform;

            cameraTransform.localPosition = Vector3.zero;
            cameraTransform.localRotation = Quaternion.identity;
            cameraTransform.localScale = Vector3.one;

            playerCamera.camera = cam;
            playerController.playerCamera = playerCamera;

            if (audioTransformsUpdater != null)
            {
                audioTransformsUpdater(playerController.transform, cam.transform);
            }

            spawnPoint.Spawn(playerController, reset);
            
            using (var enumerator = GameAgent.GetEnumerator())
            {
                for (var i = enumerator; i.MoveNext();)
                {
                    if (i.Current)
                    {
                        i.Current.OnAfterSpawnPlayer(spawnPoint, reset);
                    }
                }
            }

            Profiler.EndSample();
        }

        public static GameController FindGameController()
        {
            return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }
} // Gameplay
