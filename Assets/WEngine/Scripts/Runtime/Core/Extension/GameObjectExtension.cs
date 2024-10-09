using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WEngine
{
    public static class GameObjectExtension
    {
        /// <summary>获取到当前GameObject的节点路径</summary>
        public static string GetPath(this GameObject gameObject)
        {
            string path = "/" + gameObject.name;
            while (gameObject.transform.parent != null)
            {
                gameObject = gameObject.transform.parent.gameObject;
                path = "/" + gameObject.name + path;
            }

            return path;
        }

        /// <summary>获取到根节点</summary>
        public static GameObject GetRoot(this GameObject gameObject)
        {
            GameObject current = gameObject;
            GameObject root;
            do
            {
                Transform transf = current.transform.parent;
                if (transf != null)
                {
                    root = transf.gameObject;
                    current = transf.gameObject;
                }
                else//跳出循环
                {
                    root = current;
                    current = null;
                }
            } while (current != null);

            return root;
        }

        /// <summary>获取当前节点的深度</summary>
        public static int GetDepth(this GameObject gameObject)
        {
            int depth = 0;
            Transform current = gameObject.transform;
            do
            {
                current = current.transform.parent;
                if (current != null)//累加
                {
                    depth++;
                }
            } while (current != null);

            return depth;
        }

        /// <summary>查找本游戏物体下的特定名称的子物体，并将其返回（支持“/”）</summary>
        public static Transform FindDeepChild(this GameObject gameObject, string childName)
        {
            var resultTran = gameObject.transform.Find(childName);
            if (resultTran == null)
            {
                foreach (Transform transf in gameObject.transform)
                {
                    resultTran = transf.gameObject.FindDeepChild(childName);
                    if (resultTran != null)
                    {
                        return resultTran;
                    }
                }
            }

            return resultTran;
        }

        /// <summary>是否为空或者没有激活</summary>
        public static bool IsNullOrInactive(this GameObject go)
        {
            return go == null || !go.activeSelf;
        }

        /// <summary>是否激活</summary>
        public static bool IsActive(this GameObject go)
        {
            return go != null && go.activeSelf;
        }

        /// <summary>激活自己并激活父物体</summary>
        public static void ActiveAndParent(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            if (gameObject.transform.parent != null)//递归激活
            {
                gameObject.transform.parent.gameObject.ActiveAndParent();
            }
            gameObject.SetActive(true);
        }

        #region Layer
        public static void SetLayer(this GameObject gameObject, int layer)
        {
            Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(true);
            foreach (var trans in transforms)
            {
                trans.gameObject.layer = layer;
            }
        }

        public static void SetLayerByName(this GameObject gameObject, string layerName)
        {
            int layer = LayerMask.NameToLayer(layerName);
            gameObject.SetLayer(layer);
        }
        #endregion

        #region Tag
        /// <summary>设置Tag</summary>
        public static void SetTag(this GameObject gameObject, string tag)
        {
            if (gameObject != null)
                gameObject.tag = tag;
        }

        /// <summary>递归设置所有子物体的Tag</summary>
        public static void SetTagRecursion(this GameObject gameObject, string tag)
        {
            gameObject.tag = tag;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetTagRecursion(tag);
            }
        }

        #endregion

        #region Component

        public static string GetPath(this Transform obj, Transform root)
        {
            string path = "/" + obj.name;
            while (obj.transform.parent != root)
            {
                obj = obj.transform.parent;
                path = "/" + obj.name + path;
            }
            if (path.StartsWith("/"))
                path = path.Remove(0, 1);
            return path;
        }

        public static T FindChild<T>(this GameObject go, string path) where T : Component
        {
            if (go == null)
            {
                Debug.Log("对象为空");
                return null;
            }

            Transform child = go.transform.Find(path);
            if (child == null)
            {
                Debug.Log("找不到子物体：" + path);
                return null;
            }

            return child.GetComponent<T>();
        }

        public static void SetText(this GameObject go, string path, string content)
        {
            Text text = go.FindChild<Text>(path);
            if (text == null)
            {
                Debug.Log("找不到子物体：" + path);
                return;
            }

            text.text = content;
        }

        /// <summary>获取组件，不存在则添加，省去一次if判断</summary>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject != null)
            {
                var com = gameObject.GetComponent<T>();
                if (com == null)
                    com = gameObject.AddComponent<T>();

                return com;
            }

            return null;
        }

        /// <summary>添加单个构件，如果存在那么就删除掉</summary>
        public static T AddComponentSingle<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.GetComponent<T>() != null)
            {
                Object.Destroy(gameObject.GetComponent<T>());
            }

            return gameObject.GetComponent<T>();
        }

        /// <summary>搜索指定名字和指定类型的组件(子物体)</summary>
        public static T SearchComponentInChildren<T>(this GameObject gameObject, string searchName) where T : Component
        {
            T[] componets = gameObject.GetComponentsInChildren<T>();
            if (componets != null)
            {
                for (int i = 0; i < componets.Length; i++)
                {
                    if (searchName.Equals(componets[i].name))
                    {
                        return componets[i];
                    }
                }
            }

            return null;
        }

        /// <summary>搜索指定名字和指定类型的组件（父物体）</summary>
        public static T SearchComponentInParents<T>(this GameObject gameObject, string searchName) where T : Component
        {
            T[] componets = gameObject.GetComponentsInParent<T>();
            if (componets != null)
            {
                for (int i = 0; i < componets.Length; i++)
                {
                    if (searchName.Equals(componets[i].name))
                    {
                        return componets[i];
                    }
                }
            }

            return null;
        }

        /// <summary>寻找父节点中的组件</summary>
        public static T FindComponentInParents<T>(this Transform transform) where T : Component
        {
            var component = transform.GetComponent<T>();
            if (component != null)
            {
                return component;
            }

            return transform.parent != null ? transform.parent.FindComponentInParents<T>() : null;
        }

        /// <summary>创建指定路径的子节点，返回这个路径的所有GameObject，第一个节点是最高层级，最后一个节点是最小层级GameObject</summary>
        public static List<GameObject> CreateChild(this GameObject gameObject, string pathName)
        {
            if (string.IsNullOrEmpty(pathName))
            {
                return null;
            }
            GameObject go = null;
            List<GameObject> goList = new List<GameObject>();
            string[] names = pathName.Split('/');
            for (int i = 0; i < names.Length; i++)
            {
                GameObject item = new GameObject(names[i]);
                goList.Add(item);
                if (go != null)
                {
                    item.SetParent(go.transform);
                }

                go = item;
            }
            goList[0].SetParent(gameObject);
            return goList;
        }

        /// <summary>是否包含构件</summary>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }
        #endregion

        #region Transtrom
        public static void SetPosX(this GameObject gameObject, float x)
        {
            Vector3 pos = gameObject.transform.position;
            gameObject.transform.position = new Vector3(x, pos.y, pos.z);
        }

        public static void SetPosY(this GameObject gameObject, float y)
        {
            Vector3 pos = gameObject.transform.position;
            gameObject.transform.position = new Vector3(pos.x, y, pos.z);
        }

        public static void SetPosZ(this GameObject gameObject, float z)
        {
            Vector3 pos = gameObject.transform.position;
            gameObject.transform.position = new Vector3(pos.x, pos.y, z);
        }

        public static void SetPos(this GameObject gameObject, Vector3 pos)
        {
            gameObject.transform.position = pos;
        }

        public static void SetPos(this GameObject gameObject, float x, float y, float z)
        {
            gameObject.transform.position = new Vector3(x, y, z);
        }

        public static void SetLocalPosX(this GameObject gameObject, float x)
        {
            Vector3 pos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(x, pos.y, pos.z);
        }

        public static void SetLocalPosY(this GameObject gameObject, float y)
        {
            Vector3 pos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(pos.x, y, pos.z);
        }

        public static void SetLocalPosZ(this GameObject gameObject, float z)
        {
            Vector3 pos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(pos.x, pos.y, z);
        }

        public static void SetLocalPos(this GameObject gameObject, Vector3 pos)
        {
            gameObject.transform.localPosition = pos;
        }

        public static void SetLocalPos(this GameObject gameObject, float x, float y, float z)
        {
            gameObject.transform.localPosition = new Vector3(x, y, z);
        }

        public static void SetScaleX(this GameObject gameObject, float x)
        {
            Vector3 scale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(x, scale.y, scale.z);
        }

        public static void SetScaleY(this GameObject gameObject, float y)
        {
            Vector3 scale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(scale.x, y, scale.z);
        }

        public static void SetScaleZ(this GameObject gameObject, float z)
        {
            Vector3 scale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(scale.x, scale.y, z);
        }

        public static void SetScale(this GameObject gameObject, float x, float y, float z)
        {
            gameObject.transform.localScale = new Vector3(x, y, z);
        }

        public static void SetScale(this GameObject gameObject, Vector3 scale)
        {
            gameObject.transform.localScale = scale;
        }

        public static void SetLocalAngleX(this GameObject gameObject, float x)
        {
            Vector3 angle = gameObject.transform.localEulerAngles;
            gameObject.transform.localEulerAngles = new Vector3(x, angle.y, angle.z);
        }

        public static void SetLocalAngleY(this GameObject gameObject, float y)
        {
            Vector3 angle = gameObject.transform.localEulerAngles;
            gameObject.transform.localEulerAngles = new Vector3(angle.x, y, angle.z);
        }

        public static void SetLocalAngleZ(this GameObject gameObject, float z)
        {
            Vector3 angle = gameObject.transform.localEulerAngles;
            gameObject.transform.localEulerAngles = new Vector3(angle.x, angle.y, z);
        }

        public static void SetLocalAngle(this GameObject gameObject, float x, float y, float z)
        {
            gameObject.transform.localEulerAngles = new Vector3(x, y, z);
        }

        public static void SetLocalAngle(this GameObject gameObject, Vector3 angle)
        {
            gameObject.transform.localEulerAngles = angle;
        }

        public static void SetParent(this GameObject gameObject, Transform parent)
        {
            gameObject.transform.SetParent(parent);
        }

        public static void SetParent(this GameObject gameObject, GameObject parent)
        {
            if (gameObject == null)
            {
                return;
            }
            if (parent == null)
            {
                gameObject.transform.SetParent(null);
                return;
            }
            gameObject.transform.SetParent(parent.transform);
        }

        public static void ResetPos(this GameObject gameObject)
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.localEulerAngles = Vector3.zero;
        }

        public static Vector3 GetPos(this GameObject gameObject)
        {
            return gameObject.transform.position;
        }

        public static Vector3 GetLocalPos(this GameObject gameObject)
        {
            return gameObject.transform.localPosition;
        }

        public static Vector3 GetScale(this GameObject gameObject)
        {
            return gameObject.transform.localScale;
        }

        public static Vector3 GetLocalAngle(this GameObject gameObject)
        {
            return gameObject.transform.localEulerAngles;
        }

        public static void SetParent(this GameObject gameObject, Transform parent, bool worldPositionStays)
        {
            gameObject.transform.SetParent(parent, worldPositionStays);
        }

        public static void SetParent(this GameObject gameObject, GameObject parent, bool worldPositionStays)
        {
            if (parent == null)
            {
                gameObject.transform.SetParent(null, worldPositionStays);
                return;
            }
            gameObject.transform.SetParent(parent.transform, worldPositionStays);
        }
        #endregion

        #region RectTransform
        public static void SetAnchoredPosX(this GameObject gameObject, float x)
        {
            RectTransform rectrf = gameObject.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.anchoredPosition = new Vector2(x, rectrf.anchoredPosition.y);
        }

        public static void SetAnchoredPosY(this GameObject gameObject, float y)
        {
            RectTransform rectrf = gameObject.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.anchoredPosition = new Vector2(rectrf.anchoredPosition.x, y);
        }

        public static void SetAnchoredPosZ(this GameObject gameObject, float z)
        {
            RectTransform rectrf = gameObject.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.anchoredPosition3D = new Vector3(rectrf.anchoredPosition.x, rectrf.anchoredPosition.y, z);
        }

        public static void SetAnchoredPos(this GameObject gameObject, float x, float y)
        {
            RectTransform rectrf = gameObject.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.anchoredPosition = new Vector2(x, y);
        }

        public static void SetAnchoredPos(this GameObject gameObject, Vector2 pos)
        {
            RectTransform rectrf = gameObject.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.anchoredPosition = pos;
        }

        public static void SetSizeDeltaX(this GameObject gameObject, float x)
        {
            RectTransform rectrf = gameObject.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.sizeDelta = new Vector2(x, rectrf.sizeDelta.y);
        }

        public static void SetSizeDeltaY(this GameObject gameObject, float y)
        {
            RectTransform rectrf = gameObject.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.sizeDelta = new Vector2(rectrf.sizeDelta.x, y);
        }

        public static void SetSizeDelta(this GameObject gameObject, float x, float y)
        {
            RectTransform rectrf = gameObject.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.sizeDelta = new Vector2(x, y);
        }

        public static void SetSizeDelta(this GameObject gameObject, Vector2 size)
        {
            RectTransform rectrf = gameObject.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.sizeDelta = size;
        }

        public static RectTransform GetRectrf(this GameObject gameObject)
        {
            RectTransform rectrf = gameObject.GetComponent<RectTransform>();
            if (rectrf == null)
            {
                Debug.LogWarning("获取不到rectTransform组件:" + "名称:" + gameObject.name);
            }
            return rectrf;
        }
        #endregion
    }
}
