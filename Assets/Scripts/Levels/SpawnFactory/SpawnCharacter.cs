using UnityEngine;

namespace RunAndCatch
{
    internal class SpawnCharacter : SpawnGameObject <Character>
    {
        public SpawnCharacter(GameObject spawnObject, Transform spawnParent) : base(spawnObject, spawnParent) { }

        // spawn a playable character
        internal override Character SpawnAndGetObject()
        {
            Character characterMotor = SpawnHelper<Character>.SpawnAndGetComponent(spawnGameObject: this); 
            return characterMotor;
        }
    }
}
