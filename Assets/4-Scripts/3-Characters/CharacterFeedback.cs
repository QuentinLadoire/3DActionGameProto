using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFeedback : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer = null;

	private bool changeColor = false;

	private Color newColor = Color.white;
	private Color defaultColor = Color.white;

	private float duration = 0.0f;
	private float durationMax = 0.0f;

	private CharacterHealthComponent healthComponent = null;

	private void Awake()
	{
		healthComponent = GetComponent<CharacterHealthComponent>();
	}
	private void Start()
	{
		defaultColor = meshRenderer.material.color;

		healthComponent.takeHealCallback += TakeHealCallback;
		healthComponent.takeDamageCallback += TakeDamageCallback;
	}
	private void Update()
	{
		ChangeColorUpdate();
	}
	private void OnDestroy()
	{
		healthComponent.takeHealCallback -= TakeHealCallback;
		healthComponent.takeDamageCallback -= TakeDamageCallback;
	}

	private void ChangeColorUpdate()
	{
		if (!changeColor) return;

		meshRenderer.material.color = Color.Lerp(defaultColor, newColor, duration / durationMax);
		duration -= Time.deltaTime;
		if (duration <= 0.0f)
		{
			duration = 0.0f;
			changeColor = false;
		}
	}
	private void ChangeColor(Color color, float duration)
	{
		newColor = color;
		durationMax = duration;
		this.duration = durationMax;

		changeColor = true;
	}

	private void TakeHealCallback()
	{
		ChangeColor(Color.green, 1.0f);
	}
	private void TakeDamageCallback()
	{
		ChangeColor(Color.red, 1.0f);
	}
}
