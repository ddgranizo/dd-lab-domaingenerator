using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIClient.Utilities
{
    public static class FileDialogManager
    {

        //public static Task<string[]> SelectFiles(Window parent, string defaultPath = null)
        //{
        //    var file = string.Empty;

        //    var openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filters = new List<FileDialogFilter>() { new FileDialogFilter() { Extensions = new List<string>() { "*.json" } } };
        //    if (!string.IsNullOrEmpty(defaultPath))
        //    {
        //        openFileDialog.InitialDirectory = defaultPath;
        //    }
        //    var files =  openFileDialog.ShowAsync(parent);
        //    return openFileDialog.ShowAsync(parent);
           
        //}

        public static string SelectPath(string defaultPath = null)
        {
            string path = null;
            //using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            //{
            //    if (!string.IsNullOrEmpty(defaultPath) && Directory.Exists(defaultPath))
            //    {
            //        fbd.SelectedPath = defaultPath;
            //    }
            //    System.Windows.Forms.DialogResult result = fbd.ShowDialog();
            //    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            //    {
            //        path = fbd.SelectedPath;
            //    }
            //}
            return path;
        }
    }
}
