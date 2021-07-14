using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace MyOscComponents.EditorNS
{
    public static class SwitchRenderPipelinesShortcut
    {
        [MenuItem("Tools/Toggle URP %U")]
        public static void SwitchPipeline()
        {
            var pipelineAsset = GraphicsSettings.defaultRenderPipeline;
            var wasOnSrp = pipelineAsset != null;
            if (wasOnSrp)
            {
                GraphicsSettings.defaultRenderPipeline = null;
                NotifyScene("Switched to (global) Default Renderer");
            }
            else
            {
                var pipelineAssets = AssetDatabase
                    .FindAssets($"t: {typeof(RenderPipelineAsset).FullName}")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>)
                    .ToArray();

                if (pipelineAssets.Length == 0)
                {
                    NotifyScene($"No pipelines found.");
                    return;
                }
                
                var pipe = pipelineAssets[0];
                GraphicsSettings.defaultRenderPipeline = pipe;
                if (pipelineAssets.Length > 1)
                {
                    NotifyScene($"Multiple pipelines. Set (global) to {pipe.name}");
                }
                else
                {
                    NotifyScene($"Set (global) pipeline to {pipe.name}");
                }
            }
        }

        private static void NotifyScene(string text)
        {
            SceneView.lastActiveSceneView.ShowNotification(new GUIContent(text));
        }
    }
}