using DG.Tweening;
using NTC.Global.Pool;
using TMPro;
using UnityEngine;

namespace Utils.FadeText
{
    public class FadedNumbers : MonoBehaviour, IPoolItem
    {
        [SerializeField] private TMP_Text _text;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void OnSpawn()
        {
            transform.rotation = _camera.transform.rotation;
            _text.DOFade(0, 1).From(1).OnComplete(() => { NightPool.Despawn(this); });
        }

        public void OnDespawn()
        {
            _text.DOKill();
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}