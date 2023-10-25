///=============================================================================
/// name:Geometry
/// author:Renyi
/// email:ren1987yi@163.com
/// license:MIT
/// version:0.0.1-alpha
/// description:
/// 几何学相关算法
///=============================================================================
using System;
using System.Numerics;
namespace GOptixLib.Utils;

public class Geometry{
	/// <summary>
	/// 两个向量的夹角，向量 (pt1,c),向量 (pt2,c)
	/// </summary>
	/// <param name="pt1">点1</param>
	/// <param name="pt2">点2</param>
	/// <param name="c">中心点</param>
	/// <returns></returns> 
	public static float VectorAngle2D(Vector2 pt1,Vector2 pt2,Vector2 c){
		float theta = MathF.Atan2(pt1.Y - c.Y, pt1.X - c.X) - MathF.Atan2(pt2.Y - c.Y, pt2.X - c.X);
		if (theta > MathF.PI)
			theta -= 2 * MathF.PI;
		if (theta < -MathF.PI)
			theta += 2 * MathF.PI;
	
		theta = (float)(theta * 180.0 / MathF.PI);
		return theta;


	}

	/// <summary>
	/// 线性变换 2维点
	/// </summary>
	/// <param name="x1"></param>
	/// <param name="p1"></param>
	/// <param name="x2"></param>
	/// <param name="p2"></param>
	/// <param name="x"></param>
	/// <returns></returns>
	public static Vector2 LinearPoint2D(float x1,Vector2 p1,float x2,Vector2 p2,float x){
		if(x <= x1){
			return p1;
		}else if(x >= x2){
			return p2;
		}else{

			float px = (x-x1)*(p2.X - p1.X)/(x2-x1)  + p1.X;
			float py = (x-x1)*(p2.Y - p1.Y)/(x2-x1)  + p1.Y;

			return new Vector2(px,py);
		}
	}

	/// <summary>
	/// 线性变换
	/// </summary>
	/// <param name="x1"></param>
	/// <param name="y1"></param>
	/// <param name="x2"></param>
	/// <param name="y2"></param>
	/// <param name="x"></param>
	/// <returns></returns>
	public static float Linear2D(float x1,float y1,float x2,float y2,float x){
		if(x <= x1){
			return y1;
		}else if(x >= x2){
			return y2;
		}else{
			return (x-x1) * (y2 - y1)/(x2-x1)  + y1;
		}
	}
}

