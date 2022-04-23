using System.Collections.Generic;
using UnityEngine;

namespace RunAndCatch
{
    public class LevelBuilder : ILevelStatus
    {       
        private LevelSettings _levelSettings;
        private LevelManager _levelManager;       
       
        void ILevelStatus.EnterStatus(LevelManager levelManager)
        {
           // Cursor.visible = false;
           // Cursor.lockState = CursorLockMode.Confined;
            _levelManager = levelManager;
            _levelSettings = levelManager.LevelSettings;

            BuildScene();
            BuildCharacter();
            BuildCamera();
            BuildUI();

            _levelManager.ChangeStatus(new LevelProgress());
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
            _levelManager.FinishPlatform = spawnFinishPlatform.SpawnAndGetObject();

            // spawn particals boxes
            var spawnParticalsBoxes = new SpawnParticalsBoxes(_levelSettings.ParticalBoxsPrefab, levelGroup.transform, _levelSettings.LevelSize, _levelSettings.PlatformSize);
            spawnParticalsBoxes.Spawn();
        }
        

        // subscribe to tokens event
        private void SubscribeTokensEvent(Token[] tokens)
        {
            foreach (var token in tokens)
            {
                token.TokenIsTakenEvent += _levelManager.AddScoreToPalayer;
            }
        }

        // create a playable character and input handler
        private void BuildCharacter()
        {
            // create character group
            Transform characterGroup = new GameObject($"=== Character_Group ===").transform;

            var spawnCharacter = new SpawnCharacter(_levelSettings.CharacterPrefab, characterGroup.transform);
            _levelManager.CharacterMotor = spawnCharacter.SpawnAndGetObject();
            _levelManager.InputHandler = characterGroup.gameObject.AddComponent<InputHandler>();
            _levelManager.InputHandler.Initialisation(_levelManager.CharacterMotor);
        }

        // create a camera group
        private void BuildCamera()
        {
            var spawnCamera = new SpawnCamera(_levelSettings.CameraPrefab, null);
            _levelManager.CameraMotor = spawnCamera.SpawnAndGetObject();
            _levelManager.CameraMotor.Initialisation(_levelManager.CharacterMotor.transform);
        }

        // create a UI
        private void BuildUI()
        {
          _levelManager.UIMediator =  Object.Instantiate(_levelSettings.UICanvasPrefab).GetComponent<UIMediator>();
        }        
    }
}
