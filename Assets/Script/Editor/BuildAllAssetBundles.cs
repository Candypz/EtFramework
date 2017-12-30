using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ET {
    public class BuildAllAssetBundles {
        private static string m_outRootPath = Application.dataPath + "/AssetBundles";

        [MenuItem("Et/BuildAssetBundles/Iphone")]
        static void buildIphone() {
            build(BuildTarget.iOS);
        }

        [MenuItem("Et/BuildAssetBundles/Android")]
        static void buildAndroid() {
            build(BuildTarget.Android);
        }

        [MenuItem("Et/BuildAssetBundles/Win32")]
        static void buildWin32() {
            build(BuildTarget.StandaloneWindows);
        }

        [MenuItem("Et/BuildAssetBundles/Clear")]
        static void clear() {
            if (Directory.Exists(m_outRootPath)) {
                Directory.Delete(m_outRootPath, true);
                AssetDatabase.DeleteAsset(m_outRootPath.Substring(m_outRootPath.IndexOf("Assets") + "Assets".Length));
            }
            AssetDatabase.Refresh();
        }

        private static void build(UnityEditor.BuildTarget path) {
            clear();
            var builds = new List<AssetBundleBuild>();
            builds.AddRange(getBuildList(Application.dataPath + "/Resources/"));
            Directory.CreateDirectory(m_outRootPath);
            BuildPipeline.BuildAssetBundles(m_outRootPath, builds.ToArray(), BuildAssetBundleOptions.None, path);
            AssetDatabase.Refresh();
            Debug.Log("build over");
        }

        private static List<AssetBundleBuild> getBuildList(string dir) {
            List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
            int prefixLen = Application.dataPath.Length - "Assets".Length;
            string[] files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
            foreach (var filename in files) {
                if (filename.EndsWith(".meta"))
                    continue;

                string assetPath = filename.Remove(0, prefixLen);
                string assetName = Utility.removeExtension(assetPath);
                string bundleName = assetName;

                AssetBundleBuild abb = new AssetBundleBuild();
                abb.assetBundleName = bundleName;
                abb.assetNames = new string[] { assetPath };
                builds.Add(abb);
            }

            return builds;
        }
    }
}