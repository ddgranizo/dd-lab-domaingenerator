using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Models;
using DomainGeneratorUI.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Interfaces
{
    public interface IContentEditor<T>
    {
        void SetContext(GenericManager manager, T instance);
        T GetContent();
        EditorWindowResponse GetResponse();
    }
}
