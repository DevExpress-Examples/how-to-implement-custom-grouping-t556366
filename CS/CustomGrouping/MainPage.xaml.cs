using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace CustomGrouping {
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
            grid.ItemsSource = new ProductList();
            grid.Columns["UnitPrice"].GroupIndex = 0;
        }

        private void GridControl_CustomColumnGroup(object sender, DevExpress.UI.Xaml.Grid.CustomColumnSortEventArgs e) {
            if (e.Column.FieldName == "UnitPrice") {
                double x = Math.Floor(Convert.ToDouble(e.Value1) / 10);
                double y = Math.Floor(Convert.ToDouble(e.Value2) / 10);
                int res = Comparer<double>.Default.Compare(x, y);
                if (x > 9 && y > 9) res = 0;
                e.Result = res;
                e.Handled = true;
            }
        }

        private void GridControl_CustomGroupDisplayText(object sender, DevExpress.UI.Xaml.Grid.CustomGroupDisplayTextEventArgs e) {
            if (e.Column.FieldName != "UnitPrice") return;
            string interval = IntervalByValue(e.Value);
            e.DisplayText = interval;

        }
        private string IntervalByValue(object val) {
            double d = Math.Floor(Convert.ToDouble(val) / 10);
            string ret = string.Format("{0:c} - {1:c} ", d * 10, (d + 1) * 10);
            if (d > 9) ret = string.Format(">= {0:c} ", 100);
            return ret;
        }

    }
}
