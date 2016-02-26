using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour 
{
	public GameObject m_CanvasAir;
	private bool m_CanInteract;



	void Start()
	{
		m_CanInteract = false;
	}



	void Update()
	{
		if (m_CanInteract == true)
		{
			OpenCloseMenu();
		}
	}



	// Enable controls with crystal
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Crystal")
		{
			m_CanInteract = true;
		}
	}

	// Disable controls with crystal
	void OnTriggerExit()
	{
		m_CanInteract = false;
		DeactivateCanvasElement(m_CanvasAir);
	}



	// *** FOR THE MOMENT ONLY AIR *** \\
	void OpenCloseMenu()
	{
		// Turn on
		if (Input.GetKeyDown(KeyCode.E) && m_CanvasAir.activeInHierarchy == false)
		{
			ActivateCanvasElement(m_CanvasAir);
		}

		// Turn off
		else if (Input.GetKeyDown(KeyCode.E) && m_CanvasAir.activeInHierarchy == true)
		{
			DeactivateCanvasElement(m_CanvasAir);
		}
	}



	void ActivateCanvasElement(GameObject _canvasElement)
	{
		_canvasElement.SetActive(true);
	}



	void DeactivateCanvasElement(GameObject _canvasElement)
	{
		_canvasElement.SetActive(false);
	}
}