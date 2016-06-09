//Created By: Jeremy Bond
//Date: 25/03

using UnityEngine;

public class InGameTutorial : MonoBehaviour
{
	[SerializeField] private GameObject digTutorial;
	[SerializeField] private GameObject moveTutorial;

	/// <summary>
	/// The correct gameobject is disabled.
	/// </summary>
	protected void Awake ()
	{
		digTutorial.SetActive(false);
	}
	/// <summary>
	/// When the object is activated, the event listeners are added.
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddListener(StaticEventNames.TUTORIALMOVEMENTSTEP, MovementUnderstood); 
		EventManager.AddListener(StaticEventNames.TUTORIALDIGSTEP, DiggingUnderstood); 
	}
	/// <summary>
	/// When the object is disabled, the event listeners are removed.
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveListener(StaticEventNames.TUTORIALMOVEMENTSTEP, MovementUnderstood);
		EventManager.RemoveListener(StaticEventNames.TUTORIALDIGSTEP, DiggingUnderstood);
	}
	/// <summary>
	/// Continued the tutorial when movement is understood.
	/// </summary>
	private void MovementUnderstood ()
	{
		ContinueTutorial(true);
	}
	/// <summary>
	/// Continued the tutorial when digging is understood.
	/// </summary>
	private void DiggingUnderstood ()
	{
		ContinueTutorial(false);
	}
	/// <summary>
	/// Continue the totorial depending on the given bool.
	/// </summary>
	/// <param name="moving">Decides which part is understood</param>
	private void ContinueTutorial (bool moving)
	{
		if (moving)
		{
			moveTutorial.SetActive (false);
			digTutorial.SetActive (true);
		}
		else
		{
			digTutorial.SetActive (false);
		}
	}
}

