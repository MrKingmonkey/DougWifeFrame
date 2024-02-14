using System;
using System.IO;
using System.Reflection;
using BepInEx;
using DougWifeFrame;
using Photon.Voice;
using Unity.Mathematics;
using UnityEngine;
using Utilla;

namespace DougWifeFrame
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {


        public bool active;
        bool inRoom;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            AssetObj.SetActive(true);

            active = true;

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            AssetObj.SetActive(false);

            active = false;

            HarmonyPatches.RemoveHarmonyPatches();
        }
        public GameObject AssetObj;
        void OnGameInitialized(object sender, EventArgs e)
        {
            var assetBundle = LoadAssetBundle("DougWifeFrame.greeny");
            GameObject Obj = assetBundle.LoadAsset<GameObject>("GreenObj");

            AssetObj = Instantiate(Obj);
            AssetObj.transform.position = new Vector3(-65.5974f, 12.331f, -84.9168f);
            AssetObj.transform.rotation = Quaternion.Euler(270f, 64.5012f, 0f);
            AssetObj.transform.localScale = new Vector3(30.5288f, 39.1639f, 39.0486f);
            AssetObj.layer = 8;
        }

        AssetBundle LoadAssetBundle(string path)
        {
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
                AssetBundle bundle = AssetBundle.LoadFromStream(stream);
                stream.Close();
                Debug.Log("[" + PluginInfo.GUID + "] Success loading asset bundle");
                return bundle;
            }
            catch (Exception e)
            {
                Debug.Log("[" + PluginInfo.Name + "] Error loading asset bundle: " + e.Message + " " + path);
                throw;
            }
        }
    }
}