using TMPro;
using UnityEngine;
using Utils;

public class PlayerHealthBar : MonoBehaviour
{
	[SerializeField] private Player _player;
	public GameObject Bar;
	public SpriteRenderer BarImg;
	public TMP_Text Text;
	public TMP_Text DamageText;
	
	private float maxHP;
	
	private void Awake()
	{
		_player.OnHPChange.AddListener(OnHPChange);
		_player.OnUpgrade.AddListener(OnUpgrade);
	}

	private void OnDestroy()
	{
		_player.OnHPChange.RemoveListener(OnHPChange);
		_player.OnUpgrade.RemoveListener(OnUpgrade);
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
		var frac = deltaHp.currentHP / _player.MaxHealth;
		Text.text = $"{deltaHp.currentHP:####}/{_player.MaxHealth:####}";
		BarImg.size = new Vector2(frac, BarImg.size.y);
		var pos = BarImg.transform.localPosition;
		pos.x = -(1 - frac) / 2;
		BarImg.transform.localPosition = pos;
		if (deltaHp.currentHP <= 0)
		{
			Bar.SetActive(false);
		}
	}

	private void OnUpgrade()
	{
		DamageText.text = $"{_player.Damage}";
	}
}