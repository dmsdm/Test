using System;
using Test.ViewModels;
using Xamarin.Forms;

namespace Test.Views
{
	public partial class TranslatePage : ContentPage
	{
		public TranslatePage()
		{
			InitializeComponent();

            ((TestViewModel)BindingContext).SetTranslator(new Translator());
            ((TestViewModel)BindingContext).SetHistory(new History());
            ((TestViewModel)BindingContext).TranslateEnabled = false;
            ((TestViewModel)BindingContext).SaveEnabled = false;
        }

        private class Translator : ITranslator
        {
            public void GetLangs(TestViewModel viewModel)
            {
                GetLangsAsync(viewModel);
            }

            public void Translate(string direction, string text, TestViewModel viewModel)
            {
                Lookup(direction, text, viewModel);
            }

            private async void GetLangsAsync(TestViewModel viewModel)
            {
                viewModel.TranslateDirections = await Data.Dictionary.GetLangs();
            }

            private async void Lookup(String direction, String text, TestViewModel viewModel)
            {
                viewModel.Result = await Data.Dictionary.Lookup(direction, text);
            }
        }

        private class History : IHistory
        {
            public void Save(string direction, string text, string result)
            {
                Data.History.Save(direction, text, result);
            }
        }
    }
}