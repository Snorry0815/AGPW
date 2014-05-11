using UnityEngine;
using System.Collections;

namespace AGPW
{
	public class UnitIdentificationInspector : MonoBehaviour 
	{
		private static string NAME = "UnitIdentificationInspector";
		private static string TEXT_PREFAB_NAME = "TextPrefab_AGPW_1_2.1";

		private GameObject unitIdentificationText = null;

		void Start () 
		{
			Init();
		}

		private void Init()
		{
			if(unitIdentificationText != null)
			{
				Destroy(unitIdentificationText);
			}
			this.unitIdentificationText = (GameObject)Instantiate(Resources.Load(TEXT_PREFAB_NAME)) as GameObject;
			this.unitIdentificationText.name = NAME;
			this.unitIdentificationText.GetComponent<TextMesh>().text = this.gameObject.name;

			this.unitIdentificationText.transform.parent = this.transform;
		}

		void Update () 
		{
			if(this.unitIdentificationText != null)
			{
				this.unitIdentificationText.transform.LookAt(2*transform.position - Camera.main.transform.position);
			}
		}
	}
}
