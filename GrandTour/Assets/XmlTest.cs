using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

// xml 입출력을 위한 namespace
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 

public class CharacterInfo
{
	//[XmlAttribute("ID")] // 굳이 Attribute를 줄 필요는 없다.
	public int id;
	public string name;
	public int level;
	
	[XmlArray("Skills"), XmlArrayItem("SkillInfo")]
	public List<SkillInfo> skills = new List<SkillInfo>();
}

public class SkillInfo
{
	public int id;
	public string name;
}

public class XmlTest : MonoBehaviour
{
	[XmlArray("Characters"), XmlArrayItem("CharacterInfo")]
	private List<CharacterInfo> characters = new List<CharacterInfo>();
	
	void Start ()
	{	
		// create dummy data
		for(int i=0; i<5; ++i)
		{
			CharacterInfo cInfo = new CharacterInfo();
			cInfo.id = Random.Range(1000, 9999);
			cInfo.name = "Char" + cInfo.id.ToString();
			cInfo.level = Random.Range(1, 100);			
			
			for(int j=0; j<2; ++j)
			{
				SkillInfo sInfo = new SkillInfo();
				sInfo.id = Random.Range(10000, 99999);
				sInfo.name = "Skill" + sInfo.id.ToString();
				cInfo.skills.Add(sInfo);
			}
			characters.Add(cInfo);
		}
		GameObject.Find ("Result Text").GetComponent<GUIText>().text = "데이터 생성 완료";
	}

	void OnGUI()
	{
		// Android 의 경우 Read/Write 에 제약이 있으므로 persistentDataPath를 이용해야 한다.
		// 맥 관련 기기는 없어서 테스트 못함.
#if	UNITY_ANDROID
		string path = Application.persistentDataPath;
#else
		string path = Application.dataPath + "/Resources/XML";
		if(!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
#endif
		path += "/test.xml";

		if (GUI.Button(new Rect(10, 10, 100, 50), "Save"))
		{
			XmlSave(path);
		//	XmlSaveUTF8(path);
			#if UNITY_EDITOR
			AssetDatabase.Refresh();
			#endif
		}
		
		if(GUI.Button(new Rect(10, 80, 100, 50), "Load"))
		{
			if(File.Exists(path))
			{
				string result = string.Empty;
			//	var test = XmlLoad(path);
				var test = XmlLoadUTF8(path);
				for(int i = 0; i < test.Count; ++i)
				{
					result += i.ToString() + ": " + test[i].id.ToString() + "," + test[i].name + "," + test[i].level.ToString() + "\n";
					for(int j = 0; j < test[i].skills.Count; ++j)
					{
						result += "\t" + "Skill" + j.ToString() + ": " + test[i].skills[j].id.ToString() + "," + test[i].skills[j].name + "\n";
					}
				}
				GameObject.Find ("Result Text").GetComponent<GUIText>().text = result;
			}
		}
	}
	
	/// <summary>
	/// <subject>Xml Encoding Problem</subject>
	/// <description>
	/// Windows를 사용하는 pc에서 XmlSerializer를 사용하여 Xml 파일을 생성하면
	/// ks_c_5601-1987 으로 인코딩 된다. 이 인코딩은 MS 계열에서만 사용하므로
	/// UTF-8 로 변경해야 한다. 기기에 따라 달라지므로 저장을 할 때 UTF-8 로
	/// 저장한다면 읽어올 때 굳이 StreamReader 를 사용하여 인코딩을 변경할 
	/// 필요가 없다. 하지만 외부에서 만들어진 Xml을 사용할 경우 StreamReader를
	/// 사용하여 UTF-8 로 변경하여 읽어 올 필요가 있다.
	/// </description>
	/// </summary>
	void XmlSaveUTF8(string path)
	{
		var serializer = new XmlSerializer(typeof(List<CharacterInfo>));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
			serializer.Serialize(streamWriter, this.characters);
		}
		GameObject.Find ("Result Text").GetComponent<GUIText>().text = "파일 저장 완료";
	}
	
	void XmlSave(string path)
	{
		var serializer = new XmlSerializer(typeof(List<CharacterInfo>));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this.characters);
		}
		GameObject.Find ("Result Text").GetComponent<GUIText>().text = "파일 저장 완료";
	}
	
	void XmlSave(System.Type type, string path)
	{
		var serializer = new XmlSerializer(type);
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this.characters);
		}
		GameObject.Find ("Result Text").GetComponent<GUIText>().text = "파일 저장 완료";
	}
	
	public List<CharacterInfo> XmlLoadUTF8(string path)
	{
		var serializer = new XmlSerializer(typeof(List<CharacterInfo>));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
			return (List<CharacterInfo>)serializer.Deserialize(streamReader);
		}
	}
	
	public List<CharacterInfo> XmlLoad(string path)
	{
		var serializer = new XmlSerializer(typeof(List<CharacterInfo>));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return (List<CharacterInfo>)serializer.Deserialize(stream);
		}
	}
}
