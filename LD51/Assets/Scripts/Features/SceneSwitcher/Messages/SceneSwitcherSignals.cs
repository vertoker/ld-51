namespace Features.SceneSwitcher.Messages
{
    public class SceneSwitcherSignals
    {
        public sealed class SwitchToLevel
        {
            public int LevelIndex { get; }

            public SwitchToLevel(int levelIndex)
            {
                LevelIndex = levelIndex;
            }
        }
    }
}