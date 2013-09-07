using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AncoraMVVM.Base.Tests
{
    [TestClass]
    public class ToggleButtonTests
    {
        [TestMethod]
        public void Toggled_Changed_RaisesPropertyChangedOnDependentProps()
        {
            var button = new ToggleButton();
            var propertiesChanged = new List<string>();

            button.PropertyChanged += (s, e) => propertiesChanged.Add(e.PropertyName);

            button.Toggled = true;

            Assert.IsTrue(propertiesChanged.Contains("Text"), "Text property change was not raised");
            Assert.IsTrue(propertiesChanged.Contains("Command"), "Command property change was not raised.");
            Assert.IsTrue(propertiesChanged.Contains("Icon"), "Icon property change was not raised.");
        }

        [TestMethod]
        public void Toggled_True_FinalPropertiesAreSet1()
        {
            var button = new ToggleButton()
            {
                Icon1 = "icon1",
                Text1 = "text1",
                Command1 = new DelegateCommand(p => { }),

                Icon2 = "icon2",
                Text2 = "text2",
                Command2 = new DelegateCommand(() => { })
            };

            button.Toggled = true;

            Assert.AreSame(button.Icon1, button.Icon);
            Assert.AreSame(button.Text1, button.Text);
            Assert.AreSame(button.Command1, button.Command);
        }

        [TestMethod]
        public void Toggled_False_FinalPropertiesAreSet2()
        {
            var button = new ToggleButton()
            {
                Icon1 = "icon1",
                Text1 = "text1",
                Command1 = new DelegateCommand(p => { }),

                Icon2 = "icon2",
                Text2 = "text2",
                Command2 = new DelegateCommand(() => { })
            };

            button.Toggled = false;

            Assert.AreSame(button.Icon2, button.Icon);
            Assert.AreSame(button.Text2, button.Text);
            Assert.AreSame(button.Command2, button.Command);
        }
    }
}
