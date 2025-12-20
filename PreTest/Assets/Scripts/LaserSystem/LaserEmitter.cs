using System.Collections.Generic;
using UnityEngine;
using Preassignment.Interactables;

namespace Preassignment.LaserSystem
{
    /// <summary>
    /// Coordinates laser subsystems during runtime.
    /// Decides when tracing math occurs and updates the laser renderer
    /// </summary>
    public class LaserEmitter : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_lineRenderer;

        // Dependencies
        private LaserConfig _config;
        private LaserTracer _tracer;
        private LaserRenderer _renderer;

        // State
        private bool _isDirty;
        private TraceResult _currentTrace;

        // Interaction tracking
        private readonly HashSet<IInteractable> _activeInteractables = new HashSet<IInteractable>();

        private void Awake()
        {
            Initialize();
            MarkDirty();
        }

        private void LateUpdate()
        {
            if (!_isDirty) return;

            UpdateLaser();
            _isDirty = false;
        }

        private void Initialize()
        {
            _config = CreateDefaultConfig();

            LayerMask collisionMask = ResolveCollisionMask(_config.CollisionLayerNames);

            _tracer = new LaserTracer(_config, collisionMask);

            _renderer = new LaserRenderer(m_lineRenderer);
        }

        public void MarkDirty()
        {
            _isDirty = true;
        }

        private void UpdateLaser()
        {
            _currentTrace = _tracer.Trace(transform.position, transform.forward);

            _renderer.Render(_currentTrace);

            ProcessInteractions(_currentTrace);
        }

        private void ProcessInteractions(TraceResult traceResult)
        {
            // Collect interactables hit this frame
            var currentInteractables = new HashSet<IInteractable>();

            foreach (var hit in traceResult.Hits)
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    currentInteractables.Add(interactable);
                }
            }

            // If laser stops hitting mirror (previous to current)
            foreach (var interactable in _activeInteractables)
            {
                if (!currentInteractables.Contains(interactable))
                {
                    interactable.OnInteractEnd();
                }
            }

            // If laser starts/still hits mirror (current to previous)
            foreach (var interactable in currentInteractables)
            {
                if (!_activeInteractables.Contains(interactable))
                {
                    interactable.OnInteractStart();
                }
            }

            _activeInteractables.Clear();
            _activeInteractables.UnionWith(currentInteractables);
        }

        #region Config Helpers

        /// <summary>
        /// For this assignment, config values are hard-coded at creation time.
        /// </summary>
        /// <returns></returns>
        public static LaserConfig CreateDefaultConfig()
        {
            return new LaserConfig(
                maxReflections: 10,
                maxLaserDistance: 100f,
                surfaceOffset: 0.001f,
                collisionLayerNames: new[] { "Mirror", "Receiver", "Obstacle" }
            );
        }

        private static LayerMask ResolveCollisionMask(string[] layerNames)
        {
            return LayerMask.GetMask(layerNames);
        }

        #endregion
    }
}