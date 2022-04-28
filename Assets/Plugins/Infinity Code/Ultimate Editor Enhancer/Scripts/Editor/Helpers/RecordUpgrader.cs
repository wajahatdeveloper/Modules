/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.SceneTools;
using InfinityCode.UltimateEditorEnhancer.UnityTypes;
using UnityEditor;

namespace InfinityCode.UltimateEditorEnhancer
{
    [InitializeOnLoad]
    public static class RecordUpgrader
    {
        private const int CurrentUpgradeID = 1;
        private const string BookmarkItemSeparator = "|";

        static RecordUpgrader()
        {
            int upgradeID = LocalSettings.upgradeID;
            if (upgradeID < 1)
            {
                InitDefaultQuickAccessItems();
            }

            LocalSettings.upgradeID = CurrentUpgradeID;
        }

        public static void InitDefaultQuickAccessItems()
        {
            List<QuickAccessItem> items = ReferenceManager.quickAccessItems;
            if (items.Count > 0) return;

            QuickAccessItem save = new QuickAccessItem(QuickAccessItemType.menuItem)
            {
                settings = new[] { "File/Save" },
                icon = QuickAccessItemIcon.texture,
                iconSettings = Resources.iconsFolder + "Save.png",
                tooltip = "Save",
                expanded = false
            };

            QuickAccessItem hierarchy = new QuickAccessItem(QuickAccessItemType.window)
            {
                settings = new[] { SceneHierarchyWindowRef.type.AssemblyQualifiedName },
                icon = QuickAccessItemIcon.texture,
                iconSettings = Resources.iconsFolder + "Hierarchy2.png",
                tooltip = "Hierarchy",
                visibleRules = SceneViewVisibleRules.onMaximized,
                expanded = false
            };

            QuickAccessItem project = new QuickAccessItem(QuickAccessItemType.window)
            {
                settings = new[] { ProjectBrowserRef.type.AssemblyQualifiedName },
                icon = QuickAccessItemIcon.texture,
                iconSettings = Resources.iconsFolder + "Project.png",
                tooltip = "Project",
                visibleRules = SceneViewVisibleRules.onMaximized,
                expanded = false
            };

            QuickAccessItem inspector = new QuickAccessItem(QuickAccessItemType.window)
            {
                settings = new[] { InspectorWindowRef.type.AssemblyQualifiedName },
                icon = QuickAccessItemIcon.texture,
                iconSettings = Resources.iconsFolder + "Inspector.png",
                tooltip = "Inspector",
                visibleRules = SceneViewVisibleRules.onMaximized,
                expanded = false
            };

            QuickAccessItem bookmarks = new QuickAccessItem(QuickAccessItemType.window)
            {
                settings = new[] { "InfinityCode.UltimateEditorEnhancer.Windows.Bookmarks, UltimateEditorEnhancer-Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null" },
                icon = QuickAccessItemIcon.texture,
                iconSettings = Resources.iconsFolder + "Star-White.png",
                tooltip = "Bookmarks",
                expanded = false
            };

            QuickAccessItem viewGallery = new QuickAccessItem(QuickAccessItemType.window)
            {
                settings = new[] { "InfinityCode.UltimateEditorEnhancer.Windows.ViewGallery, UltimateEditorEnhancer-Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null" },
                icon = QuickAccessItemIcon.editorIconContent,
                iconSettings = "d_ViewToolOrbit",
                tooltip = "View Gallery",
                expanded = false
            };

            QuickAccessItem distanceTool = new QuickAccessItem(QuickAccessItemType.window)
            {
                settings = new[] { "InfinityCode.UltimateEditorEnhancer.Windows.DistanceTool, UltimateEditorEnhancer-Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null" },
                icon = QuickAccessItemIcon.texture,
                iconSettings = Resources.iconsFolder + "Rule.png",
                tooltip = "Distance Tool",
                expanded = false
            };

            QuickAccessItem quickAccessSettings = new QuickAccessItem(QuickAccessItemType.settings)
            {
                settings = new[] { "Project/Ultimate Editor Enhancer/Scene View/Quick Access Bar" },
                icon = QuickAccessItemIcon.editorIconContent,
                iconSettings = "d_Settings",
                tooltip = "Edit Items",
                expanded = false
            };

            QuickAccessItem info = new QuickAccessItem(QuickAccessItemType.window)
            {
                settings = new[] { "InfinityCode.UltimateEditorEnhancer.Windows.Welcome, UltimateEditorEnhancer-Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null" },
                icon = QuickAccessItemIcon.editorIconContent,
                iconSettings = "_Help",
                tooltip = "Info",
                expanded = false
            };

            items.Add(save);
            items.Add(hierarchy);
            items.Add(project);
            items.Add(inspector);
            items.Add(bookmarks);
            items.Add(viewGallery);
            items.Add(distanceTool);
            items.Add(new QuickAccessItem(QuickAccessItemType.flexibleSpace));
            items.Add(quickAccessSettings);
            items.Add(info);
        }
    }
}