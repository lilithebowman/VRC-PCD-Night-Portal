﻿
using UnityEngine;

using UnityEditor;
using UdonSharpEditor;

namespace Texel
{
    // TODO: Checks on overrides
    // [ ] Do all overrides have a mapping profile selected?
    // [ ] Do property override objects have material with CRT set?  That is probably a mixup
    // [X] Update CRT at edittime for logo?

    [CustomEditor(typeof(ScreenManager))]
    internal class ScreenManagerInspector : Editor
    {
        static bool _showErrorMatFoldout;
        static bool _showErrorTexFoldout;
        static bool _showScreenListFoldout;
        static bool[] _showScreenFoldout = new bool[0];
        static bool _showMaterialListFoldout;
        static bool[] _showMaterialFoldout = new bool[0];
        static bool _showPropListFoldout;
        static bool[] _showPropFoldout = new bool[0];

        SerializedProperty videoPlayerProperty;

        SerializedProperty debugLogProperty;

        SerializedProperty useMaterialOverrideProperty;
        SerializedProperty separatePlaybackMaterialsProperty;
        SerializedProperty playbackMaterialProperty;
        SerializedProperty playbackMaterialUnityProperty;
        SerializedProperty playbackMaterialAVProProperty;
        SerializedProperty logoMaterialProperty;
        SerializedProperty loadingMaterialProperty;
        SerializedProperty syncMaterialProperty;
        SerializedProperty audioMaterialProperty;
        SerializedProperty errorMaterialProperty;
        SerializedProperty errorInvalidMaterialProperty;
        SerializedProperty errorBlockedMaterialProperty;
        SerializedProperty errorRateLimitedMaterialProperty;
        SerializedProperty editorMaterialProperty;

        SerializedProperty screenMeshListProperty;
        SerializedProperty screenMatIndexListProperty;

        SerializedProperty useTextureOverrideProperty;
        SerializedProperty overrideAspectRatioProperty;
        SerializedProperty aspectRatioProperty;
        SerializedProperty logoTextureProperty;
        SerializedProperty loadingTextureProperty;
        SerializedProperty syncTextureProperty;
        SerializedProperty audioTextureProperty;
        SerializedProperty errorTextureProperty;
        SerializedProperty errorInvalidTextureProperty;
        SerializedProperty errorBlockedTextureProperty;
        SerializedProperty errorRateLimitedTextureProperty;
        SerializedProperty editorTextureProperty;

        SerializedProperty materialUpdateListProperty;
        SerializedProperty materialPropertyListProperty;
        SerializedProperty materialTexPropertyListProperty;
        SerializedProperty materialAVPropertyListProperty;

        SerializedProperty propRenderListProperty;
        SerializedProperty propMaterialOverrideListProperty;
        SerializedProperty propMaterialIndexListProperty;
        SerializedProperty propPropertyListProperty;

        SerializedProperty useRenderOutProperty;
        SerializedProperty outputCRTProperty;
        SerializedProperty outputMaterialPropertiesProperty;

        private void OnEnable()
        {
            videoPlayerProperty = serializedObject.FindProperty(nameof(ScreenManager.videoPlayer));

            debugLogProperty = serializedObject.FindProperty(nameof(ScreenManager.debugLog));

            useMaterialOverrideProperty = serializedObject.FindProperty(nameof(ScreenManager.useMaterialOverrides));
            separatePlaybackMaterialsProperty = serializedObject.FindProperty(nameof(ScreenManager.separatePlaybackMaterials));
            playbackMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.playbackMaterial));
            playbackMaterialUnityProperty = serializedObject.FindProperty(nameof(ScreenManager.playbackMaterialUnity));
            playbackMaterialAVProProperty = serializedObject.FindProperty(nameof(ScreenManager.playbackMaterialAVPro));
            logoMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.logoMaterial));
            loadingMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.loadingMaterial));
            syncMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.syncMaterial));
            audioMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.audioMaterial));
            errorMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.errorMaterial));
            errorInvalidMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.errorInvalidMaterial));
            errorBlockedMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.errorBlockedMaterial));
            errorRateLimitedMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.errorRateLimitedMaterial));
            editorMaterialProperty = serializedObject.FindProperty(nameof(ScreenManager.editorMaterial));

            screenMeshListProperty = serializedObject.FindProperty(nameof(ScreenManager.screenMesh));
            screenMatIndexListProperty = serializedObject.FindProperty(nameof(ScreenManager.screenMaterialIndex));

            useTextureOverrideProperty = serializedObject.FindProperty(nameof(ScreenManager.useTextureOverrides));
            overrideAspectRatioProperty = serializedObject.FindProperty(nameof(ScreenManager.overrideAspectRatio));
            aspectRatioProperty = serializedObject.FindProperty(nameof(ScreenManager.aspectRatio));
            logoTextureProperty = serializedObject.FindProperty(nameof(ScreenManager.logoTexture));
            loadingTextureProperty = serializedObject.FindProperty(nameof(ScreenManager.loadingTexture));
            syncTextureProperty = serializedObject.FindProperty(nameof(ScreenManager.syncTexture));
            audioTextureProperty = serializedObject.FindProperty(nameof(ScreenManager.audioTexture));
            errorTextureProperty = serializedObject.FindProperty(nameof(ScreenManager.errorTexture));
            errorInvalidTextureProperty = serializedObject.FindProperty(nameof(ScreenManager.errorInvalidTexture));
            errorBlockedTextureProperty = serializedObject.FindProperty(nameof(ScreenManager.errorBlockedTexture));
            errorRateLimitedTextureProperty = serializedObject.FindProperty(nameof(ScreenManager.errorRateLimitedTexture));
            editorTextureProperty = serializedObject.FindProperty(nameof(ScreenManager.editorTexture));

            materialUpdateListProperty = serializedObject.FindProperty(nameof(ScreenManager.materialUpdateList));
            materialTexPropertyListProperty = serializedObject.FindProperty(nameof(ScreenManager.materialTexPropertyList));
            materialAVPropertyListProperty = serializedObject.FindProperty(nameof(ScreenManager.materialAVPropertyList));
            materialPropertyListProperty = serializedObject.FindProperty(nameof(ScreenManager.materialPropertyList));

            propRenderListProperty = serializedObject.FindProperty(nameof(ScreenManager.propMeshList));
            propMaterialOverrideListProperty = serializedObject.FindProperty(nameof(ScreenManager.propMaterialOverrideList));
            propMaterialIndexListProperty = serializedObject.FindProperty(nameof(ScreenManager.propMaterialIndexList));
            propPropertyListProperty = serializedObject.FindProperty(nameof(ScreenManager.propPropertyList));

            useRenderOutProperty = serializedObject.FindProperty(nameof(ScreenManager.useRenderOut));
            outputCRTProperty = serializedObject.FindProperty(nameof(ScreenManager.outputCRT));
            outputMaterialPropertiesProperty = serializedObject.FindProperty(nameof(ScreenManager.outputMaterialProperties));

            // CRT texture
            UpdateEditorState();
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target))
                return;

            if (GUILayout.Button("Screen Manager Documentation"))
                Application.OpenURL("https://github.com/jaquadro/VideoTXL/wiki/Configuration:-Screen-Manager");

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(videoPlayerProperty);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Optional Components", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(debugLogProperty);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Object Material Overrides", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(useMaterialOverrideProperty);
            if (useMaterialOverrideProperty.boolValue)
            {
                EditorGUILayout.PropertyField(separatePlaybackMaterialsProperty);
                if (separatePlaybackMaterialsProperty.boolValue)
                {
                    EditorGUILayout.PropertyField(playbackMaterialUnityProperty);
                    EditorGUILayout.PropertyField(playbackMaterialAVProProperty);
                }
                else
                    EditorGUILayout.PropertyField(playbackMaterialProperty);

                EditorGUILayout.PropertyField(logoMaterialProperty);
                EditorGUILayout.PropertyField(loadingMaterialProperty);
                // EditorGUILayout.PropertyField(syncMaterialProperty);
                EditorGUILayout.PropertyField(audioMaterialProperty);
                EditorGUILayout.PropertyField(errorMaterialProperty);

                _showErrorMatFoldout = EditorGUILayout.Foldout(_showErrorMatFoldout, "Error Material Overrides");
                if (_showErrorMatFoldout)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(errorInvalidMaterialProperty);
                    EditorGUILayout.PropertyField(errorBlockedMaterialProperty);
                    EditorGUILayout.PropertyField(errorRateLimitedMaterialProperty);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.PropertyField(editorMaterialProperty);

                EditorGUILayout.Space();
                ScreenFoldout();
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Material Texture Updates and Overrides", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(useTextureOverrideProperty);
            if (useTextureOverrideProperty.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(overrideAspectRatioProperty);
                if (overrideAspectRatioProperty.boolValue)
                    EditorGUILayout.PropertyField(aspectRatioProperty);

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(logoTextureProperty);
                EditorGUILayout.PropertyField(loadingTextureProperty);
                // EditorGUILayout.PropertyField(syncTextureProperty);
                EditorGUILayout.PropertyField(audioTextureProperty);
                EditorGUILayout.PropertyField(errorTextureProperty);

                _showErrorTexFoldout = EditorGUILayout.Foldout(_showErrorTexFoldout, "Error Texture Overrides");
                if (_showErrorTexFoldout)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(errorInvalidTextureProperty);
                    EditorGUILayout.PropertyField(errorBlockedTextureProperty);
                    EditorGUILayout.PropertyField(errorRateLimitedTextureProperty);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.PropertyField(editorTextureProperty);

                EditorGUILayout.Space();
                MaterialFoldout();
                EditorGUILayout.Space();
                PropBlockFoldout();

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Render Texture Output", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(useRenderOutProperty);
                if (useRenderOutProperty.boolValue)
                {
                    EditorGUILayout.PropertyField(outputCRTProperty);
                    EditorGUILayout.PropertyField(outputMaterialPropertiesProperty);

                    if (outputMaterialPropertiesProperty.objectReferenceValue == null)
                        EditorGUILayout.HelpBox($"No property map set. The screen manager will not be able to update properties on the CRT material.", MessageType.Error);
                }
                else
                    EditorGUILayout.HelpBox("Enabling the Render Texture Output is the easiest way to supply a video texture to other shaders and materials.  For the most control and performance, use Material or Material Property Block overrides.", MessageType.Info);
            }

            if (serializedObject.hasModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties();
                UpdateEditorState();
            }
        }

        private void ScreenFoldout()
        {
            int count = screenMeshListProperty.arraySize;
            _showScreenListFoldout = EditorGUILayout.Foldout(_showScreenListFoldout, $"Video Screen Objects ({count})");
            if (_showScreenListFoldout)
            {
                EditorGUI.indentLevel++;
                _showScreenFoldout = EditorTools.MultiArraySize(serializedObject, _showScreenFoldout,
                    screenMeshListProperty, screenMatIndexListProperty);
                
                for (int i = 0; i < screenMeshListProperty.arraySize; i++)
                {
                    string name = EditorTools.GetMeshRendererName(screenMeshListProperty, i);
                    _showScreenFoldout[i] = EditorGUILayout.Foldout(_showScreenFoldout[i], $"Screen {i} ({name})");
                    if (_showScreenFoldout[i])
                    {
                        EditorGUI.indentLevel++;

                        SerializedProperty mesh = screenMeshListProperty.GetArrayElementAtIndex(i);
                        SerializedProperty matIndex = screenMatIndexListProperty.GetArrayElementAtIndex(i);

                        EditorGUILayout.PropertyField(mesh, new GUIContent("Mesh Renderer"));
                        EditorGUILayout.PropertyField(matIndex, new GUIContent("Material Index"));

                        EditorGUI.indentLevel--;
                    }
                }
                EditorGUI.indentLevel--;
            }
        }

        private void MaterialFoldout()
        {
            int count = materialUpdateListProperty.arraySize;
            _showMaterialListFoldout = EditorGUILayout.Foldout(_showMaterialListFoldout, $"Shared Material Updates ({count})");
            if (_showMaterialListFoldout)
            {
                EditorGUI.indentLevel++;
                _showMaterialFoldout = EditorTools.MultiArraySize(serializedObject, _showMaterialFoldout,
                    materialUpdateListProperty, materialPropertyListProperty, materialTexPropertyListProperty, materialAVPropertyListProperty);

                for (int i = 0; i < materialUpdateListProperty.arraySize; i++)
                {
                    string name = EditorTools.GetMaterialName(materialUpdateListProperty, i);
                    _showMaterialFoldout[i] = EditorGUILayout.Foldout(_showMaterialFoldout[i], $"Material {i} ({name})");
                    if (_showMaterialFoldout[i])
                    {
                        EditorGUI.indentLevel++;

                        SerializedProperty matUpdate = materialUpdateListProperty.GetArrayElementAtIndex(i);
                        SerializedProperty matProperties = materialPropertyListProperty.GetArrayElementAtIndex(i);
                        SerializedProperty matTexProperty = materialTexPropertyListProperty.GetArrayElementAtIndex(i);
                        SerializedProperty matAVProperty = materialAVPropertyListProperty.GetArrayElementAtIndex(i);

                        EditorGUILayout.PropertyField(matUpdate, new GUIContent("Material"));
                        EditorGUILayout.PropertyField(matProperties, new GUIContent("Property Map"));

                        EditorGUI.indentLevel--;
                    }
                }
                EditorGUI.indentLevel--;
            }

            int missingMapCount = MissingMapCount(materialPropertyListProperty);
            if (missingMapCount > 0)
                EditorGUILayout.HelpBox($"{missingMapCount} materials have no property map set. The screen manager will not be able to update properties on those materials.", MessageType.Error);
        }

        private void PropBlockFoldout()
        {
            int count = propRenderListProperty.arraySize;
            _showPropListFoldout = EditorGUILayout.Foldout(_showPropListFoldout, $"Material Property Block Overrides ({count})");
            if (_showPropListFoldout)
            {
                EditorGUI.indentLevel++;
                _showPropFoldout = EditorTools.MultiArraySize(serializedObject, _showPropFoldout,
                    propRenderListProperty, propMaterialOverrideListProperty, propMaterialIndexListProperty, propPropertyListProperty);

                for (int i = 0; i < propRenderListProperty.arraySize; i++)
                {
                    string name = EditorTools.GetMeshRendererName(propRenderListProperty, i);
                    _showPropFoldout[i] = EditorGUILayout.Foldout(_showPropFoldout[i], $"Material Override {i} ({name})");
                    if (_showPropFoldout[i])
                    {
                        EditorGUI.indentLevel++;

                        SerializedProperty mesh = propRenderListProperty.GetArrayElementAtIndex(i);
                        SerializedProperty useMatOverride = propMaterialOverrideListProperty.GetArrayElementAtIndex(i);
                        SerializedProperty matIndex = propMaterialIndexListProperty.GetArrayElementAtIndex(i);
                        SerializedProperty matProperties = propPropertyListProperty.GetArrayElementAtIndex(i);

                        EditorGUILayout.PropertyField(mesh, new GUIContent("Renderer"));

                        GUIContent desc = new GUIContent("Override Mode", "Whether to override a property on the renderer or one of its specific materials");
                        useMatOverride.intValue = EditorGUILayout.Popup(desc, useMatOverride.intValue, new string[] { "Renderer", "Material" });
                        if (useMatOverride.intValue == 1)
                            EditorGUILayout.PropertyField(matIndex, new GUIContent("Material Index"));

                        EditorGUILayout.PropertyField(matProperties, new GUIContent("Property Map"));

                        EditorGUI.indentLevel--;
                    }
                }
                EditorGUI.indentLevel--;
            }

            int missingMapCount = MissingMapCount(propPropertyListProperty);
            if (missingMapCount > 0)
                EditorGUILayout.HelpBox($"{missingMapCount} override(s) have no property map set. The screen manager will not be able to update material properties on those objects.", MessageType.Error);
        }

        private int MissingMapCount(SerializedProperty propArray)
        {
            int missingMapCount = 0;

            for (int i = 0; i < propArray.arraySize; i++)
            {
                SerializedProperty matProperties = propArray.GetArrayElementAtIndex(i);
                ScreenPropertyMap map = (ScreenPropertyMap)matProperties.objectReferenceValue;

                if (map == null)
                    missingMapCount += 1;
            }

            return missingMapCount;
        }

        private void UpdateEditorState()
        {
            UpdateEditorCRT();
            UpdateEditorSharedMaterials();
            UpdateEditorMaterialBlocks();
        }

        private void UpdateEditorSharedMaterials()
        {
            for (int i = 0; i < materialUpdateListProperty.arraySize; i++)
            {
                SerializedProperty matUpdate = materialUpdateListProperty.GetArrayElementAtIndex(i);
                SerializedProperty matProperties = materialPropertyListProperty.GetArrayElementAtIndex(i);

                if (matUpdate == null || matProperties == null)
                    continue;

                Material mat = (Material)matUpdate.objectReferenceValue;
                ScreenPropertyMap map = (ScreenPropertyMap)matProperties.objectReferenceValue;

                UpdateSharedMaterial(mat, map);
            }
        }

        private void UpdateEditorMaterialBlocks()
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();

            SyncPlayer videoPlayer = (SyncPlayer)videoPlayerProperty.objectReferenceValue;
            Texture2D logoTex = (Texture2D)editorTextureProperty.objectReferenceValue;
            if (logoTex == null)
                logoTex = (Texture2D)logoTextureProperty.objectReferenceValue;

            for (int i = 0; i < propRenderListProperty.arraySize; i++)
            {
                SerializedProperty meshProp = propRenderListProperty.GetArrayElementAtIndex(i);
                SerializedProperty useMatOverride = propMaterialOverrideListProperty.GetArrayElementAtIndex(i);
                SerializedProperty matIndex = propMaterialIndexListProperty.GetArrayElementAtIndex(i);
                SerializedProperty matProperties = propPropertyListProperty.GetArrayElementAtIndex(i);

                if (meshProp == null || matProperties == null || useMatOverride == null || matIndex == null)
                    continue;

                MeshRenderer mesh = (MeshRenderer)meshProp.objectReferenceValue;
                ScreenPropertyMap map = (ScreenPropertyMap)matProperties.objectReferenceValue;

                if (!mesh || !map)
                    continue;

                if (useMatOverride.boolValue)
                    mesh.GetPropertyBlock(block, matIndex.intValue);
                else
                    mesh.GetPropertyBlock(block);

                if (map.screenTexture != "" && logoTex)
                    block.SetTexture(map.screenTexture, logoTex);
                if (map.avProCheck != "")
                    block.SetInt(map.avProCheck, 0);
                if (map.applyGamma != "")
                    block.SetInt(map.applyGamma, 0);
                if (map.invertY != "")
                    block.SetInt(map.invertY, 0);
                if (map.screenFit != "" && videoPlayer)
                    block.SetInt(map.screenFit, (int)videoPlayer.defaultScreenFit);

                bool overrideAspectRatio = overrideAspectRatioProperty.boolValue;
                float aspectRatio = aspectRatioProperty.floatValue;
                if (map.aspectRatio != "")
                    block.SetFloat(map.aspectRatio, overrideAspectRatio && logoTex ? aspectRatio : 0);

                if (useMatOverride.boolValue)
                    mesh.SetPropertyBlock(block, matIndex.intValue);
                else
                    mesh.SetPropertyBlock(block);
            }
        }

        private void UpdateEditorCRT()
        {
            CustomRenderTexture crt = (CustomRenderTexture)outputCRTProperty.objectReferenceValue;
            if (crt)
            {
                Material crtMat = crt.material;
                ScreenPropertyMap map = (ScreenPropertyMap)outputMaterialPropertiesProperty.objectReferenceValue;

                UpdateSharedMaterial(crtMat, map);
            }
        }

        private void UpdateSharedMaterial(Material mat, ScreenPropertyMap map)
        {
            Texture2D logoTex = (Texture2D)editorTextureProperty.objectReferenceValue;
            if (logoTex == null)
                logoTex = (Texture2D)logoTextureProperty.objectReferenceValue;

            if (mat && map)
            {
                if (map.screenTexture != "" && logoTex)
                    mat.SetTexture(map.screenTexture, logoTex);
                if (map.avProCheck != "")
                    mat.SetInt(map.avProCheck, 0);
                if (map.applyGamma != "")
                    mat.SetInt(map.applyGamma, 0);
                if (map.invertY != "")
                    mat.SetInt(map.invertY, 0);

                SyncPlayer videoPlayer = (SyncPlayer)videoPlayerProperty.objectReferenceValue;
                if (map.screenFit != "" && videoPlayer)
                    mat.SetInt(map.screenFit, (int)videoPlayer.defaultScreenFit);

                bool overrideAspectRatio = overrideAspectRatioProperty.boolValue;
                float aspectRatio = aspectRatioProperty.floatValue;
                if (map.aspectRatio != "")
                    mat.SetFloat(map.aspectRatio, overrideAspectRatio && logoTex ? aspectRatio : 0);
            }
        }
    }
}