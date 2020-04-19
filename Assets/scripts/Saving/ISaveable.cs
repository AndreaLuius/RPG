namespace RPG.Saving
{
    public interface ISaveable
    {
        object captureState();
        void restoreState(object state);
    }
}