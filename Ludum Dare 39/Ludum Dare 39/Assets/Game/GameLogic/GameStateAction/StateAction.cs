namespace GameOfLife.GameLogic.GameStateAction
{
    public interface StateAction
    {
        string GetName();
        int GetDurationInSeconds();
        StatsDifference GetEffectBefore();
        StatsDifference GetEffectDuring();
        StatsDifference GetEffectAfter();
        bool IsPartnersAction();
        bool IsForBoth();
        bool IsWorkAction();
    }
}
