using System.Linq;
using UnityEngine;

namespace Preassignment.LaserSystem
{
    public sealed class LaserRenderer
    {
        private LineRenderer _lineRenderer;

        public LaserRenderer(LineRenderer lineRenderer)
        {
            _lineRenderer = lineRenderer;
            _lineRenderer.useWorldSpace = true;
        }

        public void Render(TraceResult traceResult)
        {
            // Laser is always on in this assignment so TraceResult is expected to always contain a valid path
            var points = traceResult.Points;

            _lineRenderer.positionCount = points.Count;
            _lineRenderer.SetPositions(points.ToArray());
        }
    }
}
