namespace Preassignment.Interactables
{
    using UnityEngine;

    public sealed class Receiver : MonoBehaviour, ILaserInteractable
    {
        private Renderer _renderer;
        private Material _material;

        private static readonly Color ActiveColor = Color.red;
        private static readonly Color InactiveColor = Color.blue;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();

            // using instance material so we don't affect shared materials
            _material = _renderer.material;

            // Ensure correct initial state
            _material.color = InactiveColor;
        }

        public void OnLaserEnter()
        {
            _material.color = ActiveColor;
            Debug.Log("laser has hit this receiver");
        }

        public void OnLaserExit()
        {
            _material.color = InactiveColor;
            Debug.Log("laser has left this receiver");
        }
    }
}