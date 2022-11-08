using System;

namespace Assets.Controllers {
    public class FundsController {

        private int currentFunds;

        public int CurrentFunds => currentFunds;

        public bool HasEnoughFunds(int cost) {
            return currentFunds >= cost;
        }

        public void SpendFunds(int cost) {
            currentFunds -= cost;

            if(currentFunds < 0) {
                throw new NotSupportedException("Trying to spend funds although there aren't enough!, check if it's enough first");
            }
        }

        public void GainFunds(int income) {
            currentFunds += income;
        }
    }
}