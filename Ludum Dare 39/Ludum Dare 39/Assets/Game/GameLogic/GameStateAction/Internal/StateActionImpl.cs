namespace GameOfLife.GameLogic.GameStateAction.Internal
{
    public class StateActionImpl : StateAction
    {
        public string Name = null;
        public int DurationInSeconds = 10;
        public StatsDifference EffectBefore = new StatsDifference();
        public StatsDifference EffectDuring = new StatsDifference();
        public StatsDifference EffectAfter = new StatsDifference();
        public bool ForBoth = false;
        public bool PartnersAction = false;
        public bool WorkAction = false;

        public int GetDurationInSeconds()
        {
            return DurationInSeconds;
        }

        public StatsDifference GetEffectAfter()
        {
            return EffectAfter;
        }

        public StatsDifference GetEffectBefore()
        {
            return EffectBefore;
        }

        public StatsDifference GetEffectDuring()
        {
            return EffectDuring;
        }

        public string GetName()
        {
            return Name;
        }

        public bool IsForBoth()
        {
            return ForBoth;
        }

        public bool IsPartnersAction()
        {
            return PartnersAction;
        }

        public bool IsWorkAction()
        {
            return WorkAction;
        }
    }
}
