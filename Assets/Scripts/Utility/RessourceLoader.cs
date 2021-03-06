﻿using System.Collections.Generic;
using UnityEngine;


public class ResourcesLoader
{
	private static Dictionary<string, Object> ResourcesMap = new Dictionary<string, Object>();

	public static void Preload<T>(string path) where T : Object
	{
		if (!ResourcesMap.ContainsKey (path))
			ResourcesMap.Add (path, Resources.Load(path));
	}

	public static T Load<T>(string path) where T : Object
	{
		T loadedObject = null;
		if (!ResourcesMap.ContainsKey (path)) {
			loadedObject = Resources.Load<T> (path);
			ResourcesMap.Add (path, loadedObject);
		} else {
			loadedObject = ResourcesMap [path] as T;
		}

		return loadedObject;
	}

	public static Object[] LoadAll(string path) 
	{
		Object[] objects = Resources.LoadAll (path);

		if (!path.EndsWith ("/"))
			path = path + "/";

		foreach (Object obj in objects) {
			ResourcesMap.Add (path + obj.name, obj);
		}

		return objects;
	}

	public static T Get<T>(string path) where T : Object
	{
		return ResourcesMap [path] as T;
	}
}


