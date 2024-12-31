using Syncfusion.Maui.ListView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
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
        

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            listView.ItemTapped -= OnListViewItemTapped;
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
}
