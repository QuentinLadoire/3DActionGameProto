using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDespawnComponent : MonoBehaviour
{
	//Paramerters
	private float despawnDelayMax = 2.0f;
	private float scalingDurationMax = 2.0f;

	//Processing Variables
	private float despawnDelay = 0.0f;
	private float scalingDuration = 0.0f;

	private bool inDespawn = false;
	private bool inScaling = false;
	private bool needToDestroy = false;

	private void Update()
	{
		if (inDespawn)
		{
			if (despawnDelay <= 0.0f)
			{
				inScaling = true;
				inDespawn = false;
			}
			despawnDelay -= Time.deltaTime;
		}

		if (inScaling)
		{
			Scale(scalingDuration / scalingDurationMax);

			if (scalingDuration <= 0.0f)
			{
				needToDestroy = true;
				inScaling = false;
			}
			scalingDuration -= Time.deltaTime;
		}

		if (needToDestroy)
		{
			Destroy(gameObject);
			needToDestroy = false;
		}
	}

	private void Scale(float percent)
	{
		var scale = Mathf.Lerp(0.0f, 1.0f, percent);
		transform.localScale = new Vector3(scale, scale, scale);
	}

	public void Init(float despawnDelayMax, float scalingDurationMax)
	{
		this.despawnDelayMax = despawnDelayMax;
		this.scalingDurationMax = scalingDurationMax;
	}
	public void Despawn()
	{
		despawnDelay = despawnDelayMax;
		scalingDuration = scalingDurationMax;

		inDespawn = true;
	}
}
