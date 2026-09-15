# How to get touch position relative to ListView item in ItemTapped event in .NET MAUI ListView (SfListView)?

This example demonstrate how to get touch position relative to listview item in itemtapped event in .NET MAUI ListView.

## Sample

```xaml
<listView:SfListView
                x:Name="listView"
                ItemSize="50"
                ItemsSource="{Binding Contacts}"
                ScrollBarVisibility="Never"
                SelectionMode="None">
    <listView:SfListView.ItemTemplate>
        <DataTemplate>
            ...
        </DataTemplate>
    </listView:SfListView.ItemTemplate>
</listView:SfListView>
```

```c#
public class Behavior : Behavior<ContentPage>
{
    #region Fields
    private SfListView listView;
    private ContentPage contentPage;
    #endregion

    #region Overrides
    protected override void OnAttachedTo(ContentPage bindable)
    {
        contentPage = bindable;
        listView = bindable.FindByName<SfListView>("listView");
        base.OnAttachedTo(bindable);
        listView.ItemTapped += OnListViewItemTapped;
    }
    
    #endregion

    #region Private Methods
    private void OnListViewItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        var listViewItemYPosition = GetListViewItemPosition(GetListViewItem(e.DataItem));
        var positionYBasedOnItem = e.Position.Y - listViewItemYPosition;
        contentPage.DisplayAlert("Item Tapped", "Item tapped at Y position: " + positionYBasedOnItem, "Ok");
        
    }

    private object GetListViewItem(object obj)
    {
        var item = obj as Contact;
        var Layout = listView.ItemsLayout as LinearLayout;
        var rowItems = Layout!.GetType().GetRuntimeFields().First(p => p.Name == "items").GetValue(Layout) as IList;

        foreach (ListViewItemInfo iteminfo in rowItems)
        {
            if (iteminfo.Element != null && iteminfo.DataItem == item)
            {
                return iteminfo.Element as View;
            }
        }

        return null;
    }

    private double GetListViewItemPosition(object obj)
    {
        var platformView = (obj as ListViewItem)!.Handler!.PlatformView;
#if WINDOWS
        var native = platformView as Microsoft.UI.Xaml.UIElement;
        var point = native!.TransformToVisual(null).TransformPoint(new Windows.Foundation.Point(0, 0));
        return point.Y;
#elif ANDROID
        var native = platformView as Android.Views.View;
        var point = new int[2];
        native!.GetLocationOnScreen(point);
        return point[1] / DeviceDisplay.MainDisplayInfo.Density;
#elif IOS
        var native = platformView as UIKit.UIView;
        var point = native!.ConvertPointToView(new CoreGraphics.CGPoint(0, 0), null);
        return point.Y;
#endif
        return 0;
    }

    #endregion
}
```

## Requirements to run the demo

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)
* Xamarin add-ons for Visual Studio (available via the Visual Studio installer).

## Troubleshooting

### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.
