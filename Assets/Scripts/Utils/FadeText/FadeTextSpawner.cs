using NTC.Global.Pool;
using UnityEngine;

namespace Utils.FadeText
{
    public class FadeTextSpawner : MonoBehaviour
    {
        public static FadeTextSpawner Instance;
        [SerializeField] private FadedNumbers _fadedPrefab;

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void SpawnFadedText(string text, Vector3 pos)
        {
            var fadedText = NightPool.Spawn(_fadedPrefab, pos);
            fadedText.SetText(text);
        }
    }
}