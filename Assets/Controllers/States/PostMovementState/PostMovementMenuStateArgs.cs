using System;

namespace Assets.Controllers.States {
    public class PostMovementMenuStateArgs  {

        public bool CanAttack;

        public Action OnMovementConfirmed;

        public Action OnAttack;

        public Action OnUndoMove;
    }
}