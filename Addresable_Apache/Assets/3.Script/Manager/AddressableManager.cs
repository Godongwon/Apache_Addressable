using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AddressableManager : MonoBehaviour
{
    public static bool isDownload { get; private set; }
    [Space]
    [Header("다운로드를 원하는 번들 또는 번들들에 포함된 레이블중 아무거나 입력해주세요.")]
    [SerializeField] string bunblekey;
    private AsyncOperationHandle<GameObject> Hanble;

    [SerializeField] private GameObject player = null;
    //public Text log;
    public void Click_down()
    {
        if (!AddressableManager.isDownload)
            StartCoroutine(BundleDown());
    }

    IEnumerator BundleDown()
    {
        //Hanble = Addressables.DownloadDependenciesAsync(bunblekey);
        //Hanble.Completed +=
        //(AsyncOperationHandle Handle) =>
        //{
        //    //Addressables.Release(Handle);
        //    Debug.Log("Complete Asset Downloaded");
        //    //log.text += "\nComplete Asset Downloaded";
        //    AddressableManager.isDownload = true;

        //};
        Hanble = Addressables.InstantiateAsync(bunblekey, Vector3.zero, Quaternion.identity);
        Hanble.Completed +=(AsyncOperationHandle<GameObject> Handle) =>
          {
              Debug.Log("Complete Asset Downloaded");
              player = Handle.Result;
              AddressableManager.isDownload = true;
          };



        while (!Hanble.IsDone)
        {
            OnClick_CheckSize();
            Debug.Log("Asset Downloading");
            //log.text += "\nAsset Downloading";
            yield return null;
        }
    }
    public void BundleDelete()
    {
        Addressables.ClearDependencyCacheAsync(bunblekey);
    }
    public void OnClick_CheckSize()
    {
        Addressables.GetDownloadSizeAsync(bunblekey).Completed +=
        (AsyncOperationHandle<long> SizeHandle) =>
        {
            string sizeText = string.Concat(SizeHandle.Result, " byte");
            Debug.Log(sizeText);
            //log.text += "\n"+ sizeText;
            Addressables.Release(SizeHandle);
        };
    }
    public void Release_Asset()
    {
        Addressables.Release(Hanble);
    }
}
