namespace Preassignment.Helpers
{
    using Preassignment.Interactables;
    using Preassignment.LaserSystem;
    using UnityEngine;

    public sealed class TransformManipulator : MonoBehaviour
    {
        [SerializeField] private LayerMask mirrorMask;
        [SerializeField] private LayerMask boardMask;
        [SerializeField] private LaserEmitter laserEmitter;

        private Camera mainCamera;
        private ITransformInteractable _active;
        private Transform _activeTransform;

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

            _activeTransform = hit.collider.transform;
            _active.BeginTransform();
        }

        private void UpdateDrag()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, boardMask))
                return;

            Vector3 position = hit.point;

            // keep mirror perpendicular to the board
            Quaternion rotation =
                Quaternion.FromToRotation(Vector3.up, hit.normal);

            _active.UpdateTransform(position, rotation);

            laserEmitter.MarkDirty();
        }

        private void EndDrag()
        {
            if (_active == null)
                return;

            _active.EndTransform();
            _active = null;
            _activeTransform = null;

            laserEmitter.MarkDirty();
        }
    }
}