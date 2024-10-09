using UnityEngine;

namespace WEngine
{
    public static class ComponentExtension
    {
        public static void SetPosX(this Component component, float x)
        {
            Vector3 pos = component.transform.position;
            component.transform.position = new Vector3(x, pos.y, pos.z);
        }

        public static void SetPosY(this Component component, float y)
        {
            Vector3 pos = component.transform.position;
            component.transform.position = new Vector3(pos.x, y, pos.z);
        }

        public static void SetPosZ(this Component component, float z)
        {
            Vector3 pos = component.transform.position;
            component.transform.position = new Vector3(pos.x, pos.y, z);
        }

        public static void SetPos(this Component component, Vector3 pos)
        {
            component.transform.position = pos;
        }

        public static void SetPos(this Component component, float x, float y, float z)
        {
            component.transform.position = new Vector3(x, y, z);
        }

        public static void SetLocalPosX(this Component component, float x)
        {
            Vector3 pos = component.transform.localPosition;
            component.transform.localPosition = new Vector3(x, pos.y, pos.z);
        }

        public static void SetLocalPosY(this Component component, float y)
        {
            Vector3 pos = component.transform.localPosition;
            component.transform.localPosition = new Vector3(pos.x, y, pos.z);
        }

        public static void SetLocalPosZ(this Component component, float z)
        {
            Vector3 pos = component.transform.localPosition;
            component.transform.localPosition = new Vector3(pos.x, pos.y, z);
        }

        public static void SetLocalPos(this Component component, Vector3 pos)
        {
            component.transform.localPosition = pos;
        }

        public static void SetLocalPos(this Component component, float x, float y, float z)
        {
            component.transform.localPosition = new Vector3(x, y, z);
        }

        public static void SetScaleX(this Component component, float x)
        {
            Vector3 scale = component.transform.localScale;
            component.transform.localScale = new Vector3(x, scale.y, scale.z);
        }

        public static void SetScaleY(this Component component, float y)
        {
            Vector3 scale = component.transform.localScale;
            component.transform.localScale = new Vector3(scale.x, y, scale.z);
        }

        public static void SetScaleZ(this Component component, float z)
        {
            Vector3 scale = component.transform.localScale;
            component.transform.localScale = new Vector3(scale.x, scale.y, z);
        }

        public static void SetScale(this Component component, float x, float y, float z)
        {
            component.transform.localScale = new Vector3(x, y, z);
        }

        public static void SetScale(this Component component, Vector3 scale)
        {
            component.transform.localScale = scale;
        }

        public static void ResetPos(this Component component)
        {
            component.transform.localPosition = Vector3.zero;
            component.transform.localScale = Vector3.one;
            component.transform.localEulerAngles = Vector3.zero;
        }

        public static Vector3 GetPos(this Component component)
        {
            return component.transform.position;
        }

        public static Vector3 GetLocalPos(this Component component)
        {
            return component.transform.localPosition;
        }

        public static Vector3 GetScale(this Component component)
        {
            return component.transform.localScale;
        }

        public static Vector3 GetLocalAngle(this Component component)
        {
            return component.transform.localEulerAngles;
        }

        public static void SetLocalAngleX(this Component component, float x)
        {
            Vector3 angle = component.transform.localEulerAngles;
            component.transform.localEulerAngles = new Vector3(x, angle.y, angle.z);
        }

        public static void SetLocalAngleY(this Component component, float y)
        {
            Vector3 angle = component.transform.localEulerAngles;
            component.transform.localEulerAngles = new Vector3(angle.x, y, angle.z);
        }

        public static void SetLocalAngleZ(this Component component, float z)
        {
            Vector3 angle = component.transform.localEulerAngles;
            component.transform.localEulerAngles = new Vector3(angle.x, angle.y, z);
        }

        public static void SetLocalAngle(this Component component, float x, float y, float z)
        {
            component.transform.localEulerAngles = new Vector3(x, y, z);
        }

        public static void SetLocalAngle(this Component component, Vector3 angle)
        {
            component.transform.localEulerAngles = angle;
        }

        public static void SetAnchoredPosX(this Component component, float x)
        {
            RectTransform rectrf = component.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.anchoredPosition = new Vector2(x, rectrf.anchoredPosition.y);
        }

        public static void SetAnchoredPosY(this Component component, float y)
        {
            RectTransform rectrf = component.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.anchoredPosition = new Vector2(rectrf.anchoredPosition.x, y);
        }

        public static void SetAnchoredPos(this Component component, float x, float y)
        {
            RectTransform rectrf = component.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.anchoredPosition = new Vector2(x, y);
        }

        public static void SetAnchoredPos(this Component component, Vector2 pos)
        {
            RectTransform rectrf = component.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.anchoredPosition = pos;
        }

        public static void SetSizeDeltaX(this Component component, float x)
        {
            RectTransform rectrf = component.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.sizeDelta = new Vector2(x, rectrf.sizeDelta.y);
        }

        public static void SetSizeDeltaY(this Component component, float y)
        {
            RectTransform rectrf = component.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.sizeDelta = new Vector2(rectrf.sizeDelta.x, y);
        }

        public static void SetSizeDelta(this Component component, float x, float y)
        {
            RectTransform rectrf = component.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.sizeDelta = new Vector2(x, y);
        }

        public static void SetSizeDelta(this Component component, Vector2 size)
        {
            RectTransform rectrf = component.GetRectrf();
            if (rectrf == null)
                return;

            rectrf.sizeDelta = size;
        }

        public static RectTransform GetRectrf(this Component component)
        {
            if (component is RectTransform)
                return component as RectTransform;

            RectTransform rectrf = component.GetComponent<RectTransform>();
            if (rectrf == null)
            {
                Debug.LogWarning("获取不到rectTransform组件:" + "名称:" + component.name);
            }
            return rectrf;
        }

        /// <summary>判断是否和另外一个RectTransform重叠</summary>
        public static bool IsRectTransformOverlap(this RectTransform rect1, RectTransform rect2)
        {
            Vector3[] corners = new Vector3[4];
            rect1.GetWorldCorners(corners);
            corners[2].x = Mathf.Abs(corners[2].x - corners[0].x);
            corners[2].y = Mathf.Abs(corners[2].y - corners[0].y);
            Rect b1 = new Rect(corners[0].x, corners[0].y, corners[2].x, corners[2].y);


            rect2.GetWorldCorners(corners);
            corners[2].x = Mathf.Abs(corners[2].x - corners[0].x);
            corners[2].y = Mathf.Abs(corners[2].y - corners[0].y);
            Rect b2 = new Rect(corners[0].x, corners[0].y, corners[2].x, corners[2].y);
            return b1.Overlaps(b2);
        }
    }
}