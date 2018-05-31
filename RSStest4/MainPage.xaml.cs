using System.Text;
using Windows.Data.Xml.Dom;
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
            Windows.Data.Xml.Dom.XmlDocument rssXDoc = new Windows.Data.Xml.Dom.XmlDocument();

            // Load the RSS file from the RSS URL
            rssXDoc.LoadXml("http://www.verkeerscentrum.be/rss/1-LOS.xml");

            // Parse the Items in the RSS file
            Windows.Data.Xml.Dom.XmlNodeList rssNodes = rssXDoc.SelectNodes("rss/channel/item");

            StringBuilder rssContent = new StringBuilder();

            // Iterate through the items in the RSS file
            foreach (IXmlNode rssNode in rssNodes)
            {
                IXmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("link");
                string link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";

                rssContent.Append("<a href='" + link + "'>" + title + "</a><br>" + description);
            }

            // Return the string that contain the RSS items
            txtRSS.Text = rssContent.ToString();
        }
    }
}
