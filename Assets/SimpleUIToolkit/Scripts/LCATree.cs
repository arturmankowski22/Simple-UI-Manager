using System;
using System.Collections.Generic;
using System.Linq;
using SUIT;
using SUIT.Components.Views;
using SUIT.Utils;
using UnityEngine;
using Logger = SUIT.Utils.Logger;

namespace SUIT
{
    #region Editor Class

#if UNITY_EDITOR
    public static class LCATreeBuilder
    {
        public static LCATree Build(SUITViewsManager suitViewsManager)
        {
            var lcaTree = new LCATree();
            var availableViews = Helpers.FindAllComponentsInChildrenHierarchy<ViewBase>(suitViewsManager.gameObject);
            if (availableViews.Count == 0)
            {
                Logger.LogWarning("No views detected under the root view.");
                return lcaTree;
            }

            lcaTree.BuildLCATable(suitViewsManager.GetComponent<RootView>(), availableViews);
            Logger.Log("Views tree rebuilt successfully!");
            return lcaTree;
        }
    }
#endif

    #endregion

    [Serializable]
    public sealed class LCATree
    {
        [SerializeField] private List<LCATableEntry> _lcaTable;
        [SerializeField] private List<ViewBase> _availableViews;
        public List<ViewBase> AvailableViews => _availableViews;

        public ViewBase FindCommonParent(ViewBase node1, ViewBase node2)
        {
            var entry1 = GetLCATableEntry(node1);
            var entry2 = GetLCATableEntry(node2);

            if (entry1 == null || entry2 == null)
                return null;

            var ancestorsOfNode1 = new HashSet<ViewBase>();

            while (entry1 != null)
            {
                ancestorsOfNode1.Add(entry1.Node);
                entry1 = GetLCATableEntry(entry1.Parent);
            }

            while (entry2 != null)
            {
                if (ancestorsOfNode1.Contains(entry2.Node)) return entry2.Node;
                entry2 = GetLCATableEntry(entry2.Parent);
            }

            return null;
        }

        public ViewBase GetView(Type viewType)
        {
            if (!viewType.IsSubclassOf(typeof(ViewBase)))
                throw new TypeAccessException();

            var view = AvailableViews.FirstOrDefault(x => x.GetType() == viewType);
            if (view == null)
                throw new NullReferenceException($"View of type=[{viewType}]");

            return view;
        }

        private LCATableEntry GetLCATableEntry(ViewBase node)
        {
            return _lcaTable.Find(entry => entry.Node == node);
        }

        [Serializable]
        private class LCATableEntry
        {
            [SerializeField] private ViewBase _node;
            [SerializeField] private ViewBase _parent;

            public LCATableEntry(ViewBase node, ViewBase parent)
            {
                _node = node;
                _parent = parent;
            }

            public ViewBase Node => _node;
            public ViewBase Parent => _parent;
        }

        #region Editor Methods

#if UNITY_EDITOR
        public void BuildLCATable(ViewBase rootView, List<ViewBase> availableViews)
        {
            _lcaTable = new List<LCATableEntry>();
            _availableViews = availableViews;
            PreprocessLCATable(rootView, null);

            foreach (var view in _availableViews)
                view.ChildViews.Clear();

            foreach (var view in _availableViews)
                view.SetupParentViewFromEditor(TryFindParentView(view.transform));
        }

        private void PreprocessLCATable(ViewBase current, ViewBase parent)
        {
            _lcaTable.Add(new LCATableEntry(current, parent));
            foreach (var child in current.ChildViews)
                PreprocessLCATable(child, current);
        }

        private static ViewBase TryFindParentView(Transform currentObject)
        {
            while (true)
            {
                if (currentObject.transform.parent == null)
                    return null;

                var parent = currentObject.transform.parent;
                if (parent.TryGetComponent<ViewBase>(out var parentView))
                    return parentView;

                currentObject = parent;
            }
        }
#endif

        #endregion
    }
}