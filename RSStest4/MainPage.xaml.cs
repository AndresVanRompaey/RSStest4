using System;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RSStest4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Load the RSS file from the RSS URL
            XmlDocument.LoadFromUriAsync(new Uri("http://www.verkeerscentrum.be/rss/1-LOS.xml"))
                .Completed = XmlLoadedAsync;
        }

        private async Task UpdateTextBox(string data)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => txtRSS.Text = data);
        }

        private async void XmlLoadedAsync(IAsyncOperation<XmlDocument> asyncInfo, AsyncStatus asyncStatus)
        {
            var rssXDoc = asyncInfo.GetResults();

            // Parse the Items in the RSS file
            var rssNodes = rssXDoc.SelectNodes("rss/channel/item");

            var rssContent = new StringBuilder();

            // Iterate through the items in the RSS file
            foreach (var rssNode in rssNodes)
            {
                var rssSubNode = rssNode.SelectSingleNode("title");
                var title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssContent.AppendLine($"{title}");
            }

            // Return the string that contain the RSS items
            await UpdateTextBox(rssContent.ToString());
        }
    }
}
