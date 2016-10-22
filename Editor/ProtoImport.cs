using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;
using System.Linq;

public class ProtoImport : AssetPostprocessor {

	static bool IsProto(string path) {
		
		var isProto =  Path.GetExtension(path).ToLower() == ".proto";
		return isProto;
	}
	
	static string ProtocPath {
		get {
			return Application.dataPath+"/ProtoImport/bin/"+
#if UNITY_EDITOR_OSX
			"protoc.osx"
#elif UNITY_EDITOR_WIN
			"protoc.exe"
#else
			"not-a-real-thing.fake"
#endif
			;
		}
	}
	
	static void Compile(string path) {
		path = path.Replace(" ", "\\ ");
		//var outPath = Application.dataPath+"/ProtoImport/Output"; //TODO: make this somewhere else?
		var outPath = FileUtil.GetUniqueTempPathInProject();
		Directory.CreateDirectory(outPath);
		var startInfo = new System.Diagnostics.ProcessStartInfo(ProtocPath, string.Format("--csharp_out={0} {1}", outPath, path));
		startInfo.RedirectStandardError = true;
		startInfo.RedirectStandardOutput = true;
		startInfo.UseShellExecute = false;
		var proc = System.Diagnostics.Process.Start(startInfo);
		
		proc.Start();
		proc.WaitForExit();
		if(proc.ExitCode != 0) {
			var err = proc.StandardError;
			Debug.LogError(err.ReadToEnd());
			return;
		}
		
		//move the file to original path
		var outFile = Directory.GetFiles(outPath).Single();
		if(File.Exists(path+".cs")) {
			File.Delete(path+".cs");
		}
		FileUtil.MoveFileOrDirectory(outFile, path+".cs");
	}
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
		
		foreach (var str in deletedAssets) {
			if(!IsProto(str)) continue;
			//delete cs file
			File.Delete(str+".cs");
		}
		for (var i=0; i<movedAssets.Length; i++) {
			var str = movedAssets[i];
			if(!IsProto(str)) continue;
			//move cs file
			File.Move(movedFromAssetPaths[i]+".cs", movedAssets[i]+".cs");
		}
		
		foreach (var str in importedAssets) {
			if(!IsProto(str)) continue;
			Compile(str);
		}
		
		AssetDatabase.Refresh();
	}
}
