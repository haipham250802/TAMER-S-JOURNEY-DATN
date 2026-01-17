using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace WE.Unit.Controller
{
    [RequireComponent(typeof(SortingGroup))]
    public class SortingOrderController : MonoBehaviour
    {
#if UNITY_EDITOR
        [GUIColor(0, 1, 0)]
        public bool ShowCheckingBox = true;



#endif

        //public SpriteRenderer renderer;

        [Required]
        public SortingGroup sortingGroup;
        public float OffsetY = 0.0f;

        public float timerMax = 0.2f;

        private const int SortingOrderBase = 10000;


        [SerializeField] private bool runOnlyOnce = false;

        public float sHeight = 1;

        public float sWidth = 1;

        private float timer = 1;

        bool inited;


        void Awake()
        {
            //if (renderer != null)
            //{
            //    sHeight = renderer.sprite.rect.height / renderer.sprite.pixelsPerUnit;
            //}
            if (sortingGroup == null)
                sortingGroup = GetComponent<SortingGroup>();

            //int sortingOrder = (int)((transform.position.y - sHeight / 2 + OffsetY) * -50) + SortingOrderBase;//SortingOrderBase - (int)((pos.y -sHeight/2 + OffsetY)  *100);
            //sortingGroup.sortingOrder = sortingOrder;
            //if (runOnlyOnce)
            //{
            //    enabled = false;
            //}
            timer = timerMax;
        }

        void OnEnable()
        {
            timer = timerMax;
            inited = false;
        }
        void LateUpdate()
        {
            if (runOnlyOnce && inited) return;   
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = timerMax;
                int sortingOrder = (int)((transform.position.y - sHeight / 2 + OffsetY) * -50) + SortingOrderBase;//SortingOrderBase - (int)((pos.y -sHeight/2 + OffsetY)  *100);                
                sortingGroup.sortingOrder = sortingOrder;
                inited = true;
            }
        }


#if UNITY_EDITOR

        //void CalculateSpriteBound()
        //{
        //    if (renderer != null)
        //    {
        //        Rect rect = renderer.sprite.rect;
        //        float ppu = renderer.sprite.pixelsPerUnit;
        //        sWidth = rect.width / ppu;
        //        sHeight = rect.height / ppu;
        //    }
        //}

        void OnDrawGizmos()
        {
            if (ShowCheckingBox)
            {
                //CalculateSpriteBound();

                var color = Gizmos.color;
                Gizmos.color = Color.cyan;
                Vector2 pos = transform.position;
                float hW = sWidth / 2;
                float hH = sHeight / 2;
                float x = pos.x;
                float y = pos.y + OffsetY;

                Gizmos.DrawLine(new Vector3(x - hW, y - hH), new Vector3(x + hW, y - hH));
                Gizmos.DrawLine(new Vector3(x + hW, y - hH), new Vector3(x + hW, y + hH));
                Gizmos.DrawLine(new Vector3(x + hW, y + hH), new Vector3(x - hW, y + hH));
                Gizmos.DrawLine(new Vector3(x - hW, y + hH), new Vector3(x - hW, y - hH));

                Gizmos.color = color;
            }
        }
#endif
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(SortingOrderController)), CanEditMultipleObjects]
    public class SortingOrderControllerEditor : OdinEditor
    {
        private SortingOrderController _target;

        GUIStyle style = new GUIStyle();

        private string typeName;

        protected override void OnEnable()
        {
            _target = target as SortingOrderController;
            style.normal.textColor = Color.white;
            typeName = nameof(SortingOrderController);
        }
        void OnSceneGUI()
        {
            if (_target.ShowCheckingBox)
            {
                string info = $"{typeName} - {_target.name}";
                Undo.RecordObject(_target, info);

                var pos = _target.transform.position;

                var bottomLeftPos = Handles.PositionHandle(
                    new Vector3(pos.x - _target.sWidth / 2, pos.y - _target.sHeight / 2 + _target.OffsetY),
                    Quaternion.identity);

                var topRightPos = Handles.PositionHandle(
                    new Vector3(pos.x + _target.sWidth / 2, pos.y + _target.sHeight / 2 + _target.OffsetY),
                    Quaternion.identity);

                var offset = Handles.PositionHandle(new Vector3(pos.x, pos.y + _target.OffsetY), Quaternion.identity);


                Handles.Label(bottomLeftPos, "Bottom Left Sorting Bound");
                Handles.Label(topRightPos, "Top Right Sorting Bound");
                Handles.Label(offset, "Offset Sorting Bound");

                _target.sWidth = Mathf.Abs(topRightPos.x - bottomLeftPos.x);
                _target.sHeight = Mathf.Abs(topRightPos.y - bottomLeftPos.y);
                _target.OffsetY = offset.y - pos.y;
            }
        }
    }
#endif
}

