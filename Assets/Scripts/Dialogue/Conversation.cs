using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Assertions;
using System.Reflection;

namespace Shakespeare.Dialogue
{
    [CreateAssetMenu(menuName = ("Shakespeare/Conversation"))]
    public class Conversation : ScriptableObject
    {
        [SerializeField] List<ConversationNode> _nodes;

        public delegate void ValidateDelegate();
        public event ValidateDelegate onValidated;

        public List<ConversationNode> nodes
        {
            get
            {
                return _nodes;
            }
        }

        private Dictionary<string, ConversationNode> rootNodes = new Dictionary<string, ConversationNode>();

        public void AddNode(Vector2 position)
        {
            Undo.RecordObject(this, "Add node.");

            var node = new ConversationNode();
            node.position = position;
            _nodes.Add(node);

            OnValidate();
        }


        public ConversationNode GetNodeByUUID(string UUID)
        {
            foreach (ConversationNode node in _nodes)
            {
                if (node.UUID == UUID)
                {
                    return node;
                }
            }
            return null;
        }

        public void DeleteNode(ConversationNode node)
        {
            Undo.RecordObject(this, "Delete node.");
            _nodes.Remove(node);

            OnValidate();
        }

        public ConversationNode GetRootNode()
        {
            foreach (var item in rootNodes)
            {
                return item.Value;
            }

            return null;
        }

        // Called when the model is updated in any way.
        void OnValidate()
        {
            HashSet<string> UUIDs = AssignUUIDs();

            UpdateRootNode();

            RemoveNonExistantChildLinks(UUIDs);

            onValidated?.Invoke();
        }

        private void UpdateRootNode()
        {
            foreach (ConversationNode node in _nodes)
            {
                rootNodes[node.UUID] = node;
            }

            foreach (ConversationNode node in _nodes)
            {
                foreach (var child in node.children)
                {
                    if (rootNodes.ContainsKey(child))
                    {
                        rootNodes.Remove(child);
                    }
                }
            }
        }

        private void RemoveNonExistantChildLinks(HashSet<string> UUIDs)
        {
            foreach (var node in _nodes)
            {
                var childrenCopy = node.children.ToArray();
                foreach (var child in childrenCopy)
                {
                    if (!UUIDs.Contains(child))
                    {
                        node.children.Remove(child);
                        EditorUtility.SetDirty(this);
                    }
                }
            }
        }

        private HashSet<string> AssignUUIDs()
        {
            var UUIDs = new HashSet<string>();

            foreach (var node in _nodes)
            {
                while (UUIDs.Contains(node.UUID) || node.UUID == "" || node.UUID == null)
                {
                    node.UUID = System.Guid.NewGuid().ToString();
                    EditorUtility.SetDirty(this);
                }
                // if (node.condition != null)
                // {
                //     while (UUIDs.Contains(node.condition.uniqueId) || node.condition.uniqueId == "")
                //     {
                //         node.condition.uniqueId = System.Guid.NewGuid().ToString();
                //         EditorUtility.SetDirty(this);
                //     }
                //     UUIDs.Add(node.condition.uniqueId);
                // }
                UUIDs.Add(node.UUID);
            }

            return UUIDs;
        }
    }
}
