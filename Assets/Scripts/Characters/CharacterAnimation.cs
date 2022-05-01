using UnityEngine;

namespace RunAndCatch
{
    internal class CharacterAnimation : IPlayAnimation
    {
        private Animator _animator;
        private int _animatorRunID = Animator.StringToHash("isRun");

        internal CharacterAnimation(Animator animator)
        {           
            _animator = animator;          
        }

        void IPlayAnimation.AnimationMove(bool isMove)
        {
            _animator.SetBool(_animatorRunID, isMove);
        }
    }
}
