using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/LevelData")]
public class LevelData : ScriptableObject
{
	public int Index;

	public bool[,] GetMap() => MapCellDrawing;
	
	[ShowInInspector]
	[TableMatrix(HorizontalTitle = "Map", DrawElementMethod = "DrawColoredEnumElement", ResizableColumns = true)]
	private bool[,] MapCellDrawing = new bool[12, 12];
	
	private static bool DrawColoredEnumElement(Rect rect, bool value)
	{
		if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
		{
			value = !value;
			GUI.changed = true;
			Event.current.Use();
		}

		UnityEditor.EditorGUI.DrawRect(rect.Padding(1), value ? new Color(0.1f, 0.8f, 0.2f) : new Color(0, 0, 0, 0.5f));

		return value;
	}

	public List<WaveData> WaveDatas;
	public float WaveInterval = 5f;
}