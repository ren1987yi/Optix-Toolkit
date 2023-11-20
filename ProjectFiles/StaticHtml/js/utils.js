function getQueryString(url_string, name) {
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

function isNull(str){
if(str == "") return true;
if(str == null) return true;
var regu = "^[ ]+$";
var re = new RegExp(regu);
return re.test(str);
}

//HTML标签转义（  < -----> &lt;）

function html2Escape(sHtml) {

	return sHtml.replace(/[<>&"]/g,function(c){
   
	  return {'<':'&lt;','>':'&gt;','&':'&amp;','"':'&quot;'}[c];
   
	});
   
   }

   //HTML标签反转义（  &lt; ----> < ）

function escape2Html(str) {

	var arrEntities={'lt':'<','gt':'>','nbsp':' ','amp':'&','quot':'"'};
  
	return str.replace(/&(lt|gt|nbsp|amp|quot);/ig,function(all,t){
  
	  return arrEntities[t];
  
	});
  
  }
  