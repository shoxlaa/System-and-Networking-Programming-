using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HTTP_protocol__project.Models;
using HTTP_protocol__project.Services;
using Microsoft.Win32;

namespace HTTP_protocol__project.ViewModels;

//https://icatcare.org/app/uploads/2018/07/Thinking-of-getting-a-cat.png
public partial class DownloadViewModel : BaseViewModel
{
    [ObservableProperty] private List<int> _longIntegerList;
    [ObservableProperty] private string? _threadsCount;
    [ObservableProperty] private double? _progress;
    [ObservableProperty] private string? _link;
    [ObservableProperty] private ObservableCollection<MyFile> _files;
    [ObservableProperty] private ObservableCollection<MyFile> _allfiles;
    [ObservableProperty] private int _selectedItem;

    private HttpServer _server = new HttpServer();

    public DownloadViewModel()
    {

        _longIntegerList = new List<int>(Enumerable.Range(0, 1000));
        _threadsCount = "1";
        _link = String.Empty;
        _files = new();
        _allfiles = new();
    }

    [RelayCommand]
    void Download()
    {
        if (_link != null)
        {
            _server.Url = _link;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog();
            FileInfo f = new FileInfo(saveFileDialog.FileName);
            _server.FileName = saveFileDialog.FileName;
            _server.Read(Convert.ToInt32(_threadsCount));
            _files.Add(new MyFile() { FileName = f.Name, fileInfo = f });
            Progress = _server._process;

        }
    }
    [RelayCommand]
    void Rename()
    {
        if (SelectedItem != null)
        {
            SaveFileDialog openFile = new();
            openFile.FileName = _files[SelectedItem].FileName;
            openFile.ShowDialog();
            FileInfo info = new(openFile.FileName);
            File.Move(_files[SelectedItem].fileInfo.FullName, info.FullName);
            _files.RemoveAt(SelectedItem);
            _files.Add(new() { FileName = info.Name, fileInfo = info });
        }
    }
    [RelayCommand]
    void Delete()
    {
        if (SelectedItem != null)
        {
            File.Delete(_files[_selectedItem].fileInfo.FullName);
            _files.RemoveAt(_selectedItem);
        }
    }


    [RelayCommand]
    void Stop()
    {
        if (SelectedItem != null && Progress < 100)
        {
            _server.Cancel();
            Progress = 0;
            _files.RemoveAt(SelectedItem);
        }
    }
}