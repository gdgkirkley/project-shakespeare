using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Shakespeare.Dialogue;

namespace Shakespeare.Editor.Dialogue
{
    public class ConditionNode
    {
        private GUIStyle style = new GUIStyle();
        private Vector2 size = new Vector2(100, 150);

        private BaseCondition conditionModel;

        public int index { get; }

        public string id { get; }

        public ConditionNode(BaseCondition condition)
        {
            style.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            style.border = new RectOffset(12, 12, 12, 12);
            style.padding = new RectOffset(20, 20, 20, 20);

            conditionModel = condition;

            Draw();
        }

        public void Draw()
        {
            DrawBox();
        }

        private void DrawBox()
        {
            GUILayout.BeginArea(GetRect(), style);
            var textStyle = new GUIStyle(EditorStyles.textArea);
            textStyle.wordWrap = true;

            GUILayout.EndArea();
        }

        private Rect GetRect()
        {
            return new Rect(conditionModel.position, size);
        }
    }
}