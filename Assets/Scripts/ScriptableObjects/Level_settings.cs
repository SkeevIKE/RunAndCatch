using UnityEngine;

namespace RunAndCatch
{
    [CreateAssetMenu(fileName = "New Level Settings", menuName = "RunAndCatch settings/Level settings", order = 51)]
    public class Level_settings : ScriptableObject
    {       
        [SerializeField]
        private string _levelName = "Level_Default";
        internal string LevelName => _levelName;

        [SerializeField] [Range(5, 25)]
        private int _levelSize = 10;
        internal int LevelSize => _levelSize;

        [SerializeField] [Range(1, 9)]
        private int _tokensMultiplier = 1;
        internal int _TokensMultiplier => _tokensMultiplier;

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
        private int _platformSize = 5;
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
    }
}
