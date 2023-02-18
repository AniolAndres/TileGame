using System;

namespace Assets.Controllers.States {
    public class PreMovementMenuStateArgs  {

        public bool CanAttack;

        public Action OnMovementConfirmed;

        public Action OnAttack;
    }
}