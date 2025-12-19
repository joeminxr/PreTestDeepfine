using System.Collections.Generic;
using UnityEngine;

namespace LaserSystem
{
    /// <summary>
    /// Coordinates laser subsystems during runtime.
    /// Decides when tracing math occurs and updates the laser renderer
    /// </summary>
    public class LaserEmitter : MonoBehaviour
    {
        // Dependencies
        private LaserConfig _config;
        private LaserTracer _tracer;
        private LaserRenderer _renderer;

        // State
        private bool _isDirty;
        private TraceResult _currentTrace;

        // Interaction tracking
        private HashSet<IInteractable> _currentLaserInteractables;

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

            _renderer = GetComponent<LaserRenderer>();
        }

        public void MarkDirty()
        {
            _isDirty = true;
        }

        private void UpdateLaser()
        {
            // 1. Trace laser path
            // 2. Render laser
            // 3. Process interactions (enter / exit)
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
                collisionLayerNames: new[] { "Mirror", "Receiver" }
            );
        }

        private static LayerMask ResolveCollisionMask(string[] layerNames)
        {
            return LayerMask.GetMask(layerNames);
        }

        #endregion
    }
}