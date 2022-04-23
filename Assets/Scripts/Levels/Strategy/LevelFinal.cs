using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunAndCatch
{
    public class LevelFinal : ILevelStatus
    {
        private LevelManager _levelManager;

        void ILevelStatus.EnterStatus(LevelManager levelStatus)
        {
            _levelManager = levelStatus;
            Object.Destroy(levelStatus.InputHandler);
            levelStatus.InputHandler = null;
            ((ICharacterMoveToTarget)_levelManager.CharacterMotor).MoveToTarget(_levelManager.FinishPlatform.FinalLevel());      // start of movement to the final point       
            ((ICharacterMoveToTarget)_levelManager.CharacterMotor).MoveDone += ShowFinalScreen;         // subscribe for the event that the character has reached the final point           
            _levelManager.UIMediator.SubscribeNextLevelBuutonEvent(NextLevel);                           // subscribe to the click of a next level button
        }

        private void ShowFinalScreen()
        {
            _levelManager.UIMediator.ShowFinalScreen();
        }

        private void NextLevel()
        {
            ((ICharacterMoveToTarget)_levelManager.CharacterMotor).MoveDone -= ShowFinalScreen;
            SceneManager.LoadScene(_levelManager.LevelSettings.LevelName);
        }
    }
}


