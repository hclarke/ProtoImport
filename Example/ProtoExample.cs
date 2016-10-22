using UnityEngine;
using System.Collections;
using Google.Protobuf;

public class ProtoExample : MonoBehaviour {

	//simple program to show you how to create, write, read, and use a protobuf generated class

	Tutorial.Person Create() {
		var p = new Tutorial.Person();
		p.Name = "Metatron";
		p.Email = "voice@god.com";
		p.Id = 0;
		
		var phone = new Tutorial.Person.Types.PhoneNumber();
		phone.Number = "000-000-0000";
		phone.Type = Tutorial.Person.Types.PhoneType.Work;
		p.Phones.Add(phone);
		
		return p;
	}
	
	byte[] serialized;
	
	// Use this for initialization
	void Start () {
	
		var p = Create();
		
		serialized = p.ToByteArray();
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		var p = Tutorial.Person.Parser.ParseFrom(serialized) as Tutorial.Person;
		
		GUILayout.Label("Name: " + p.Name);
		GUILayout.Label("Id: " + p.Id);
		GUILayout.Label("Email: " + p.Email);
		foreach(var phone in p.Phones) {
			GUILayout.Label("  Type: " + phone.Type);
			GUILayout.Label("  Number: " + phone.Number);
		}
	}
}
