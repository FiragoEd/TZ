using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class HealthBar : MonoBehaviour, IMobComponent
    {
        [SerializeField] private Mob.Mob _mob;
        [SerializeField] private GameObject _bar;
        [SerializeField] private SpriteRenderer _barImg;
        [SerializeField] private TMP_Text _text;

        private float _maxHP;
        
        private void Awake()
        {
            _mob.OnHPChange.AddListener(OnHPChange);
            _mob.OnDeath.AddListener(OnDeath);
        }

        private void Start()
        {
            _maxHP = _mob.MaxHealth;
            UpdateBar(_maxHP);
        }

        private void OnDestroy()
        {
            _mob.OnHPChange.RemoveListener(OnHPChange);
            _mob.OnDeath.RemoveListener(OnDeath);
        }

        public void OnDeath()
        {
            _bar.SetActive(false);
        }

        private void LateUpdate()
        {
            _bar.transform.rotation = Camera.main.transform.rotation; // TODO: рефакторинг этого этапа
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
    }
}