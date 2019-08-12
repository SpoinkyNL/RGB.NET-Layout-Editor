﻿using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Win32;
using RGB.NET.Core.Layout;
using Stylet;

namespace LayoutEditor.UI.Pages
{
    public class DeviceLayoutEditorViewModel : Screen
    {
        public DeviceLayoutEditorViewModel(DeviceLayout deviceLayout)
        {
            DeviceLayout = deviceLayout;

        }

        public DeviceLayout DeviceLayout { get; }

        public async void SelectDeviceImage()
        {
            await Task.Run(() =>
            {
                var dialog = new OpenFileDialog { CheckFileExists = true, Filter = "Image files(*.PNG)|*.PNG" };
                dialog.ShowDialog();
                DeviceImagePath = dialog.FileName;
            });
        }

        public async void Save()
        {
            // Set the base path for images
            DeviceLayout.ImageBasePath = Path.Combine("Images", DeviceLayout.Vendor, DeviceLayout.Type + "s");

            await Task.Run(() =>
            {
                var dialog = new SaveFileDialog {Filter = "Layout Files(*.XML)|*.XML"};
                dialog.ShowDialog();

                var writer = new XmlSerializer(typeof(DeviceLayout));
                var file = File.Create(dialog.FileName);
                writer.Serialize(file, DeviceLayout);
                file.Close();
            });
        }

        public string DeviceImagePath { get; set; }
    }
}