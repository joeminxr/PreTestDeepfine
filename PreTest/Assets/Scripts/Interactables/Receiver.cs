namespace Preassignment.Interactables
{
    using UnityEngine;

    public sealed class Receiver : MonoBehaviour, ILaserInteractable
    {
        public void OnLaserEnter()
        {
            Debug.Log("laser has hit this receiver");
        }

        public void OnLaserExit()
        {
        }
    }
}