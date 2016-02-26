using UnityEngine;
using System.Collections;

public class PlayerAir : MonoBehaviour 
{
	public float m_AirLife;
	public float m_delayDecreaseAir;
	public float m_decreaseAir;

	public bool m_AirQuality;

	void Start()
	{
		CleanAirQuality();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "UncleanAir")
		{
			UncleanAirQuality();
			StartCoroutine(DecreaseAir());
		}

		if (other.gameObject.tag == "CleanAir")
		{			
			CleanAirQuality();
		}
	}
		

	public void SetAirCapacity(float m_AirMaxCapacity)
	{
		m_AirMaxCapacity = 60 * m_AirMaxCapacity; // 60 * is used to convert this value into minutes
		m_AirLife = m_AirMaxCapacity;
	}



	IEnumerator DecreaseAir()
	{
		while (m_AirQuality == false && m_AirLife > 0)
		{
			m_AirLife -= m_decreaseAir;
			yield return new WaitForSeconds(m_delayDecreaseAir);
		}

		yield break;
	}



	// AIR QUALITY
	// If air quality = true ; air quality is clean
	void CleanAirQuality()
	{
		m_AirQuality = true;
	}

	// If air quality = false ; air quality is unclean
	void UncleanAirQuality()
	{
		m_AirQuality = false;
	}
}