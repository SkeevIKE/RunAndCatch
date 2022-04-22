using System.Collections.Generic;
using UnityEngine;

namespace RunAndCatch
{
    public class LevelBuilder : ILevelStatus
    {
        private const string spawnTokenPosition_tag = "SpawnTokenPosition";
        private Level_settings _levelSettings;
        private LevelManager _levelManager;

        void ILevelStatus.EnterStatus(LevelManager levelManager)
        {
            _levelManager = levelManager;
            _levelSettings = levelManager.LevelSettings;

            Transform levelGroup = new GameObject($"{_levelSettings.LevelName}_Group").transform;
            Transform tokensGroup = new GameObject("Tokens_Group").transform;
            Transform characterGroup = new GameObject($"Character_Group").transform;
            tokensGroup.SetParent(levelGroup);

            BuildPlatforms(levelGroup, tokensGroup);
            BuildCharacter(characterGroup);
            BuildUI();

            levelManager.ChangeStatus(new LevelProgress());
        }

        // create a scene from platforms and a finish platform
        private void BuildPlatforms(Transform levelGroup, Transform tokensGroup)
        {           
            var spawnPosition = Vector3.zero;
            bool isParticalSpawn = false;

            for (int i = 0; i < _levelSettings.LevelSize; i++)
            {
                Transform platform = Object.Instantiate(_levelSettings.PlatformPrefab, spawnPosition, Quaternion.identity, levelGroup.transform).transform;
                if (i > 0)
                {
                    SpawnTokens(tokensGroup, platform);
                }
                spawnPosition = new Vector3(0, 0, spawnPosition.z + _levelSettings.PlatformSize);

                if (!isParticalSpawn && i >= (_levelSettings.LevelSize / 2))
                {
                    SpawnParticalsBoxes(levelGroup, platform);
                    isParticalSpawn = true;
                }
            }

            Object.Instantiate(_levelSettings.FinishPlatformPrefab, spawnPosition, Quaternion.identity, levelGroup);

        }

        // create particals boxes in middle level
        private void SpawnParticalsBoxes(Transform levelGroup, Transform platform)
        {
           ParticleSystem[] particleSystems = Object.Instantiate(_levelSettings.ParticalBoxsPrefab, platform.position, Quaternion.identity, levelGroup)
                                                                .GetComponentsInChildren<ParticleSystem>();
            foreach (var partical in particleSystems)
            {
                UnityEngine.ParticleSystem.ShapeModule shapeModule = partical.shape;
                shapeModule.scale = new Vector3(shapeModule.scale.x, shapeModule.scale.y, _levelSettings.LevelSize / 2);
            }
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
                Token token = Object.Instantiate(_levelSettings.TokenPrefab, spawnPositionsList[randomValue].position, Quaternion.identity, tokensGroup)
                                                 .GetComponent<Token>();
                token.TokenIsTakenEvent += _levelManager.AddScoreToPalayer;

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

        // create a UI
        private void BuildUI()
        {
          _levelManager.UIMediator =  Object.Instantiate(_levelSettings.UICanvasPrefab).GetComponent<UIMediator>();
        }
    }
}
