using DomainGeneratorUI.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Interfaces
{
    public interface IContentEditor<T>
    {
        void SetContent(T instance);
        T GetContent();
        EditorWindowResponse GetResponse();
    }
}
