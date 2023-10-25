function getQueryString(url_string,name) {
	//const url_string = "https://www.baidu.com/t.html?name=mick&age=20"; // window.location.href
	const url = new URL(url_string);
	return url.searchParams.get(name);
}


function looseJsonParse(obj) {
	return Function('"use strict";return (' + obj + ")")();
  }


  function b64_to_utf8(str) {
	return decodeURIComponent(escape(window.atob(str)));
}
