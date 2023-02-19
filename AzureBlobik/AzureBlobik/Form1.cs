using Azure.Storage.Blobs;
using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureBlobik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string BlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=blobdimon;AccountKey=g63GBBTmOsOM95rqBoJQ6aSXouyZV6E5/FkvtyuEm0r4ay+CMGmIkQOxTDLPEOoa+E8HbXFqUTQP+AStKZZXiA==;EndpointSuffix=core.windows.net";
        string BlobStorageContainerName = TextBox.(this);

        List<string> strings = new List<string>();

        var backupBlobClient = CloudStorageAccount.Parse(BlobStorageConnectionString).CreateCloudBlobClient();
        var backupContainer = backupBlobClient.GetContainerReference(BlobStorageContainerName);

        BlobServiceClient blobServiceClient = new BlobServiceClient(BlobStorageConnectionString);
        BlobContainerClient cont = blobServiceClient.GetBlobContainerClient("fileupload");

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
