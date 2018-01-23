/* WebHook 0.9 - https://github.com/willnode/WebViewHook/ - MIT */

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


public class WebViewHook : ScriptableObject
{

    Object webView;
    EditorWindow host;
    object hostCache;

    static Type _T;
    static FieldInfo _Parent;
    static MethodInfo _Show, _Hide, _Back, _Reload, _Forward;
    static MethodInfo _SetSizeAndPosition;
    static MethodInfo _InitWebView;
    static MethodInfo _SetDelegateObject;
    static MethodInfo _SetHostView;
    static MethodInfo _ExecuteJavascript;
    static MethodInfo _LoadURL;

    static WebViewHook()
    {
        _T = typeof(Editor).Assembly.GetTypes().First(x => x.Name == "WebView");
        _Parent = typeof(EditorWindow).GetField("m_Parent", Instance);
        _Show = (_T.GetMethod("Show", Instance));
        _Hide = (_T.GetMethod("Hide", Instance));
        _Back = (_T.GetMethod("Back", Instance));
        _Reload = (_T.GetMethod("Reload", Instance));
        _Forward = (_T.GetMethod("Forward", Instance));
        _InitWebView = (_T.GetMethod("InitWebView", Instance));
        _SetSizeAndPosition = (_T.GetMethod("SetSizeAndPosition", Instance));
        _SetHostView = (_T.GetMethod("SetHostView", Instance));
        _SetDelegateObject = (_T.GetMethod("SetDelegateObject", Instance));
        _ExecuteJavascript = (_T.GetMethod("ExecuteJavascript", Instance));
        _LoadURL = (_T.GetMethod("LoadURL", Instance));
    }

    public void OnEnable()
    {
        if (!webView)
        {
            webView = CreateInstance(_T);
            webView.hideFlags = HideFlags.DontSave;
        }
    }

    public void OnDisable()
    {
        if (webView)
        Detach();
    }

    public void OnDestroy()
    { 
        DestroyImmediate(webView);
        webView = null;
    }

    public bool Hook(EditorWindow host)
    { 


        if (host == this.host) return false;
        if (!webView)
            OnEnable();
        Invoke(_InitWebView, _Parent.GetValue(hostCache = (this.host = host)), 0, 0, 1, 1, false);
        Invoke(_SetDelegateObject, this);
        return true;
    }

    public void Detach ()
    {
        Invoke(_SetHostView, this.hostCache = null);

    }
    void SetHostView(object host)
    {
        Invoke(_SetHostView, this.hostCache = host);
        Hide();
        Show();
    }

    void SetSizeAndPosition(Rect position)
    {
        Invoke(_SetSizeAndPosition, (int)position.x, (int)position.y, (int)position.width, (int)position.height);
    }

    void OnGUI() { }

    public void OnGUI(Rect r)
    {

        if (host)
        {
            var h = _Parent.GetValue(host);
            if (hostCache != h)
            {
                SetHostView(h); 
            } else
                Invoke(_SetHostView, h);

        }
        SetSizeAndPosition(r);

    }

    public void Forward()
    {
        Invoke(_Forward);
    }

    public void Back()
    {
        Invoke(_Back);
    }

    public void Show()
    {
        Invoke(_Show);
    }

    public void Hide()
    {
        Invoke(_Hide);
    }

    public void Reload()
    {
        Invoke(_Reload);
    }

    public void LoadURL(string url)
    {
        Invoke(_LoadURL, url);
    }

    public void LoadHTML(string html)
    {
        Invoke(_LoadURL, "data:text/html," + html);
    }

    public void LoadFile(string path)
    {
        Invoke(_LoadURL, "file:///" + path);
    }

    public void ExecuteJavascript(string js)
    {
        Invoke(_ExecuteJavascript, js);
    }

    private void OnWebViewDirty()
    {
        host.Repaint();
    } 

    private void OnOpenExternalLink(string url)
    {
        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
        {
            return;
        }
        Application.OpenURL(url);
    }

    void Invoke(MethodInfo m, params object[] args)
    {
        try
        {
            m.Invoke(webView, args);
        }
        catch (Exception)
        {
        }
    }

    public void OnLoadError(string url)
    {
        Debug.LogError(url);
    }

    const BindingFlags Instance = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;


}
