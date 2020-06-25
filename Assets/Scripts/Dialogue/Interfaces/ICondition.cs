namespace Shakespeare.Dialogue
{
    public interface ICondition
    {
        // Can this condition be met?
        bool CanUse(Conversation conversation);
    }
}