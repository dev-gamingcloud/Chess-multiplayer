﻿using System.IO;
using UnityEngine;
using UnityEditor;
using gamingCloud;
using Newtonsoft.Json;

public class InitializeGCService : EditorWindow
{

    string ServiceToken = "";
    string PrivateKey = "";
    string PrivateMap = "";
    [MenuItem("GamingCloud/Initialize")]
    static void Init()
    {
        InitializeGCService window = (InitializeGCService)EditorWindow.GetWindow(typeof(InitializeGCService));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();


        GUILayout.Label("Setup Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        ServiceToken = EditorGUILayout.TextField("Enter Service Token:", ServiceToken);
        EditorGUILayout.Space();

        PrivateMap = EditorGUILayout.TextField("Enter Private Map:", PrivateMap);
        EditorGUILayout.Space();

        PrivateKey = EditorGUILayout.TextField("Enter Private Key:", PrivateKey);


        if (GUILayout.Button("Generate Config"))
            GenerateFile();

        EditorGUILayout.EndVertical();
    }

    void GenerateFile()
    {

        string filePath = "Assets/Resources/GamingCloud.txt";
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        ConfigTemplate sd = new ConfigTemplate();
        sd.PrivateKey = PrivateKey;
        sd.PrivateMap = PrivateMap;
        sd.ServiceToken = ServiceToken;
        outStream.WriteLine(JsonConvert.SerializeObject(sd));
        outStream.Close();
        Debug.Log("Saved Successfully");

    }



}


