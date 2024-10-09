using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WEngine
{
    public static class VectorExtension  {

        /// <summary>毫米精度</summary>
        private const float EpsilonLower = 1e-3f;

        /// <summary>float是否相等（毫米精度）</summary>
        public static bool EqualsWithEpsilonLower(this float a, float b)
        {
            return Math.Abs(a - b) < EpsilonLower;
        }

        /// <summary>float绝对值</summary>
        public static float Abs(this float value)
        {
            return Mathf.Abs(value);
        }

        /// <summary>int绝对值</summary>
        public static int Abs(this int value)
        {
            return Mathf.Abs(value);
        }

        public static string SerializeArray(Vector3[] aVectors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Vector3 v in aVectors)
            {
                sb.Append(v.x).Append(" ").Append(v.y).Append(" ").Append(v.z).Append("|");
            }
            if (sb.Length > 0) // remove last "|"
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string SerializeArray(Vector2[] aVectors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Vector3 v in aVectors)
            {
                sb.Append(v.x).Append(" ").Append(v.y).Append("|");
            }
            if (sb.Length > 0) // remove last "|"
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string SerializeArray(int[] aVectors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int v in aVectors)
            {
                sb.Append(v).Append("|");
            }
            if (sb.Length > 0) // remove last "|"
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        #region Vector2
        /// <summary>Vector2是否相等，毫米精度</summary>
        public static bool EqualsVector2(this Vector2 a, Vector2 b)
        {
            return Math.Abs(a.x - b.x) < EpsilonLower && Math.Abs(a.y - b.y) < EpsilonLower;
        }

        /// <summary>Vector2绝对值</summary>
        public static Vector2 Abs(this Vector2 vec)
        {
            return new Vector2(Mathf.Abs(vec.x), Mathf.Abs(vec.y));
        }

        public static float DistanceToPoint(this Vector2 v, Vector2 targetV)
        {
            return Vector3.Distance(v, targetV);
        }

        /// <summary>比较是否相等</summary>
        public static int CompareTo(this Vector2 v1, Vector2 v2)
        {
            if (v1.EqualsVector2(v2))
                return 0;
            else
                return -1;
        }

        /// <summary>Vector2通过点积判断两个向量是否平行</summary>
        public static bool IsParallelByDot(this Vector2 dir1, Vector2 dir2)
        {
            float dotValue = Vector3.Dot(dir1.normalized, dir2.normalized);
            if ((Mathf.Abs(dotValue) - 1).EqualsWithEpsilonLower(0))//点积结果绝对值趋近于0表示平行
            {
                return true;
            }
            return false;
        }

        /// <summary>Vector2通过叉积判断两个向量是否垂直</summary>
        public static bool IsVerticalByDot(this Vector2 dir1, Vector2 dir2)
        {
            float value = Vector3.Dot(dir1.normalized, dir1.normalized).Abs();
            if (Mathf.Abs(value).EqualsWithEpsilonLower(0))
                return true;
            return false;
        }

        /// <summary>Vector2变为Vector3，增加y轴</summary>
        public static Vector3 ToVector3(this Vector2 v, float y = 0)
        {
            return new Vector3(v.x, y, v.y);
        }

        /// <summary>Vector2列表变为Vector3列表，增加y轴</summary>
        public static List<Vector3> ToVector3(this IEnumerable<Vector2> list, float y = 0)
        {
            List<Vector3> v = new List<Vector3>();
            foreach (var item in list)
                v.Add(item.ToVector3(y));
            return v;
        }

        /// <summary>Vector2列表变为Vector3数组，增加y轴</summary>
        public static Vector3[] ToVector3(this List<Vector2> list, float y = 0)
        {
            Vector3[] v = new Vector3[list.Count];
            for (int i = 0; i < list.Count; i++)
                v[i] = list[i].ToVector3(y);
            return v;
        }

        /// <summary>Vector2数组变为Vector3数组，增加y轴</summary>
        public static Vector3[] ToVector3(this Vector2[] list, float y = 0)
        {
            Vector3[] v = new Vector3[list.Length];
            for (int i = 0; i < list.Length; i++)
                v[i] = list[i].ToVector3(y);
            return v;
        }

        /// <summary>克隆vector3</summary>
        public static Vector2 Clone(this Vector2 v)
        {
            return new Vector2(v.x, v.y);
        }
        #endregion

        #region Vector3
        /// <summary>Vector3是否相等，毫米精度</summary>
        public static bool EqualsVector3(this Vector3 a, Vector3 b, float epsilon = EpsilonLower)
        {
            return Math.Abs(a.x - b.x) <= epsilon && Math.Abs(a.y - b.y) <= epsilon && Math.Abs(a.z - b.z) <= epsilon;
        }

        /// <summary>Vector3绝对值</summary>
        public static Vector3 Abs(this Vector3 vec)
        {
            return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
        }

        public static float DistanceToPoint(this Vector3 v, Vector3 targetV)
        {
            return Vector3.Distance(v, targetV);
        }

        /// <summary>比较是否相等</summary>
        public static int CompareTo(this Vector3 v1, Vector3 v2)
        {
            if (v1.EqualsVector3(v2))
                return 0;
            else
                return -1;
        }

        /// <summary>Vector3通过点积判断两个向量是否平行</summary>
        public static bool IsParallelByDot(this Vector3 dir1, Vector3 dir2)
        {
            float dotValue = Vector3.Dot(dir1.normalized, dir2.normalized);
            if ((Mathf.Abs(dotValue) - 1).EqualsWithEpsilonLower(0))//点积结果绝对值趋近于0表示平行
            {
                return true;
            }
            return false;
        }

        /// <summary>Vector3通过叉积判断两个向量是否垂直</summary>
        public static bool IsVertical(this Vector3 dir1, Vector3 dir2)
        {
            float value = Vector3.Dot(dir1.normalized, dir1.normalized).Abs();
            if (Mathf.Abs(value).EqualsWithEpsilonLower(0))
                return true;
            return false;
        }

        /// <summary>Vector3变为Vector2，丢弃y轴</summary>
        public static Vector2 ToVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }

        /// <summary>Vector3列表变为Vector2列表，丢弃y轴</summary>
        public static List<Vector2> ToVector2(this IEnumerable<Vector3> list)
        {
            List<Vector2> v = new List<Vector2>();
            foreach (var item in list)
                v.Add(item.ToVector2());
            return v;
        }

        /// <summary>Vector3列表变为Vector2数组，丢弃y轴</summary>
        public static Vector2[] ToVector2(this List<Vector3> list)
        {
            Vector2[] v = new Vector2[list.Count];
            for (int i = 0; i < list.Count; i++)
                v[i] = list[i].ToVector2();
            return v;
        }

        /// <summary>Vector3数组变为Vector2数组，丢弃y轴</summary>
        public static Vector2[] ToVector2(this Vector3[] list)
        {
            Vector2[] v = new Vector2[list.Length];
            for (int i = 0; i < list.Length; i++)
                v[i] = list[i].ToVector2();
            return v;
        }

        /// <summary>设置vector3数组的y值</summary>
        public static Vector3[] SetVertor3(this Vector3[] list, float y = 0)
        {
            Vector3[] v = new Vector3[list.Length];
            for (int i = 0; i < list.Length; i++)
                v[i] = new Vector3(list[i].x, y, list[i].z);
            return v;
        }

        /// <summary>设置vector3列表的y值</summary>
        public static Vector3[] SetVertor3(this List<Vector3> list, float y = 0)
        {
            Vector3[] v = new Vector3[list.Count];
            for (int i = 0; i < list.Count; i++)
                v[i] = new Vector3(list[i].x, y, list[i].z);
            return v;
        }

        /// <summary>克隆vector3</summary>
        public static Vector3 Clone(this Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
        #endregion
    }
}
