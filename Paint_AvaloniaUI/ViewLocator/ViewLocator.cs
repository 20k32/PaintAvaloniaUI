using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_AvaloniaUI.ViewLocator
{
    internal class ViewLocator : IDataTemplate
    {
        public Control? Build(object? param)
        {
            var fileName = param.GetType().FullName;

            var fullFileName = fileName.Replace("ViewModel", "View");

            var type = Type.GetType(fullFileName);

            if (type is not null)
            {
                return Activator.CreateInstance(type) as Control;
            }

            return new TextBlock() { Text = $"File {fullFileName} not found for {fileName}." };
        }

        public bool Match(object? data) =>
            data is ILocatorAccessible;
    }
}
