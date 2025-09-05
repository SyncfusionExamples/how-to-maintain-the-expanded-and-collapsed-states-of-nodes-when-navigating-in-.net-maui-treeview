# how-to-maintain-the-expanded-and-collapsed-states-of-nodes-when-navigating-in-.net-maui-treeview
This repository demonstrates how to maintain the expanded and collapsed states of nodes when navigating in the .NET MAUI TreeView (SfTreeView) control. It provides a sample implementation that uses a model property and event handlers to persist and restore the expansion state of each node, ensuring a consistent user experience across navigation and data reloads.

## Sample
You can keep the expanded and collapsed state of the TreeViewNode after navigation by using a model class property and updating its value whenever the node is expanded or collapsed.

### XAML

```xaml
<ContentPage.Resources>
    <ResourceDictionary>
        <local:ExpanderIconVisibilityConverter x:Key="ExpanderIconVisibilityConverter"/>
        <local:ExpanderIconConverter x:Key="ExpanderIconConverter" />
        <local:ExpanderStateConverer x:Key="ExpanderStateConverer" />
    </ResourceDictionary>
</ContentPage.Resources>

<ContentPage.Content>
    <Grid RowDefinitions="*">
        <treeView:SfTreeView x:Name="treeView" Grid.Row="1"
                           ExpanderWidth="0"
                           Indentation="40"
                           ExpandActionTarget="Node"
                           ChildPropertyName="SubFiles"
                           ItemTemplateContextType="Node" 
                           ItemsSource="{Binding ImageNodeInfo}">
            <treeView:SfTreeView.Behaviors>
                <local:TreeViewBehavior/>
            </treeView:SfTreeView.Behaviors>
            <treeView:SfTreeView.ItemTemplate>
                <DataTemplate>
                    <Grid x:Name="grid" RowSpacing="0" >
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="40" />
                            <ColumnDefinition Width="40" />
                            <ColumnDefinition Width="*" />
                        </Grid.ColumnDefinitions>
                        <Image Grid.Column="0" Source="{Binding IsExpanded, Converter={StaticResource ExpanderIconConverter}}"
                        IsVisible="{Binding HasChildNodes,Converter={StaticResource ExpanderIconVisibilityConverter}}"/>
                        <Image Grid.Column="1" Source="{Binding Content.ImageIcon}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="35" WidthRequest="35"/>
                        <Grid Grid.Column="2" RowSpacing="1" Padding="1,0,0,0" VerticalOptions="Center">
                            <Label LineBreakMode="NoWrap" Text="{Binding ., Converter={x:StaticResource ExpanderStateConverer}}" VerticalTextAlignment="Center"/>
                        </Grid>
                    </Grid>
                </DataTemplate>
            </treeView:SfTreeView.ItemTemplate>
        </treeView:SfTreeView>
    </Grid>
</ContentPage.Content>
```

### Behavior
```csharp
public class TreeViewBehavior : Behavior<SfTreeView>
{
    protected override void OnAttachedTo(SfTreeView bindable)
    {
        bindable.NodeExpanded += Bindable_NodeExpanded;
        bindable.NodeCollapsed += Bindable_NodeCollapsed;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(SfTreeView bindable)
    {
        bindable.NodeExpanded -= Bindable_NodeExpanded;
        bindable.NodeCollapsed -= Bindable_NodeCollapsed;
        base.OnDetachingFrom(bindable);
    }

    private void Bindable_NodeCollapsed(object? sender, NodeExpandedCollapsedEventArgs e)
    {
        (e.Node!.Content as FileManager)!.IsExpanded = e.Node.IsExpanded;
    }

    private void Bindable_NodeExpanded(object? sender, NodeExpandedCollapsedEventArgs e)
    {
        (e.Node!.Content as FileManager)!.IsExpanded = e.Node.IsExpanded;
    }
}
```

### Converters
```csharp
#region ExpanderIconConverter
public class ExpanderIconConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value != null)
            return (bool)value ? "expand.png" : "collapse.png";

        return "expand.png";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return "expand.png";
    }
}
#endregion

#region ExpanderIconVisibilityConverter
public class ExpanderIconVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value != null)
            return (bool)value ? true : false;

        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return false;
    }
}
#endregion

#region ExpanderStateConverer
public class ExpanderStateConverer : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        var treeViewNode = value as Syncfusion.TreeView.Engine.TreeViewNode;
        var dataObject = treeViewNode!.Content as FileManager;
        treeViewNode.IsExpanded = dataObject!.IsExpanded;
        return dataObject.ItemName;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
#endregion
```

## Requirements to run the demo

To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements).

## Troubleshooting:
### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion® has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion® liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion®'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion®'s samples.