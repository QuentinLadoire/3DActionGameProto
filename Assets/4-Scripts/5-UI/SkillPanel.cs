using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private Character character = null;
	[SerializeField] private ComboBar comboBar = null;
    [SerializeField] private SkillIcon attackIcon = null;
    [SerializeField] private SkillIcon dodgeIcon = null;

	private void Start()
	{
		character.DodgeComponent.inCooldownCallback += InCooldownDodgeCallback;
		character.AttackComponent.inCooldownCallback += InCooldownAttackCallback;
		character.AttackComponent.inComboCallback += InComboAttackCallback;
	}
	private void OnDestroy()
	{
		character.DodgeComponent.inCooldownCallback -= InCooldownDodgeCallback;
		character.AttackComponent.inCooldownCallback -= InCooldownAttackCallback;
		character.AttackComponent.inComboCallback -= InComboAttackCallback;
	}

	private void InCooldownDodgeCallback()
	{
		dodgeIcon.SetCooldown(character.DodgeComponent.Cooldown, character.DodgeComponent.CooldownInPercent);
	}
	private void InCooldownAttackCallback()
	{
		attackIcon.SetCooldown(character.AttackComponent.Cooldown, character.AttackComponent.CooldownInPercent);
	}
	private void InComboAttackCallback()
	{
		comboBar.SetGauge(character.AttackComponent.ComboDelayInPercent);
	}
}
