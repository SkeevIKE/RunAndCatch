using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunAndCatch
{
    public class LevelFinal : ILevelStatus
    {
        private Level _level;
        private IMoveToTarget _moveToTarget;

        void ILevelStatus.EnterStatus(Level level)
        {
            _level = level;
            Object.Destroy(level.InputHandler);
            level.InputHandler = null;

            _moveToTarget = _level.Character.gameObject.AddComponent<CharacterMoveToTarget>();
            // start of movement to the final point     
            _moveToTarget.MoveToTarget(_level.FinishPlatform.FinalLevel(), _level.Character);            
            // subscribe for the event that the character has reached the final point   
            _moveToTarget.MoveIsDone += ShowFinalScreen;   

            _level.UIMediator.SubscribeNextLevelBuutonEvent(NextLevel);                                        
        }

        private void ShowFinalScreen()
        {
            _level.UIMediator.ShowFinalScreen();
        }

        private void NextLevel()
        {
            _level.UIMediator.UnsubscribeNextLevelBuutonEvent(NextLevel);                                        
            _moveToTarget.MoveIsDone -= ShowFinalScreen;
            SceneManager.LoadScene(_level.LevelSettings.LevelName);
        }
    }
}


