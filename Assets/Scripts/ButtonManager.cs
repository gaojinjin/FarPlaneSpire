using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using FSEvent;
using FSFile;

public class ButtonManager : MonoBehaviour {
    public Text title;

	// Use this for initialization
	void Start () {
        //EventListener.Instance.RegisterEvent(EventEnum.EVENT_SHOW_DEMO, actionDemo);

        StartCoroutine("Test");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerable Test() {
        Debug.Log("运行 1 。。。 ");
        yield return 0;
        Debug.Log("运行 2 。。。 ");
    }

    public void buttonClicked() {
        print("点击");

        Dictionary<string, object> info = new Dictionary<string, object>();
        info.Add("test", 100);

        //EventListener.Instance.PostEvent(EventEnum.EVENT_SHOW_DEMO, info);

        string value = FileManager.Instance.ReadResourceText("Jsons/kof97");
        string url = Application.persistentDataPath + "/Savedata/kof97.json";

        print("项目路径 .. " + url);
        //print("内容 .. " + FileManager.Instance.ReadTextFile(url));

        FileManager.Instance.SaveText(Application.persistentDataPath + "/Save", "kof97.json", value);

        //FileManager.Instance.SaveTextToFile(url, value);

        ArrayList names = FileManager.Instance.CheckFilesInDirector(Application.persistentDataPath, "json");
        foreach (string fileName in names) {
            print("文件名 .. " + fileName);
        }

        FileManager.Instance.RemoveDirector(Application.persistentDataPath + "/Savedata");
    }

    private void actionTest() {
        print("测试方法运行...");
    }

    private void actionDemo(Dictionary<string, object> info) {
        object value = null;
        info.TryGetValue("test", out value);

        print("输出 .. " + (int)value);

        title.text = value.ToString();
    }
}
