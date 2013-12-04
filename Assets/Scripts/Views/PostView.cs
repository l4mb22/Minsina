using UnityEngine;
using System.Collections;

public class PostView : IView 
{
	/// <summary>
	/// The register view.
	/// </summary>
	public IView LoginView;

	/// <summary>
	/// The content of the post.
	/// </summary>
	public UIInput PostContent;

	/// <summary>
	/// The primary tag.
	/// </summary>
	public UIInput PrimaryTag;

	/// <summary>
	/// The secondary tag.
	/// </summary>
	public UIInput SecondaryTag;

	/// <summary>
	/// Cos the post.
	/// </summary>
	/// <returns>The post.</returns>
	private IEnumerator CoPost()
	{
		WWWForm form = new WWWForm();

		form.AddField("login", LoginView.gameObject.GetComponent<LoginView>().LoginLabel.text);

		form.AddField ("post", PostContent.text);

		form.AddField ("tag1", PrimaryTag.text);

		form.AddField ("tag2", SecondaryTag.text);

		WWW www = new WWW("localhost/post.php?", form); 
		
		yield return www;
		
		Debug.Log(www.text);

		if(www.text.Equals("done"))
		{
			PostContent.text = "";

			PrimaryTag.text = "";

			SecondaryTag.text = "";
		}
		else
		{
			
		}
	}

	public void Post()
	{
		StartCoroutine ("CoPost");
	}
}
