# WebViewHook

This is a code snippet to access a hidden `WebView` API from Unity Editor.

![Screenshot](Screenshots/Demo.png)

## Code

Download [WebViewHook](Assets/Editor/WebViewHook.cs). See usage example in [WebWindow](Assets/Editor/WebWindow.cs) or [WebEditor](Assets/Editor/WebEditor.cs). You may download the whole project if necessary.

## Usage

+ Browse the Internet inside Unity
+ Open local documentation (Plain Text, HTML ~~and PDF~~*)
+ Built-in code editor (Use your JavaScript imagination)

*\*) PDF Plugin indeed exist but disabled due to `--ignore-plugins` as seen from `chrome://version`.

## Caveat

`WebView` is hidden for a good reason. The editor might **crash** if done improperly so follow the examples and don't experiment with project unsaved.

The technology behind `WebView` is [Chrome Embedded Framework](https://en.wikipedia.org/wiki/Chromium_Embedded_Framework), [version 37](https://twitter.com/willnode/status/955079655630913541). In Windows it's contained inside the gigantic `libcef.dll`.

Remember `WebView` is not Chrome. Some features are modified (e.g. default backcolor is darkgrey). I also don't know how the browser deal with downloads and cookies.

I solving bugs as I can, but of course limited. If you can't open a specific website, mostly comes down to the fact that Unity haven't upgrade their CEF browser.

For safety reason, the WebViewHook uses `AssemblyReloadEvents` which only available in Unity version 2017.1 or higher.