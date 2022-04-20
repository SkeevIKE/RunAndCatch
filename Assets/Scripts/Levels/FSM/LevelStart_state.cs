using System.Collections.Generic;
using UnityEngine;

namespace RunAndCatch
{
    public class LevelStart_state : ILevelState
    {
        private const string spawnTokenPosition_tag = "SpawnTokenPosition";
        private Level_settings _levelSettings;

        void ILevelState.EnterState(Level_status levelStatus)
        {
            _levelSettings = levelStatus.LevelSettings;

            Transform levelGroup = new GameObject($"{_levelSettings.LevelName}_Group").transform;
            Transform tokensGroup = new GameObject("Tokens_Group").transform;
            Transform characterGroup = new GameObject($"Character_Group").transform;

            tokensGroup.SetParent(levelGroup);
            BuildPlatforms(levelGroup, tokensGroup);
            BuildCharacter(characterGroup);

            levelStatus.ChangeState(new LevelPlay_state());
        }

        // create a scene from platforms and a finish platform
        private void BuildPlatforms(Transform levelGroup, Transform tokensGroup)
        {           
            var spawnPosition = Vector3.zero;

            for (int i = 0; i < _levelSettings.LevelSize; i++)
            {
                Transform platform = Object.Instantiate(_levelSettings.PlatformPrefab, spawnPosition, Quaternion.identity, levelGroup.transform).transform;
                if (i > 0)
                {
                    SpawnTokens(tokensGroup, platform);
                }
                spawnPosition = new Vector3(0, 0, spawnPosition.z + _levelSettings.PlatformSize);
            }

            Object.Instantiate(_levelSettings.FinishPlatformPrefab, spawnPosition, Quaternion.identity, levelGroup);

        }

        // create tokens in random positions
        private void SpawnTokens(Transform tokensGroup, Transform platform)
        {
            var spawnPositionsList = new List<Transform>();
            foreach (var transform in platform.GetComponentsInChildren<Transform>())
            {
                if (transform.tag == spawnTokenPosition_tag)
                {
                    spawnPositionsList.Add(transform);
                }
            }

            int tokensCount;
            // checking that the token multiplier does not exceed the possible spawn position
            if (_levelSettings._TokensMultiplier > spawnPositionsList.Count)
            {
                tokensCount = spawnPositionsList.Count;
                Debug.LogWarning($"Token multiplier in {_levelSettings} exceeds the number of spawns position in {platform}, and is equal to it");
            }
            else
            {
                tokensCount = _levelSettings._TokensMultiplier;
            }

            int i = 0;
            while (i < tokensCount)
            {
                int randomValue = Random.Range(0, spawnPositionsList.Count);
                Object.Instantiate(_levelSettings.TokenPrefab, spawnPositionsList[randomValue].position, Quaternion.identity, tokensGroup);
                spawnPositionsList.RemoveAt(randomValue);
                i++;
            }
        }

        // create a playable character, input handler and camera
        private void BuildCharacter(Transform characterGroup)
        {
            Character_motor characterMotor = Object.Instantiate(_levelSettings.CharacterPrefab, Vector3.zero, Quaternion.identity, characterGroup.transform)
                                                                .GetComponent<Character_motor>();

            characterGroup.gameObject.AddComponent<Input_handler>().Initialisation(characterMotor);
            Object.Instantiate(_levelSettings.CameraPrefab).GetComponent<Camera_motor>().Initialisation(characterMotor.transform);

        }
    }
}
