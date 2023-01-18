using DG.Tweening;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class EnemyBar : MonoBehaviour
    {
        [SerializeField] private Mob.Mob _mob;
        [SerializeField] private MobAnimator _mobAnimator;
        [SerializeField] private GameObject _bar;
        [SerializeField] private SpriteRenderer _barImg;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private SpriteRenderer _alertImg;

        private float _maxHP;
        private Camera _camera;

        private void Awake()
        {
            _mob.OnHPChange.AddListener(OnHPChange);
            _mob.OnDeath.AddListener(OnDeath);
            _mob.OnSpawnEvent.AddListener(OnSpawn);
            _mobAnimator.OnAttack.AddListener(ShowAlertAnimation);
            _camera = Camera.main;
        }

        private void Start()
        {
            SetupBar();
        }

        private void OnDestroy()
        {
            _mob.OnSpawnEvent.RemoveListener(OnSpawn);
            _mob.OnHPChange.RemoveListener(OnHPChange);
            _mob.OnDeath.RemoveListener(OnDeath);
            _mobAnimator.OnAttack.RemoveListener(ShowAlertAnimation);
        }

        public void OnSpawn()
        {
            SetupBar();
            _bar.SetActive(true);
        }

        public void OnDeath()
        {
            _bar.SetActive(false);
        }

        private void SetupBar()
        {
            var color = _alertImg.color;
            color.a = 0f;
            _alertImg.color = color;
            _maxHP = _mob.MaxHealth;
            UpdateBar(_maxHP);
        }

        private void LateUpdate()
        {
            _bar.transform.rotation = _camera.transform.rotation;
        }

        private void UpdateBar(float hp)
        {
            var frac = hp / _maxHP;
            _text.text = $"{hp:####}/{_maxHP:####}";
            _barImg.size = new Vector2(frac, _barImg.size.y);

            var barTransform = _barImg.transform;

            var pos = barTransform.localPosition;
            pos.x = -(1 - frac) / 2;
            barTransform.localPosition = pos;
        }

        private void OnHPChange(DeltaHP deltaHp)
        {
            UpdateBar(deltaHp.currentHP);
        }

        private void ShowAlertAnimation()
        {
            _alertImg.DOKill();
            _alertImg.DOFade(1, 0.3f).OnComplete(() => { _alertImg.DOFade(0, 0.3f).SetDelay(0.5f); });
        }
    }
}