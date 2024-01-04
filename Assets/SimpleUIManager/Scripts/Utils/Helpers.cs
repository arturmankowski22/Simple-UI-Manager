using System.Collections.Generic;
using SUIM.Components.Views;
using UnityEngine;

namespace SUIM.Utils
{
    public static class Helpers
    {
        public static ViewBase GetDeepestChild(ViewBase view)
        {
            if (view.ChildViews == null || view.ChildViews.Count == 0)
                return view;
            return GetDeepestChild(view.ChildViews[0]);
        }

        public static List<T> FindAllComponentsInChildrenHierarchy<T>(GameObject gameObject) where T : Component
        {
            var components = new List<T>();
            FindComponentsInChildrenRecursive(gameObject.transform, components);
            return components;
        }

        private static void FindComponentsInChildrenRecursive<T>(Transform parent, ICollection<T> components)
            where T : Component
        {
            var component = parent.GetComponent<T>();
            if (component != null)
                components.Add(component);

            var childCount = parent.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var child = parent.GetChild(i);
                FindComponentsInChildrenRecursive(child, components);
            }
        }
    }
}