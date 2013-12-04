using UnityEngine;
using System.Collections;

public class HomeView : IView 
{

	#region Propriedades

	public UIRoot ScrollView;

	public IView ProfileView;

	public IView PostView;

	#endregion Propriedades
	
	#region Metodos
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start()
	{
	
	}

	/// <summary>
	/// Activates the view.
	/// </summary>
	/// <returns>The view.</returns>
	public override void ActivateView()
	{
		ScrollView.gameObject.SetActive (true);

		base.ActivateView();
	}

	/// <summary>
	/// Gos to perfil.
	/// </summary>
	/// <returns>The to perfil.</returns>
	public void GoToPerfil()
	{
		ScrollView.gameObject.SetActive (false);

		ProfileView.gameObject.SetActive (true);

		ProfileView.ActivateView ();
	}

	public void GoToPost()
	{
		ScrollView.gameObject.SetActive (false);
		
		PostView.gameObject.SetActive (true);
		
		PostView.ActivateView ();
	}

	#endregion Metodos
}
