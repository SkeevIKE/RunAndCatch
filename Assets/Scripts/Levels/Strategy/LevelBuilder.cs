using System.Collections.Generic;
using UnityEngine;

namespace RunAndCatch
{
    public class LevelBuilder : ILevelStatus
    {       
        private LevelSettings _levelSettings;
        private Level _level;       
       
        void ILevelStatus.EnterStatus(Level level)
        {           
            _level = level;
            _levelSettings = level.LevelSettings;

            BuildScene();
            BuildCharacter();
            BuildCamera();
            BuildUI();

            _level.ChangeStatus(new LevelProgress());
        }

        // create a scene from platforms and a finish platform
        private void BuildScene()
        {
            // create scene groups
            Transform levelGroup = new GameObject($"=== {_levelSettings.LevelName}_Group ===").transform;
            Transform tokensGroup = new GameObject("--- Tokens_Group ---").transform;
            tokensGroup.SetParent(levelGroup);

            // spawn platforms
            var spawnPlatforms = new SpawnPlatforms(_levelSettings.PlatformPrefab, levelGroup.transform, _levelSettings.LevelSize, _levelSettings.PlatformSize);
            Transform[] platforms = spawnPlatforms.SpawnAndGetObjects();

            // spawn tokens
            var spawnTokens = new SpawnTokens(_levelSettings.TokenPrefab, tokensGroup, _levelSettings._TokensMultiplier, platforms);
            Token[] tokens = spawnTokens.SpawnAndGetObjects();
            SubscribeTokensEvent(tokens);

            // spawn finish platform
            var spawnFinishPlatform = new SpawnFinishPlatform(_levelSettings.FinishPlatformPrefab, levelGroup.transform, _levelSettings.LevelSize, _levelSettings.PlatformSize);
            _level.FinishPlatform = spawnFinishPlatform.SpawnAndGetObject();

            // spawn particals boxes
            var spawnParticalsBoxes = new SpawnParticalsBoxes(_levelSettings.ParticalBoxsPrefab, levelGroup.transform, _levelSettings.LevelSize, _levelSettings.PlatformSize);
            spawnParticalsBoxes.Spawn();
        }
        

        // subscribe to tokens event
        private void SubscribeTokensEvent(Token[] tokens)
        {
            foreach (var token in tokens)
            {
                token.TokenIsTakenEvent += _level.AddScoreToPalayer;
            }
        }

        // create a playable character and input handler
        private void BuildCharacter()
        {
            // create character group
            Transform characterGroup = new GameObject($"=== Character_Group ===").transform;

            var spawnCharacter = new SpawnCharacter(_levelSettings.CharacterPrefab, characterGroup.transform);
            _level.Character = spawnCharacter.SpawnAndGetObject();
            _level.InputHandler = characterGroup.gameObject.AddComponent<InputHandler>();                     
            _level.Character.Initialization(_level.InputHandler);
        }

        // create a camera group
        private void BuildCamera()
        {
            var spawnCamera = new SpawnCamera(_levelSettings.CameraPrefab, null);
            _level.CameraMotor = spawnCamera.SpawnAndGetObject();
            _level.CameraMotor.Initialisation(_level.Character.transform);
        }

        // create a UI
        private void BuildUI()
        {
          _level.UIMediator =  Object.Instantiate(_levelSettings.UICanvasPrefab).GetComponent<UIMediator>();
        }        
    }
}
