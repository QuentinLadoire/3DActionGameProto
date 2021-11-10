using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private Player player = null;
	[SerializeField] private ComboBar comboBar = null;
    [SerializeField] private SkillIcon attackIcon = null;
    [SerializeField] private SkillIcon dodgeIcon = null;

	private void Start()
	{
		player.DodgeComponent.inCooldownCallback += InCooldownDodgeCallback;
		player.AttackComponent.inCooldownCallback += InCooldownAttackCallback;
		player.AttackComponent.inComboCallback += InComboAttackCallback;
	}
	private void OnDestroy()
	{
		player.DodgeComponent.inCooldownCallback -= InCooldownDodgeCallback;
		player.AttackComponent.inCooldownCallback -= InCooldownAttackCallback;
		player.AttackComponent.inComboCallback -= InComboAttackCallback;
	}

	private void InCooldownDodgeCallback()
	{
		dodgeIcon.SetCooldown(player.DodgeComponent.Cooldown, player.DodgeComponent.CooldownInPercent);
	}
	private void InCooldownAttackCallback()
	{
		attackIcon.SetCooldown(player.AttackComponent.Cooldown, player.AttackComponent.CooldownInPercent);
	}
	private void InComboAttackCallback()
	{
		comboBar.SetGauge(player.AttackComponent.ComboDelayInPercent);
	}
}
