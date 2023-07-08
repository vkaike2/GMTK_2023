using System.Collections;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public class Die : PlayerBaseState
    {
        public override State State => State.Die;

        public override void OnEnterState()
        {
            _player.EntityAnimator.Play(ANIMATION_ENTITY_DIE);

            _player.StartCoroutine(WaitThenFinishStage());
        }

        public override void OnExitState()
        {
        }

        public override void Update()
        {
        }

        private IEnumerator WaitThenFinishStage()
        {
            yield return new WaitForSeconds(1f);
            _player.StageManager.OnFinishState.Invoke();
        }
    }
}
