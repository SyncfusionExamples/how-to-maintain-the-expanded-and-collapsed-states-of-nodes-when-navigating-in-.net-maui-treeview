using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUITreeView
{
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
}
