# WebViewHook

This is a code snippet to access a hidden `WebView` API from Unity Editor.

## Code

Download [WebViewHook](Assets/Editor/WebViewHook.cs). See usage example in [WebWindow](Assets/Editor/WebWindow.cs). You also can download the whole project too.

## Usage

+ Browse the internet inside Unity
+ Open local documentation (Plain Text, HTML and PDF*)
+ Built-in code editor (Use your JavaScript imagination)

*\*) Need Info: PDF in my observation can't be opened, but then why `pdf.dll` exist?*

## Caveat

`WebView` is hidden for a good reason. The editor might **crash** if done improperly so never conduct an experiment with your work inside.

`WebView` is used by Unity for *Asset Store* and *Welcome Launcher*, so it's not designed for browsing the web, even it is possible.

The technology behind `WebView` is [Chrome Embedded Framework](https://en.wikipedia.org/wiki/Chromium_Embedded_Framework), [version 37](https://twitter.com/willnode/status/955079655630913541). In Windows it's contained inside the gigantic `libcef.dll`.

Remember `WebView` is not Chrome. Some features are modified (e.g. default backcolor is darkgrey). I also don't know how it deals with downloads and cookies.

I solving bugs as I can, but of course limited. If you can't open a specific website, mostly comes down to the fact that Unity haven't upgrade their CEF browser.

Do never enter passwords or sensitive data inside `WebView`.