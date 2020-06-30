﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Shakespeare.Dialogue;

namespace Shakespeare.Editor.Dialogue
{
    public class Node
    {
        private GUIStyle style = new GUIStyle();
        private GUIStyle conditionStyle = new GUIStyle();
        private Vector2 size = new Vector2(300, 150);

        private ConversationNode nodeModel;
        private Conversation conversationModel;
        private DialogueEditor editor;
        Vector2? draggingOffset = null;

        public int index { get; }

        public string id { get; }

        public Node(ConversationNode node, Conversation conversation, DialogueEditor dialogueEditor)
        {
            style.normal.background = EditorGUIUtility.Load("node2") as Texture2D;
            style.border = new RectOffset(12, 12, 12, 12);
            style.padding = new RectOffset(20, 20, 20, 20);

            conditionStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            conditionStyle.border = new RectOffset(12, 12, 12, 12);
            conditionStyle.padding = new RectOffset(5, 5, 5, 5);
            conditionStyle.normal.textColor = Color.white;
            conditionStyle.alignment = TextAnchor.MiddleCenter;

            nodeModel = node;
            conversationModel = conversation;
            editor = dialogueEditor;
        }

        public void Draw()
        {
            DrawBox();
            DrawLinks();
            DrawDraggingLink();
        }

        private void DrawBox()
        {
            GUILayout.BeginArea(GetRect(), style);
            var textStyle = new GUIStyle(EditorStyles.textArea);
            textStyle.wordWrap = true;

            EditorGUILayout.BeginVertical();
            EditorGUILayout.PrefixLabel("Dialogue Text");
            nodeModel.text = EditorGUILayout.TextArea(nodeModel.text, textStyle);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Dialogue Action");
            nodeModel.actionToTrigger = (DialogueEvent)EditorGUILayout.EnumPopup(nodeModel.actionToTrigger);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Is Response?");
            nodeModel.isResponse = EditorGUILayout.Toggle(nodeModel.isResponse);
            EditorGUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawLinks()
        {
            foreach (var childId in nodeModel.children)
            {
                var childModel = conversationModel.GetNodeByUUID(childId);
                if (childModel == null) continue;
                Node child = new Node(childModel, conversationModel, editor);
                Handles.DrawBezier(
                    GetCentreBottom(), child.GetCentreTop(),
                    GetCentreBottom() + Vector2.up * 10,
                    child.GetCentreTop() + Vector2.down * 10, Color.white, null, 3
                );
                if (childModel.conditions != null && childModel.conditions.Length > 0)
                {
                    string description = "";
                    int index = 0;

                    foreach (BaseCondition condition in childModel.conditions)
                    {
                        if (condition == null) continue;

                        description += index + ": " + condition.description + "\n";
                        index++;
                    }

                    Handles.Label(
                        GetConditionPosition(this, child),
                        description,
                        conditionStyle
                        );
                }
            }
        }

        private void DrawDraggingLink()
        {
            if (editor.linkingNode != this) return;
            Handles.DrawBezier(
                GetCentreBottom(),
                Event.current.mousePosition,
                GetCentreBottom() + Vector2.up * 10,
                Event.current.mousePosition + Vector2.down * 10,
                Color.white,
                null,
                3
            );
            GUI.changed = true;
        }

        public void ProcessEvent(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (!GetRect().Contains(e.mousePosition)) break;
                    switch (e.button)
                    {
                        case 0:
                            LeftClick(e);
                            break;
                        case 1:
                            RightClick(e);
                            break;
                    }
                    break;
                case EventType.MouseDrag:
                    ExecuteDragging(e);
                    break;
                case EventType.MouseUp:
                    MouseUp(e);
                    break;
            }
        }

        private void LeftClick(Event e)
        {
            if (editor.linkingNode != null)
            {
                editor.linkingNode.nodeModel.children.Add(nodeModel.UUID);
                editor.linkingNode = null;
                GUI.changed = true;
            }
            else
            {
                draggingOffset = e.mousePosition - GetRect().position;
            }
            e.Use();
        }

        private void ExecuteDragging(Event e)
        {
            if (!draggingOffset.HasValue) return;

            nodeModel.position = e.mousePosition - draggingOffset.Value;
            e.Use();
            GUI.changed = true;
        }

        private void MouseUp(Event e)
        {
            if (draggingOffset.HasValue)
            {
                draggingOffset = null;
                e.Use();
            }
        }

        private void RightClick(Event e)
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add connection"), false, () => StartLinking());
            menu.AddItem(new GUIContent("Break child connections"), false, () => BreakChildConnections());
            menu.AddItem(new GUIContent("Delete"), false, () => conversationModel.DeleteNode(nodeModel));
            menu.ShowAsContext();
            e.Use();
        }

        private void StartLinking()
        {
            editor.linkingNode = this;
        }

        private void BreakChildConnections()
        {
            nodeModel.children.Clear();
            GUI.changed = true;
        }


        private Rect GetRect()
        {
            return new Rect(nodeModel.position, size);
        }

        private Vector2 GetConditionPosition(Node fromNode, Node toNode)
        {
            float x = (toNode.GetCentreBottom().x + fromNode.GetCentreTop().x) / 2;
            float y = (toNode.GetCentreBottom().y + fromNode.GetCentreTop().y) / 2;
            return new Vector2(x, y);
        }

        private Vector2 GetCentreBottom()
        {
            var rect = GetRect();
            return new Vector2(rect.center.x, rect.yMax);
        }

        private Vector2 GetCentreTop()
        {
            var rect = GetRect();
            return new Vector2(rect.center.x, rect.yMin);
        }
    }
}
