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
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;
namespace GOptixLib.Utils;

public class Encode
{


	#region Base64加码解码
	/// <summary>
	/// Base64编码，采用utf8编码
	/// </summary>
	/// <param name="strPath">待编码的明文</param>
	/// <returns>Base64编码后的字符串</returns>
	public static string Base64Encrypt(string strPath)
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
			returnData = System.Text.Encoding.UTF8.GetString(bpath);
		}
		catch
		{
			returnData = strPath;
		}
		return returnData;
	}
	#endregion


	#region  SHA256
	/// <summary>
	/// SHA256加密
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	public static string SHA256EncryptString(string data)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(data);
		byte[] hash = SHA256.Create().ComputeHash(bytes);

		StringBuilder builder = new StringBuilder();
		for (int i = 0; i < hash.Length; i++)
		{
			builder.Append(hash[i].ToString("x2"));
		}
		return builder.ToString();
	}

	/// <summary>
	/// SHA256加密
	/// </summary>
	/// <param name="StrIn">待加密字符串</param>
	/// <returns>加密数组</returns>
	public static Byte[] SHA256EncryptByte(string StrIn)
	{
		var sha256 = SHA256.Create();
		var Asc = new ASCIIEncoding();
		var tmpByte = Asc.GetBytes(StrIn);
		var EncryptBytes = sha256.ComputeHash(tmpByte);
		sha256.Clear();
		return EncryptBytes;
	}

	#endregion
}

