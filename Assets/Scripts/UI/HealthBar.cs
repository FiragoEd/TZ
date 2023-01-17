using TMPro;
using UnityEngine;
using Utils;

public class HealthBar : MonoBehaviour, IMobComponent
{
    public GameObject Bar;
    public SpriteRenderer BarImg;
    public TMP_Text Text;
    private float maxHP;
    private Mob.Mob _mob;

    private void Awake()
    {
        _mob = GetComponent<Mob.Mob>();
        maxHP = _mob.MaxHealth;
        _mob.OnHPChange.AddListener(OnHPChange);
    }

    private void OnDestroy()
    {
        _mob.OnHPChange.AddListener(OnHPChange);
    }

    public void OnDeath()
    {
        Bar.SetActive(false);
    }

    private void LateUpdate()
    {
        Bar.transform.rotation = Camera.main.transform.rotation;
    }

    private void OnHPChange(DeltaHP deltaHp)
    {
        var frac = deltaHp.currentHP / maxHP;
        Text.text = $"{deltaHp.currentHP:####}/{maxHP:####}";
        BarImg.size = new Vector2(frac, BarImg.size.y);
        var pos = BarImg.transform.localPosition;
        pos.x = -(1 - frac) / 2;
        BarImg.transform.localPosition = pos;
    }
}