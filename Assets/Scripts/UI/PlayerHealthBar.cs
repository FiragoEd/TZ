using TMPro;
using UnityEngine;
using Utils;

public class PlayerHealthBar : MonoBehaviour
{
	public GameObject Bar;
	public SpriteRenderer BarImg;
	public TMP_Text Text;
	public TMP_Text DamageText;
	private float maxHP;
	private Player player;
	private void Awake()
	{
		player = GetComponent<Player>();
		player.OnHPChange.AddListener(OnHPChange);
	}

	private void OnDestroy()
	{
		player.OnHPChange.RemoveListener(OnHPChange);
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
		var frac = deltaHp.currentHP / player.MaxHealth;
		Text.text = $"{deltaHp.currentHP:####}/{player.MaxHealth:####}";
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
		DamageText.text = $"{player.Damage}";
	}
}