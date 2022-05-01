using UnityEngine;

namespace RunAndCatch
{
    [CreateAssetMenu(fileName = "New Level Settings", menuName = "RunAndCatch settings/Level settings", order = 51)]
    public class LevelSettings : ScriptableObject
    {
        [Space]
        [Header("Level settings")]

        [SerializeField]
        private string _levelName = "Level_Default";
        internal string LevelName => _levelName;

        [SerializeField] [Range(5, 25)]
        private int _levelSize = 6;
        internal int LevelSize => _levelSize;

        [SerializeField] [Range(1, 9)]
        private int _tokensMultiplier = 4;
        internal int _TokensMultiplier => _tokensMultiplier;

        [Space]
        [Header("Prefabs links")]

        [SerializeField]
        private GameObject _characterPrefab;
        internal GameObject CharacterPrefab => _characterPrefab;

        [SerializeField]
        private GameObject _cameraPrefab;
        internal GameObject CameraPrefab => _cameraPrefab;

        [SerializeField]
        private GameObject _platformPrefab;
        internal GameObject PlatformPrefab => _platformPrefab;

        [SerializeField]
        private int _platformSize = 10;
        internal int PlatformSize => _platformSize;

        [SerializeField]
        private GameObject _finishPlatformPrefab;
        internal GameObject FinishPlatformPrefab => _finishPlatformPrefab;

        [SerializeField]
        private GameObject _tokenPrefab;
        internal GameObject TokenPrefab => _tokenPrefab;

        [SerializeField]
        private GameObject _particalBoxsPrefab;
        internal GameObject ParticalBoxsPrefab => _particalBoxsPrefab;

        [SerializeField]
        private GameObject _uiCanvasPrefab;
        internal GameObject UICanvasPrefab => _uiCanvasPrefab;

        [Space]
        [Header("Audio links")]

        [SerializeField]
        private AudioClip _musicAudioClip;
        internal AudioClip MusicAudioClip => _musicAudioClip;

        [SerializeField]
        private AudioClip _tokenAudioClip;
        internal AudioClip TokenAudioClip => _tokenAudioClip;

        [Space]
        [Header("Tokens audio clip volume")]

        [SerializeField]
        [Range(0.01f, 1)]
        private float _tokensAudioClipVolume = 1;
        internal float TokensAudioClipVolume => _tokensAudioClipVolume;
    }
}
