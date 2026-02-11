#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.NetLogic;
#endregion

public class PanelBuilder : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void AddPanelToNavigation(string title, NodeId panelId, NodeId aliasId)
    {
        // Get the navigation panel (Owner of this NetLogic)
        var navigationPanel = Owner;

        // Get the Panels node where NavigationPanelItems should be added
        var panelItems = navigationPanel.Get("Panels");

        // Create a new NavigationPanelItem
        var newPanelItem = InformationModel.Make<NavigationPanelItem>("Panel");

        // Set all NavigationPanelItem properties
        newPanelItem.Title = title;
        newPanelItem.Panel = panelId;
        newPanelItem.AliasNode = aliasId;
        newPanelItem.Enabled = true;
        newPanelItem.Visible = true;

        // Add the navigation item to the navigation panel
        panelItems.Add(newPanelItem);
    }

    [ExportMethod]
    public void BuildAllPanels()
    {
        // nodeID of the screen used for each of the tabs added to the navigation panel
        var commonPanelId = Project.Current.Get("UI/Screens/CustomScreenTemplate").NodeId;

        var aliasFolder = Project.Current.Get("Model/DataObjects");

        foreach (var child in aliasFolder.Children)
        {
            var title = child.BrowseName;
            var panelId = commonPanelId;
            var aliasId = child.NodeId;
            AddPanelToNavigation(title, panelId, aliasId);
        }
    }
}
