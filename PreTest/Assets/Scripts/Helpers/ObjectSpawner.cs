namespace Preassignment.Helpers
{
    using Preassignment.Interactables;
    using Preassignment.LaserSystem;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Simple spawning system with object pooling
    /// </summary>
    public sealed class ObjectSpawner : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private GameObject spawnablePrefab;
        [SerializeField] private LayerMask placementSurfaceMask;
        [SerializeField] private LayerMask spawnPrefabLayerMask;
        [SerializeField] private LaserEmitter laserEmitter;

        [Header("Limits")]
        [SerializeField] private int maxSpawns = 12;

        private Camera mainCamera;
        private readonly Queue<GameObject> _spawnPool = new Queue<GameObject>();

        private void Start()
        {
            mainCamera = Camera.main;
            if (laserEmitter == null || laserEmitter.Equals(null))
            {
                laserEmitter = FindFirstObjectByType<LaserEmitter>();
            }
            InitializePool();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TrySpawnAtMouse();
            }
            else if (Input.GetMouseButtonDown(1)) // Right click
            {
                TryDespawnAtMouse();
            }
        }

        private void InitializePool()
        {
            for (int i = 0; i < maxSpawns; i++)
            {
                GameObject mirror = Instantiate(spawnablePrefab);
                mirror.SetActive(false);
                _spawnPool.Enqueue(mirror);
            }
        }

        private void TrySpawnAtMouse()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementSurfaceMask))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Vector3 spawnPosition = hit.point;

                    // Make sure that the mirrors spawned on the slanted board are perpendicular to the board
                    Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                    SpawnOrReuse(spawnPosition, spawnRotation);
                    Debug.Log("Spawning prefab");
                    return;
                }
            }

            Debug.Log("Mouse is not hovering over the game board. Please try a spot on the game board.");
        }

        private void TryDespawnAtMouse()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, spawnPrefabLayerMask))
            {
                if (hit.collider.TryGetComponent<ITransformInteractable>(out var transformInteractable))
                {
                    if (transformInteractable.IsBeingTransformed)
                    {
                        Debug.LogWarning("You are attempting to despawn an object you are interacting with. Unable to despawn object while it is being manipulated");
                        return;
                    }
                }

                GameObject mirror = hit.collider.gameObject;

                if (!mirror.activeSelf) return;

                mirror.SetActive(false);

                laserEmitter.MarkDirty();

                Debug.Log("Despawning prefab");
            }
        }

        private void SpawnOrReuse(Vector3 position, Quaternion rotation)
        {
            GameObject mirror = _spawnPool.Dequeue();

            mirror.transform.SetPositionAndRotation(position, rotation);
            mirror.SetActive(true);

            _spawnPool.Enqueue(mirror);

            laserEmitter.MarkDirty();
        }
    }
}