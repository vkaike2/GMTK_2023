using System;
using System.Collections;
using UnityEngine;


public partial class Player : MonoBehaviour
{
    public class Move : PlayerBaseState
    {
        public override State State => Player.State.Move;

        public override void Start(Player player)
        {
            base.Start(player);
        }

        public override void OnEnterState()
        {
            _player.MoveInput.Canceled = () => OnMoveInputCanceled();
            _player.EntityAnimator.Play(ANIMATION_ENTITY_MOVE);
        }

        public override void OnExitState()
        {
        }

        public override void Update()
        {
            //_rigidbody2D.velocity = new Vector2(_player.MoveInput.Value.x + _status.MovementSpeed, _player.MoveInput.Value.y + _status.MovementSpeed);
            _rigidbody2D.velocity = _player.MoveInput.Value * _status.MovementSpeed;
        }

        private void OnMoveInputCanceled()
        {
            _rigidbody2D.velocity = Vector3.zero;
            _player.ChangeState(State.Idle);
        }

    }
}
