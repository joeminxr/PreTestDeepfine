namespace Preassignment.Interactables
{
    using UnityEngine;

    /// <summary>
    /// Reflective surface implementation and orchestrates interactive events
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class Mirror : MonoBehaviour, ILaserInteractable, ITransformInteractable
    {
        /// <summary>
        /// </summary>
        /// <param name="incomingDirection"></param>
        /// <param name="surfaceNormal"></param>
        /// <returns></returns>
        public Vector3 Reflect(Vector3 incomingDirection, Vector3 surfaceNormal)
        {
            return Vector3.Reflect(incomingDirection, surfaceNormal);
        }

        #region ILaserInteractable Properties

        public void OnLaserEnter()
        {
            Debug.Log("This mirror is reflecting a laser");
        }

        public void OnLaserExit()
        {
            Debug.Log("This mirror stopped reflecting a laser");
        }

        #endregion

        #region ITransformInteractable Properties

        public bool IsBeingTransformed { get; private set; }

        public Transform Transform => transform;

        public void BeginTransform()
        {
            IsBeingTransformed = true;
        }

        public void UpdateTransform(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }

        public void EndTransform()
        {
            IsBeingTransformed = false;
        }

        #endregion
    }
}