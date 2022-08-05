using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] ParticleSystem _spawnVfx;
	[SerializeField] ParticleSystem _deSpawnVfx;


	public void ActivateSpawnVFX()
	{
		_spawnVfx?.Play();
	}
	public void ActivateDespawnVFX()
	{
		_deSpawnVfx?.Play();
	}

}
