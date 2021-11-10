using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
	[SerializeField] private ComboBar comboBar = null;
    [SerializeField] private SkillIcon attackIcon = null;
    [SerializeField] private SkillIcon dodgeIcon = null;

    private PlayerController playerController = null;

	private void Start()
	{
		playerController = GameManager.PlayerController;

		playerController.DodgeComponent.inCooldownCallback += InCooldownDodgeCallback;
		playerController.AttackComponent.inCooldownCallback += InCooldownAttackCallback;
		playerController.AttackComponent.inComboCallback += InComboAttackCallback;
	}
	private void OnDestroy()
	{
		playerController.DodgeComponent.inCooldownCallback -= InCooldownDodgeCallback;
		playerController.AttackComponent.inCooldownCallback -= InCooldownAttackCallback;
		playerController.AttackComponent.inComboCallback -= InComboAttackCallback;
	}

	private void InCooldownDodgeCallback()
	{
		dodgeIcon.SetCooldown(playerController.DodgeComponent.Cooldown, playerController.DodgeComponent.CooldownInPercent);
	}
	private void InCooldownAttackCallback()
	{
		attackIcon.SetCooldown(playerController.AttackComponent.Cooldown, playerController.AttackComponent.CooldownInPercent);
	}
	private void InComboAttackCallback()
	{
		comboBar.SetGauge(playerController.AttackComponent.ComboDelayInPercent);
	}
}
