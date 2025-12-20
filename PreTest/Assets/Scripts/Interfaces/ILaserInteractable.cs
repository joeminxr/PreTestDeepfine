namespace Preassignment.Interactables
{
    /// <summary>
    /// Interface for objects that respond to laser enter/exit events
    /// </summary>
    public interface ILaserInteractable
    {
        void OnLaserEnter();
        void OnLaserExit();
    }
}
