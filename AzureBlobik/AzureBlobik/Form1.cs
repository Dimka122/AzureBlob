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
        CloudStorageAccount storageAccount = null;
        CloudBlobContainer cloudBlobContainer = null;
        string BlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=blobdimon;AccountKey=g63GBBTmOsOM95rqBoJQ6aSXouyZV6E5/FkvtyuEm0r4ay+CMGmIkQOxTDLPEOoa+E8HbXFqUTQP+AStKZZXiA==;EndpointSuffix=core.windows.net";
        //DefaultEndpointsProtocol=https;AccountName=dimka777;AccountKey=OOwuRqytL1ap6MPYWGi1isXSRPq7Umv8IjGKHhofKvOav02m+pEaVPyXKhInKY3icyOupLgP9Yq7+AStFllKgA==;EndpointSuffix=core.windows.net
        public Form1()
        {
            InitializeComponent();
            this.CreateAccountObjects();
        }

        public async void CreateAccountObjects()
        {
            if (CloudStorageAccount.TryParse(BlobStorageConnectionString, out storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                cloudBlobContainer = cloudBlobClient.GetContainerReference("myFiles");
                if (cloudBlobContainer != null)
                {
                    return;
                }
                //if the container do not exists create it. 
                cloudBlobContainer = cloudBlobClient.GetContainerReference("myFiles" + Guid.NewGuid().ToString());
                await cloudBlobContainer.CreateAsync();

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListItems();
        }
        
        public void ListItems()
        {
            listView1.Items.Clear();

            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = cloudBlobContainer.ListBlobsSegmented(null, blobContinuationToken);

                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    listView1.Items.Add(item.ToString());
                }
            } while (blobContinuationToken != null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (listView1.SelectedIndex!= -1)
            //{
            //var fileName =textBox1.SelectedItem.Text;
            //    var blob = this.cloudBlobContainer.GetBlockBlobReference(Path.GetFileName(fileName));
            //    var result = blob.DeleteIfExists();
            //    if (result == false)
            //    {
            //        MessageBox.Show("Cannot Find File");
            //    }
            //    ListItems();
            //}
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string file = dlg.FileName;
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Path.GetFileName(file));
                await cloudBlockBlob.UploadFromFileAsync(file);
                ListItems();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //BlobContainerClient container = new BlobContainerClient(BlobStorageConnectionString, BlobStorageContainerName);
            //foreach(BlobItem blobItem in container.GetBlobs())
            //{
            //    //FileInfo fileInfo = new FileInfo(blobItem);
            //}
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
