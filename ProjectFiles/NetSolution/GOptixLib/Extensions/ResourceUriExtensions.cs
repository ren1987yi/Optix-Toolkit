using System;
using System.Linq;
using FTOptix.Core;
using FTOptix.HMIProject;
using UAManagedCore;


public static class ResourceUriExtensions{

    /// <summary>
    /// 判断 URI是否是本地资源
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
	public static bool IsLocalFile(this ResourceUri uri){
		var _uri = uri.Uri;
		if(_uri.StartsWith("http://") || _uri.StartsWith("https://")){
            return false;
        }else{
            return true;
        }
	}


	public static string ConvertToURL(this ResourceUri uri){
		string _uri = string.Empty;
		if(uri.IsLocalFile()){

            _uri = uri.Uri.Replace("\\","/");
            _uri = _uri.Replace(" ","%20");
                

        }else{
            _uri = uri.Uri;
        }


		return _uri;
	}

}