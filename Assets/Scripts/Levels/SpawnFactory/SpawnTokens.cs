using System.Collections.Generic;
using UnityEngine;

namespace RunAndCatch
{
    internal class SpawnTokens : SpawnGameObject <Token>
    {
        private const string spawnTokenPosition_tag = "SpawnTokenPosition";
        private int _tokensMultiplier;
        private Transform[] _platforms;

        public SpawnTokens(GameObject spawnObject, Transform spawnParent, int tokensMultiplier, Transform[] platforms) : base(spawnObject, spawnParent)
        {
            _tokensMultiplier = tokensMultiplier;
            _platforms = platforms;
        }

        // ñalling spawn and grouping tokens
        internal override Token[] SpawnAndGetObjects()
        {
            List<Token> tokens = new List<Token>();

            for (int i = 1; i < _platforms.Length; i++)
            {
                tokens.AddRange(SpawnTokensOnPlatfom(_platforms[i]));
            }
           
            return tokens.ToArray();
        }

        // spawn tokens in random positions
        private Token[] SpawnTokensOnPlatfom(Transform platform)
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
            if (_tokensMultiplier > spawnPositionsList.Count)
            {
                tokensCount = spawnPositionsList.Count;
                Debug.LogWarning($"Token multiplier in LevelSettings exceeds the number of spawns position in {platform}, and is equal to it");
            }
            else
            {
                tokensCount = _tokensMultiplier;
            }

            Token[] tokens = new Token[tokensCount];
            int i = 0;
            while (i < tokensCount)
            {
                int randomValue = Random.Range(0, spawnPositionsList.Count);

                SpawnPosition = spawnPositionsList[randomValue].position;
                tokens[i] = SpawnHelper<Token>.SpawnAndGetComponent(spawnGameObject: this);

                spawnPositionsList.RemoveAt(randomValue);
                i++;
            }
            return tokens;
        }
    }
}
