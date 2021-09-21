using UnityEngine;

namespace Rabbit
{
    public class GameCombo : GameStatus
    {
        [ReadOnly] public int Combo;
        [ReadOnly] public int HighestCombo;
        [ReadOnly] public int Failed;

        private bool isBreakCombo;
        
        public override void Initialize()
        {
            Combo = 0;
            HighestCombo = 0;
            Failed = 0;

            isBreakCombo = false;
        }

        public void ComboSuccess()
        {
            if (isBreakCombo) {
                isBreakCombo = false;
            }
            if (++Combo > HighestCombo) {
                HighestCombo = Combo;
            }
        }

        public void BreakCombo()
        {
            if (!isBreakCombo) {
                isBreakCombo = true;
                Failed++;
            }
            Combo = 0;
        }
    }
}