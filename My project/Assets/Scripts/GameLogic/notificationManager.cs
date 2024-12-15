using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class notificationManager : MonoBehaviour
{
    public readonly string newRecordNotif = "notifNewRecordBg";
    public readonly string nearRecordNotif = "notifNearRecordBg";
    private const float createDestroyTime = 1f;
    private Transform CanvasUI;
    public GameObject newRecordNotifObj;

    private static notificationManager _instance;
    public static notificationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("notificationManager").AddComponent<notificationManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CanvasUI = GameObject.Find("CanvasUI").transform;
    }

    public async void NewRecordNotifLifetime()
    {
        /*
        Task task1 = Task.Run(() =>
        {
            if (newRecordNotifObj == null)
                newRecordNotifObj = CreateNotification(newRecordNotif);
        });

        if (newRecordNotifObj != null)
            await task1.ContinueWith(t => DestroyNotification(newRecordNotifObj));
        */
        newRecordNotifObj = CreateNotification(newRecordNotif);

        while (newRecordNotifObj.GetComponent<CanvasGroup>().alpha != 1)
        {
            await Task.Yield();
        }
        if (newRecordNotifObj != null)
            DestroyNotification(newRecordNotifObj);
    }

    public GameObject CreateNotification(string name)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + name);

        GameObject obj = Instantiate(prefab, CanvasUI);
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        StartCoroutine(createDestroyAnim(canvasGroup, true));

        return obj;
    }
    public void DestroyNotification(GameObject GO)
    {
        CanvasGroup canvasGroup = GO.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;

        StartCoroutine(createDestroyAnim(canvasGroup, false));
    }
    private IEnumerator createDestroyAnim(CanvasGroup cg, bool create)
    {
        int iterations = 100;

        for (int i = 0; i < iterations; i++)
        {
            yield return new WaitForSeconds(createDestroyTime / iterations);
            if (create)
                cg.alpha += 1f / iterations;
            else
                cg.alpha -= 1f / iterations;
        }

        if (create)
            cg.alpha = 1;
        else
            cg.alpha = 0;

        if (!create)
            Destroy(cg.gameObject);
    }
}
