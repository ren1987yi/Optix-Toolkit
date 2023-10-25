///=============================================================================
/// name:Encode
/// author:Renyi
/// email:ren1987yi@163.com
/// license:MIT
/// version:0.0.1-alpha
/// description:
/// 编码，解码相关
/// 	
///=============================================================================


using System;
using System.Numerics;
namespace GOptixLib.Utils;

public class Encode{


	#region Base64加码解码
	/// <summary>
	/// Base64编码，采用utf8编码
	/// </summary>
	/// <param name="strPath">待编码的明文</param>
	/// <returns>Base64编码后的字符串</returns>
	public static  string Base64Encrypt(string strPath)
	{
		string returnData;
		System.Text.Encoding encode = System.Text.Encoding.UTF8;
		byte[] bytedata = encode.GetBytes(strPath);
		try
		{
			returnData = Convert.ToBase64String(bytedata, 0, bytedata.Length);
		}
		catch
		{
			returnData = strPath;
		}
		return returnData;
	}

	/// <summary>
	/// Base64解码，采用utf8编码方式解码
	/// </summary>
	/// <param name="strPath">待解码的密文</param>
	/// <returns>Base64解码的明文字符串</returns>
	public static string Base64DesEncrypt(string strPath)
	{
		string returnData;
		byte[] bpath = Convert.FromBase64String(strPath);
		try
		{
			returnData =  System.Text.Encoding.UTF8.GetString(bpath);
		}
		catch
		{
			returnData = strPath;
		}
		return returnData;
	}
	#endregion
}

