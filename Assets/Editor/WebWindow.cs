using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEditor;
using UnityEngine;

class WebWindow : EditorWindow
{

    WebViewHook webView;
    string url = "https://google.com";

    [MenuItem("Tools/Web Window %#w")]
    static void Load()
    {
        WebWindow window = GetWindow<WebWindow>();
        window.Show();
    }

    void OnEnable()
    {
        if (!webView)
        {
            // create webView
            webView = CreateInstance<WebViewHook>();
        }
    }

    public void OnBecameInvisible()
    {
        if (webView)
        {
            // signal the browser to unhook
            webView.Detach();
        }
    }

    void OnDestroy()
    {
        //Destroy web view
        DestroyImmediate(webView);
    }

    void OnGUI()
    {
        // hook to this window
        if (webView.Hook(this))
            // do the first thing to do
            webView.LoadURL(url);

        Rect webViewRect = new Rect(0, 60, position.width, position.height - (40));

        // Navigation
        if (GUI.Button(new Rect(0, 0, 25, 20), "←"))
            webView.Back();
        if (GUI.Button(new Rect(25, 0, 25, 20), "→"))
            webView.Forward();

        // URL text field
        GUI.SetNextControlName("urlfield");
        url = GUI.TextField(new Rect(50, 0, position.width - 30, 20), url);

        // Focus on web view if return is pressed in URL field
        if (Event.current.isKey && Event.current.keyCode == KeyCode.Return && GUI.GetNameOfFocusedControl().Equals("urlfield"))
        {
            webView.LoadURL(url);
        }

        if (Event.current.type == EventType.Repaint)
        {
            // keep the browser aware with resize
            webView.OnGUI(webViewRect);
        }
    }
}
