using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.ViewModels;

namespace UnitTestProject1
{
    [TestClass]
    public class TestViewModelTest
    {
        TestViewModel vm;

        [TestInitialize]
        public void SetUp()
        {
            vm = new TestViewModel();
            vm.SetTranslator(new Translator());
            History history = new History();
            history.vm = vm;
            vm.SetHistory(history);
            vm.TranslateDirection = vm.TranslateDirections[0];
            vm.Text = "text";
        }

        [TestMethod]
        public void GetLangs_WhenTranslatorSet()
        {
            Assert.AreEqual("en-ru", vm.TranslateDirections[0], "wrong: " + vm.TranslateDirections[0]);
        }

        [TestMethod]
        public void TranslateCalled()
        {
            vm.Translate.Execute(null);

            Assert.AreEqual<String>("Translated text (en-ru)", vm.Result, "wrong: " + vm.Result);
        }

        [TestMethod]
        public void SaveDisabled()
        {
            Assert.IsFalse(vm.SaveEnabled);
        }

        [TestMethod]
        public void SaveEnabled_WhenTranslated()
        {
            vm.Translate.Execute(null);

            Assert.IsTrue(vm.SaveEnabled);
        }

        [TestMethod]
        public void SaveDisabled_WhenSaved()
        {
            vm.SaveEnabled = true;

            vm.Save.Execute(null);

            Assert.IsFalse(vm.SaveEnabled);
        }

        [TestMethod]
        public void SaveCalled()
        {
            vm.Save.Execute(null);

            Assert.AreEqual("saved", vm.Result, "wrong: " + vm.Result);
        }

        private class Translator : ITranslator
        {
            public void GetLangs(TestViewModel viewModel)
            {
                viewModel.TranslateDirections = new string[] { "en-ru" };
            }

            public void Translate(string direction, string text, TestViewModel viewModel)
            {
                viewModel.Result = String.Format("Translated {0} ({1})", text, direction);
            }
        }

        private class History : IHistory
        {
            public TestViewModel vm;

            public void Save(string direction, string text, string result)
            {
                vm.Result = "saved";
            }
        }
    }
}