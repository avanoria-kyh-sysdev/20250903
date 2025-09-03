using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cts;
        private bool _isDownloading;

        private static readonly string _fileName = "2025-05-13-raspios-bookworm-arm64-lite.img.xz";
        private static readonly string _sourceFilePath = $"https://downloads.raspberrypi.com/raspios_lite_arm64/images/raspios_lite_arm64-2025-05-13/{_fileName}";

        private static readonly string _targetFolderPath = @"c:\downloads";
        private static readonly string _targetFilePath = @$"{_targetFolderPath}\{_fileName}";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Btn_Download_Click(object sender, RoutedEventArgs e)
        {
            if (_isDownloading)
            {
                _cts?.Cancel();
                return;
            }

            try
            {
                CreateDownloadFolder();
                DeleteDownloadedFile();

                Lv_EventLog.Items.Add($"{DateTime.Now} \t Downloading '{_fileName[..30]}...'");
                Lv_EventLog.ScrollIntoView(Lv_EventLog.Items[Lv_EventLog.Items.Count - 1]);

                _cts = new CancellationTokenSource();
                _isDownloading = true;
                Btn_Download.Content = "Cancel";

                await DownloadSourceFileAsync(_cts.Token);

                Lv_EventLog.Items.Add($"{DateTime.Now} \t Download completed.");
            }
            catch (OperationCanceledException) 
            {
                Lv_EventLog.Items.Add($"{DateTime.Now} \t Download canceled.");
                DeleteDownloadedFile();
            }
            catch (Exception ex)
            {
                Lv_EventLog.Items.Add($"{DateTime.Now} \t Error: {ex.Message}");
                DeleteDownloadedFile();
            }
            finally
            {
                _isDownloading = false;
                _cts = null!;
                Btn_Download.Content = "Download";
                Lv_EventLog.ScrollIntoView(Lv_EventLog.Items[Lv_EventLog.Items.Count - 1]);
            }


        }

        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            Lv_EventLog.Items.Clear();
        }


        private async Task DownloadSourceFileAsync(CancellationToken token)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_sourceFilePath, token);

            await using var fileStream = new FileStream(_targetFilePath, FileMode.CreateNew);
            await response.Content.CopyToAsync(fileStream, token);
        }

        private void DeleteDownloadedFile()
        {
            if (File.Exists(_targetFilePath))
                File.Delete(_targetFilePath);
        }

        private void CreateDownloadFolder()
        {
            if (!Directory.Exists(_targetFolderPath))
            {
                Directory.CreateDirectory(_targetFolderPath);
            }  
        }
    }
}