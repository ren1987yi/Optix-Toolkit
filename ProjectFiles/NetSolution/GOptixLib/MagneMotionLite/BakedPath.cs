///=============================================================================
/// name:BakedPath
/// author:Renyi
/// email:ren1987yi@163.com
/// license:MIT
/// version:0.0.1-alpha
/// description:
/// 烘培后的MML 的路径，已经计算后 所有路径长度对应的 X,Y,RotationAngle，间隔 1mm
///=============================================================================

using System.Linq;
using System.Collections.Generic;
using System.Numerics;
using GOptixLib.Utils;

namespace GOptixLib.MagneMotionLite;


    public class BakedPath{
     
       
        public uint ID{get;set;}

        private float _length;
        public float Length
        {
            get { return _length; }
            set { _length = value; }
        }
        
		
		/// <summary>
		/// 烘培后的位置
		/// </summary>
		/// <typeparam name="float">路径长度</typeparam>
		/// <typeparam name="Location2D">位置</typeparam>
		/// <returns></returns>
		Dictionary<float,OptixUILocation> dcBakedLocations = new Dictionary<float, OptixUILocation>();
		/// <summary>
		/// 烘培后所有的路径长度，已经排序
		/// </summary>
		float[] BackedLengths;

		public BakedPath(Dictionary<float,System.Numerics.Vector2> bakedPoints){
			BuildLocations(bakedPoints);
		}



		private void BuildLocations(Dictionary<float,System.Numerics.Vector2> bakedPoints ){
			var lengths = bakedPoints.Keys.ToList();
			lengths.Sort();
			dcBakedLocations.Clear();
			BackedLengths = lengths.ToArray();

			foreach(var length in lengths){
				var loc = new OptixUILocation(){
					Position = new Vector2(bakedPoints[length].X,bakedPoints[length].Y),
					RotationAngle = 0.0f
				};
				dcBakedLocations.Add(length,loc);
			}

			if(BackedLengths.Length <= 1){

			}else if(BackedLengths.Length >= 2){
				Vector2 p1,p2;
				
				//算角度

				//第一点 p[0]-> p[1] 的角度
				var l1 = BackedLengths[0];
				var l2 = BackedLengths[1];
				p1 = dcBakedLocations[l1].Position;
				p2 = dcBakedLocations[l2].Position;
				dcBakedLocations[l1].RotationAngle = CalcVector2Angle(p1,p2);

				//中间  n = p[n-1] -> p[n+1] 的角度
				for(var i = 1;i<BackedLengths.Length-1 ;i++){
					var l = BackedLengths[i];
					l1 = BackedLengths[i-1];
					l2 = BackedLengths[i+1];
					p1 = dcBakedLocations[l1].Position;
					p2 = dcBakedLocations[l2].Position;

					dcBakedLocations[l].RotationAngle = CalcVector2Angle(p1,p2);
					
				}


				//最后一点 n = n-1 的角度
				l1 = BackedLengths[BackedLengths.Length - 1];
				l2 = BackedLengths.Last();
				p1 = dcBakedLocations[l1].Position;
				p2 = dcBakedLocations[l2].Position;
				dcBakedLocations[l2].RotationAngle = CalcVector2Angle(p1,p2);



			}
		}

		/// <summary>
		/// 两个向量的夹角 单位:度
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
		private float CalcVector2Angle(Vector2 p1,Vector2 p2){
        	Vector2 v2 = System.Numerics.Vector2.UnitX;
			var v1 = p2 - p1;
			var angle = Geometry.VectorAngle2D(v1,v2,System.Numerics.Vector2.Zero);
			return angle;
		}


		/// <summary>
		/// get location with length
		/// 在烘培中，在所有以烘培的长度里找到小于参数里最大的
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public OptixUILocation GetLocation(float length){
			var l = BackedLengths.Where(l=> l <= length).Max();
			return dcBakedLocations[l];
		}
		

        

    }
