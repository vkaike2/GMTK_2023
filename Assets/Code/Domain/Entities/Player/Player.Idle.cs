using System.Collections;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public class Idle : PlayerBaseState
    {
        public override State State => Player.State.Idle;

        public override void Start(Player player)
        {
            base.Start(player);

        }

        public override void OnEnterState()
        {
            _player.MoveInput.Started = () => OnMoveInputStarted();
            _player.EntityAnimator.Play(ANIMATION_ENTITY_IDLE);
        }

        public override void OnExitState()
        {
            _player.ClearInputActions();
        }

        public override void Update()
        {
        }

        private void OnMoveInputStarted()
        {
            _player.ChangeState(State.Move);
        }
    }
}
