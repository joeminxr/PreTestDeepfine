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
        private readonly HashSet<ILaserInteractable> _activeLaserInteractables = new HashSet<ILaserInteractable>();

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

            var collisionMask = ResolveCollisionMask(_config.CollisionLayerNames);

            _tracer = new LaserTracer(_config, collisionMask);

            _renderer = new LaserRenderer(m_lineRenderer);
        }

        public void MarkDirty()
        {
            _isDirty = true;
        }

        private void UpdateLaser()
        {
            _currentTrace = _tracer.Trace(m_lineRenderer.transform.position, m_lineRenderer.transform.forward);

            _renderer.Render(_currentTrace);

            ProcessInteractions(_currentTrace);
        }

        // logic setup for now- need to revisit after mirror transform manipulation logic
        private void ProcessInteractions(TraceResult traceResult)
        {
            // Collect interactables hit this frame
            var currentLaserInteractables = new HashSet<ILaserInteractable>();

            foreach (var hit in traceResult.Hits)
            {
                if (hit.collider.TryGetComponent<ILaserInteractable>(out var interactable))
                {
                    currentLaserInteractables.Add(interactable);
                }
            }

            // If laser stops hitting mirror (previous to current)
            foreach (var interactable in _activeLaserInteractables)
            {
                if (!currentLaserInteractables.Contains(interactable))
                {
                    interactable.OnLaserExit();
                }
            }

            // If laser starts/still hits mirror (current to previous)
            foreach (var interactable in currentLaserInteractables)
            {
                if (!_activeLaserInteractables.Contains(interactable))
                {
                    interactable.OnLaserEnter();
                }
            }

            _activeLaserInteractables.Clear();
            _activeLaserInteractables.UnionWith(currentLaserInteractables);
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