using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(menuName = "Data/LevelData")]
	public class LevelData : SerializedScriptableObject
	{
		[SerializeField] private int _index;
		[SerializeField] List<WaveData> _waveDatas;
		[SerializeField] float _waveInterval = 5f;
	
		[TableMatrix(HorizontalTitle = "Map", DrawElementMethod = "DrawColoredEnumElement", ResizableColumns = true)]
		[OdinSerialize]
		private bool[,] _mapCellCellDrawing = new bool[12, 12];

		public int Index => _index;
		public float WaveInterval => _waveInterval;
		public List<WaveData> WaveData => _waveDatas;
		public bool[,] MapCell => _mapCellCellDrawing;
		

		private static bool DrawColoredEnumElement(Rect rect, bool value)
		{
			if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
			{
				value = !value;
				GUI.changed = true;
				Event.current.Use();
			}

			EditorGUI.DrawRect(rect.Padding(1), value ? new Color(0.1f, 0.8f, 0.2f) : new Color(0, 0, 0, 0.5f));

			return value;
		}

	
	}
}