using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Utils
{
    public class RandomUtils
    {
        public static T GetRandomFromArrayWithWeight<T>(Dictionary<T, float> weights)
        {
            var totalWeight = weights.Sum(x => x.Value);
            if (totalWeight <= 1)
                throw new ArgumentOutOfRangeException();
            while (true)
            {
                var totalSum = 0f;
                var targetRange = Random.Range(1, totalWeight);
                foreach (var weight in weights)
                {
                    totalSum += weight.Value;
                    if (totalSum >= targetRange)
                        return weight.Key;
                }
            }
        }
    }
}