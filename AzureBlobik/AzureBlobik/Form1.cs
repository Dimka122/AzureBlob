using Microsoft.Azure.Storage;
using System;
using System.Windows.Forms;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using EnvDTE;

namespace AzureBlobik
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            
            
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=dimka777;AccountKey=OOwuRqytL1ap6MPYWGi1isXSRPq7Umv8IjGKHhofKvOav02m+pEaVPyXKhInKY3icyOupLgP9Yq7+AStFllKgA==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("blobs");

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(Path.GetFileName(fileName));
                using (var fileStream = System.IO.File.OpenRead(fileName))
                {
                    blob.UploadFromStream(fileStream);
                }
                listBox1.Items.Add(fileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=dimka777;AccountKey=OOwuRqytL1ap6MPYWGi1isXSRPq7Umv8IjGKHhofKvOav02m+pEaVPyXKhInKY3icyOupLgP9Yq7+AStFllKgA==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("blobs");

            if (listBox1.SelectedItem != null)
            {
                string fileName = listBox1.SelectedItem.ToString();
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);
                blob.DeleteIfExists();
                listBox1.Items.Remove(fileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=dimka777;AccountKey=OOwuRqytL1ap6MPYWGi1isXSRPq7Umv8IjGKHhofKvOav02m+pEaVPyXKhInKY3icyOupLgP9Yq7+AStFllKgA==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("blobs");

            listBox1.Items.Clear();
            foreach (IListBlobItem item in blobContainer.ListBlobs(null, true, BlobListingDetails.Metadata))
            {
                if (item is CloudBlob blob)
                {
                    listBox1.Items.Add(blob.Name);
                }
            }
        }
    }
}
