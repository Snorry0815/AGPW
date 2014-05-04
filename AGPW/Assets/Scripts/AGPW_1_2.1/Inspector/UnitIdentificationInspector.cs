using UnityEngine;
using System.Collections;

public class UnitIdentificationInspector : MonoBehaviour 
{
	public Font font;

	private GameObject unitIdentificationText = null;
	private TextMesh textMesh = null;
	// Use this for initialization
	void Start () 
	{
		Init();
	}

	private void Init()
	{
		this.unitIdentificationText = new GameObject("UnitIdentificationInspector");
		this.unitIdentificationText.transform.parent = this.transform;
		this.unitIdentificationText.AddComponent<MeshRenderer>();
		this.textMesh = this.unitIdentificationText.AddComponent<TextMesh>();
		this.textMesh.font = Resources.Load("Arial", typeof(Font)) as Font;
		this.textMesh.font = font;
		this.textMesh.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		this.textMesh.fontSize = 50; 
		this.textMesh.text = this.name;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.unitIdentificationText != null)
		{
			this.unitIdentificationText.transform.LookAt(2*transform.position - Camera.main.transform.position);
		}
	}
}
