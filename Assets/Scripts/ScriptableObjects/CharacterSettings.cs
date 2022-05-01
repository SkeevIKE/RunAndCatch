using UnityEngine;

namespace RunAndCatch
{
    [CreateAssetMenu(fileName = "New Character Settings", menuName = "RunAndCatch settings/Character Settings", order = 51)]
    public class CharacterSettings : ScriptableObject
    {
        [Space]
        [Header("Movement settings")]

        [SerializeField]
        private float _moveSpeed;
        internal float MoveSpeed => _moveSpeed;

        [SerializeField]
        private float _strafeSpeed;
        internal float StrafeSpeed => _strafeSpeed;

        [SerializeField]
        private float _rotationSpeed;
        internal float RotationSpeed => _rotationSpeed;

        [Space]
        [Header("Time settings")]

        [SerializeField]
        private float _moveTimeToTarget;
        internal float MoveTimeToTarget => _moveTimeToTarget;

        [SerializeField]
        private float _rotationTimeToTarget;
        internal float RotationTimeToTarget => _rotationTimeToTarget;

        [Space]
        [Header("Audio links")]

        [SerializeField]
        private AudioClip _footStepAudioClip;
        internal AudioClip FootStepAudioClip => _footStepAudioClip;
    }
}
