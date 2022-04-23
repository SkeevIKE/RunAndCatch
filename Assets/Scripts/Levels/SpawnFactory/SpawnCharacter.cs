using UnityEngine;

namespace RunAndCatch
{
    internal class SpawnCharacter : SpawnGameObject <CharacterMotor>
    {
        public SpawnCharacter(GameObject spawnObject, Transform spawnParent) : base(spawnObject, spawnParent) { }

        // spawn a playable character
        internal override CharacterMotor SpawnAndGetObject()
        {
            CharacterMotor characterMotor = SpawnHelper<CharacterMotor>.SpawnAndGetComponent(spawnGameObject: this);
            return characterMotor;
        }
    }
}
