var callback = function () {
  /**
     * 
     * <link rel="preconnect" href="https://fonts.gstatic.com">
<link href="https://fonts.googleapis.com/css2?family=Open+Sans&display=swap" rel="stylesheet">
     */
  var link = document.createElement("link");
  link.rel = "stylesheet";
  link.href = "https://fonts.googleapis.com/css2?family=Open+Sans&display=swap";
  document.head.appendChild(link);

  //var x = document.getElementsByTagName("img");
  //console.log()
  // x.item  = "asdf"
  // var elem = document.createElement("div");
  // elem.innerHTML =
  //     "<div style=\"text-align: center; font-family: Titillium Web,sans-serif; margin: 16px;\">This text was injected via /ext/custom-javascript.js, using the SwaggerUIOptions.InjectJavascript method.</div>";

  //document.body.insertBefore(elem, document.body.firstChild);

  document.title = "Yolted - get our games Fast!";
};

if (
  document.readyState === "complete" ||
  (document.readyState !== "loading" && !document.documentElement.doScroll)
) {
  callback();
} else {
  document.addEventListener("DOMContentLoaded", callback);
}
