using extOSC.Editor;
using extOSC.Editor.Windows;
using UnityEditor;
using UnityEngine;

namespace MyOscComponents.EditorNS
{
	
	public class MyOscBindingWindow : OSCWindow<MyOscBindingWindow, OscBindingsPanel>
	{
		[MenuItem("Tools/My OSC/" + "OSC Bindings")]
		public static void Open()
		{
			Instance.titleContent = new GUIContent("OSC Bindings", OSCEditorTextures.IronWallSmall);
			Instance.minSize = new Vector2(550, 200);
			Instance.Show();
			Instance.Focus();
		}
		private readonly string _lastFileSettings = OSCEditorSettings.Debug + "lastfile";
		
		private OscBindingsPanel bindingsPanel;
		
		protected override void OnEnable()
		{
			bindingsPanel = new OscBindingsPanel(this);
			base.OnEnable();
		}

		protected void OnInspectorUpdate()
		{
			bindingsPanel?.Refresh();
			Repaint();
		}
		
		protected override void SaveWindowSettings()
		{
			// if (bindingsPanel == null) return;
			//
			// var debugPacket = bindingsPanel.CurrentPacket;
			// if (debugPacket != null)
			// {
			// 	if (string.IsNullOrEmpty(_packetEditorPanel.FilePath))
			// 	{
			// 		_packetEditorPanel.FilePath = OSCEditorUtils.BackupFolder + "unsaved.eod";
			// 	}
			//
			// 	OSCEditorUtils.SavePacket(_packetEditorPanel.FilePath, debugPacket);
			// 	OSCEditorSettings.SetString(_lastFileSettings, _packetEditorPanel.FilePath);
			//
			// 	return;
			// }
			//
			// OSCEditorSettings.SetString(_lastFileSettings, "");
		}

		protected override void LoadWindowSettings()
		{
			// if (_packetEditorPanel == null) return;
			//
			// var lastOpenedFile = OSCEditorSettings.GetString(_lastFileSettings, "");
			//
			// if (!string.IsNullOrEmpty(lastOpenedFile))
			// {
			// 	var debugPacket = OSCEditorUtils.LoadPacket(lastOpenedFile);
			// 	if (debugPacket != null)
			// 	{
			// 		_packetEditorPanel.CurrentPacket = debugPacket;
			// 		_packetEditorPanel.FilePath = lastOpenedFile;
			// 	}
			// }
		}

	}
}