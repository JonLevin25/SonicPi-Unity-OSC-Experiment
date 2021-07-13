using System.Reflection;
using extOSC;
using extOSC.Core;
using extOSC.Editor;
using extOSC.Editor.Panels;
using extOSC.Editor.Windows;
using GDX;
using UnityEditor;
using UnityEngine;

namespace MyOscComponents.EditorNS
{
    public class OscBindingsPanel : OSCPanel
    {
        private static readonly GUIContent _receiversContent = new GUIContent("Receivers:");
        private static readonly GUIContent _triggeredFXContent = new GUIContent("Triggerable FX:");

        private OscReceiverBinding[] _receiverBindings;
        private Vector2 _scrollPosition;
        private MethodInfo _receiveMethod;
        private Color _defaultColor;
        private OscTriggeredEffect[] _triggerableFX;

        protected override void DrawContent(ref Rect contentRect)
        {
            _defaultColor = GUI.color;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                DrawRefreshButton();
                GUILayout.FlexibleSpace();
            }

            using (var scroll = new GUILayout.ScrollViewScope(_scrollPosition))
            {
                var expand = contentRect.width > 350;
                if (expand) GUILayout.BeginHorizontal();

                DrawReceivers();
                DrawTriggeredFX();

                if (expand) GUILayout.EndHorizontal();

                _scrollPosition = scroll.scrollPosition;
            }
        }

        private void DrawReceivers()
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Label(_receiversContent, OSCEditorStyles.ConsoleBoldLabel);

                if (_receiverBindings.Length > 0)
                {
                    foreach (var t in _receiverBindings)
                    {
                        DrawTransBinding(t);
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("Scene doesn't have any Receiver Bindings.", MessageType.Info);
                }
            }
        }
        
        private void DrawTriggeredFX()
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Label(_triggeredFXContent, OSCEditorStyles.ConsoleBoldLabel);

                if (_triggerableFX.Length > 0)
                {
                    foreach (var t in _triggerableFX)
                    {
                        DrawTriggerable(t);
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("Scene doesn't have any Triggered FX.", MessageType.Info);
                }
            }
        }

        private void DrawRefreshButton()
        {
            // const float size = 40f;

            // var oldSize = GUI.skin.button.fontSize;
            // GUI.skin.button.fontSize = Mathf.RoundToInt(size * 0.5f);
            // if (GUILayout.Button("↻", GUILayout.Width(size), GUILayout.Height(size)))
            if (GUILayout.Button("↻"))
            {
                Refresh();
            }

            // GUI.skin.button.fontSize = oldSize;
        }

        public OscBindingsPanel(OSCWindow window) : base(window)
        {
            Refresh();
        }

        public void Refresh()
        {
            _receiverBindings = Object.FindObjectsOfType<OscReceiverBinding>();
            _triggerableFX = Object.FindObjectsOfType<OscTriggeredEffect>();
        }

        private void DrawTransBinding(OscReceiverBinding transBind)
        {
            // GUI.color = osc.IsStarted ? Color.green : Color.red;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new GUILayout.VerticalScope(OSCEditorStyles.Box))
                {
                    if (GUILayout.Button($"{transBind.name} ({transBind.transform.GetScenePath()})"))
                    {
                        EditorGUIUtility.PingObject(transBind);
                        Selection.activeObject = transBind;
                    }
                    GUILayout.Label($"Address: {transBind.Address}");
                }

                // GUILayout.Label(_actionsContent);
                using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
                {
                    // DrawActions(transBind);
                }
            }

            GUI.color = _defaultColor;
        }
        
        private void DrawTriggerable(OscTriggeredEffect triggerablke)
        {
            // GUI.color = osc.IsStarted ? Color.green : Color.red;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new GUILayout.VerticalScope(OSCEditorStyles.Box))
                {
                    if (GUILayout.Button($"{triggerablke.name} ({triggerablke.transform.GetScenePath()})"))
                    {
                        EditorGUIUtility.PingObject(triggerablke);
                        Selection.activeObject = triggerablke;
                    }
                }

                // GUILayout.Label(_actionsContent);
                using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
                {
                    // DrawActions(transBind);
                }
            }

            GUI.color = _defaultColor;
        }

        private void DrawActions(OSCBase osc)
        {
            GUI.color = Color.yellow;
            GUI.enabled = osc.IsStarted;

            switch (osc)
            {
                case OSCTransmitter transmitter:
                    if (GUILayout.Button("_sendActionContent"))
                    {
                        var debugPacket = OSCWindowDebug.CurrentPacket;
                        if (debugPacket != null)
                        {
                            transmitter.Send(debugPacket.Copy(), OSCSendOptions.IgnoreBundle);
                        }
                    }

                    break;

                case OSCReceiver receiver:
                    if (GUILayout.Button("_receiveActionContent"))
                    {
                        var debugPacket = OSCWindowDebug.CurrentPacket;
                        if (debugPacket != null)
                        {
                            if (_receiveMethod == null)
                                _receiveMethod = typeof(OSCReceiver).GetMethod("PacketReceived",
                                    BindingFlags.Instance | BindingFlags.NonPublic);

                            _receiveMethod.Invoke(receiver, new object[] {debugPacket.Copy()});
                        }
                    }

                    break;
            }

            GUI.enabled = true;
            GUI.color = _defaultColor;
            
            if (GUILayout.Button("_selectActionContent", GUILayout.MaxWidth(60)))
            {
                // EditorGUIUtility.PingObject(osc);
                Selection.activeGameObject = osc.gameObject;
            }
        }
    }
}