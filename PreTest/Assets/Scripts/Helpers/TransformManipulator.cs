namespace Preassignment.Helpers
{
    using Preassignment.Interactables;
    using Preassignment.LaserSystem;
    using UnityEngine;

    public sealed class TransformManipulator : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private LayerMask mirrorMask;
        [SerializeField] private LayerMask boardMask;
        [SerializeField] private LaserEmitter laserEmitter;

        [Header("Rotation")]
        [SerializeField] private float rotationSpeed = 90f; // degrees per second

        private Camera mainCamera;
        private ITransformInteractable _active;
        private Quaternion _currentRotation;

        void Start()
        {
            mainCamera = Camera.main;
            if (laserEmitter == null || laserEmitter.Equals(null))
            {
                laserEmitter = FindFirstObjectByType<LaserEmitter>();
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryBeginDrag();

            if (Input.GetMouseButton(0) && _active != null)
                UpdateDrag();

            if (Input.GetMouseButtonUp(0))
                EndDrag();
        }

        private void TryBeginDrag()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mirrorMask))
                return;

            if (!hit.collider.TryGetComponent<ITransformInteractable>(out _active))
                return;

            if (!Physics.Raycast(ray, out RaycastHit boardHit, Mathf.Infinity, boardMask))
                return;

            Quaternion alignToSurface =
                Quaternion.FromToRotation(Vector3.up, boardHit.normal);

            _currentRotation =
                _active.Transform.rotation * Quaternion.Inverse(alignToSurface);

            _active.BeginTransform();
        }

        private void UpdateDrag()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, boardMask))
                return;

            Vector3 position = hit.point;

            // Align mirror perpendicular to surface
            Quaternion alignToSurface =
                Quaternion.FromToRotation(Vector3.up, hit.normal);

            // Build surface axes
            Vector3 surfaceNormal = hit.normal;
            Vector3 surfaceRight = Vector3.Cross(surfaceNormal, Vector3.up).normalized;

            if (surfaceRight.sqrMagnitude < 0.001f)
                surfaceRight = Vector3.right; // fallback for near-vertical normals

            // Handle rotation input
            float delta = rotationSpeed * Time.deltaTime;

            Quaternion rotationDelta = Quaternion.identity;

            if (Input.GetKey(KeyCode.A))
                rotationDelta *= Quaternion.AngleAxis(-delta, surfaceNormal);

            if (Input.GetKey(KeyCode.D))
                rotationDelta *= Quaternion.AngleAxis(delta, surfaceNormal);

            if (Input.GetKey(KeyCode.W))
                rotationDelta *= Quaternion.AngleAxis(-delta, surfaceRight);

            if (Input.GetKey(KeyCode.S))
                rotationDelta *= Quaternion.AngleAxis(delta, surfaceRight);

            _currentRotation = rotationDelta * _currentRotation;

            Quaternion finalRotation = _currentRotation * alignToSurface;

            _active.UpdateTransform(position, finalRotation);
            laserEmitter.MarkDirty();
        }

        private void EndDrag()
        {
            if (_active == null)
                return;

            _active.EndTransform();
            _active = null;
            _currentRotation = Quaternion.identity;

            laserEmitter.MarkDirty();
        }
    }
}