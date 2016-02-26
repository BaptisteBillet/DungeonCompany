using UnityEngine;
using System.Collections;

public class PlayersStats : MonoBehaviour 
{
	public float m_MaxLife;
	public float m_Life;

	float m_PlayerAir;

	void Start()
	{
		m_PlayerAir = GetComponent<PlayerAir>().m_AirLife;

		// Init life amount
		m_MaxLife = 100 + m_PlayerAir;
	}

	void SetLife(float _life)
	{
		
	}
}