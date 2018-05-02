using System;
using Test.ViewModels;
using Xamarin.Forms;

namespace Test.Data
{
    class History
    {
        private static HistoryModel historyModel;

        internal static void Save(string direction, string text, string result)
        {
            if (Application.Current.Properties != null)
            {
                String key = direction + ":" + text;
                Application.Current.Properties.Add(key, result);
                if (historyModel != null)
                {
                    HistoryModel.HistoryItem historyItem = new HistoryModel.HistoryItem();
                    historyItem.Text = key;
                    historyItem.Detail = result;
                    historyModel.Items.Add(historyItem);
                }
            }
        }

        internal static void Load(HistoryModel bindingContext)
        {
            historyModel = bindingContext;
            if (Application.Current.Properties != null)
            {
                historyModel.Items.Clear();
                foreach (String key in Application.Current.Properties.Keys)
                {
                    HistoryModel.HistoryItem historyItem = new HistoryModel.HistoryItem();
                    historyItem.Text = key;
                    historyItem.Detail = (String)Application.Current.Properties[key];
                    historyModel.Items.Add(historyItem);
                }
            }
        }
    }
}