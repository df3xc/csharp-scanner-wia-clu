using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;

namespace ScannerDemo
{
    public partial class Form1 : Form
    {
        string document_image_path = "";
        string output_path;
        string image_filename;
        int color_mode = 4;
        DateTime dt = new DateTime();
        string DestMailAddress = "carsten.lueck@outlook.com";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ListScanners() == false)
            {
                timer1.Enabled = true;
                InfoDialog info = new InfoDialog();
                info.InfoText("Es wurde kein Scanner gefunden. \n\nDrucker einschalten und mit Computer verbinden");
                info.showInfoDialog(true);
                info.Close();
            }

            // Set start output folder TMP
            output_path = Path.GetTempPath();
            image_filename = "scan2wia";
            // Set JPEG as default
            //comboBox1.SelectedIndex = 1;

            // delete old scan files
            DirectoryInfo dir = new DirectoryInfo(output_path);
            dir.EnumerateFiles("scan2wia*.jpeg").ToList().ForEach(f => f.Delete());

        }

        private Boolean ListScanners()
        {
            Boolean result = false;
            // Clear the ListBox.
            listBox1.Items.Clear();

            // Create a DeviceManager instance
            var deviceManager = new DeviceManager();

            // Loop through the list of devices and add the name to the listbox
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                // Add the device only if it's a scanner
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }

                // Add the Scanner device to the listbox (the entire DeviceInfos object)
                // Important: we store an object of type scanner (which ToString method returns the name of the scanner)
                listBox1.Items.Add(
                    new Scanner(deviceManager.DeviceInfos[i])
                );
            }

            if (deviceManager.DeviceInfos.Count == 0)
            {
                btnListDevices.Enabled = true;
            }
            else
            {
                btnListDevices.Enabled = false;
                timer1.Enabled = false;
                result = true;
            }
            return (result);
        }

        private void TriggerScan()
        {
            Console.WriteLine("Image succesfully scanned");
        }

        public void StartScanning()
        {
            Scanner device = null;

            this.Invoke(new MethodInvoker(delegate ()
            {
                //device = listBox1.SelectedItem as Scanner;
                if (listBox1.Items.Count > 0) device = listBox1.Items[0] as Scanner;
            }));

            if (listBox1.Items.Count == 0)
            {
                //MessageBox.Show("Bitte Scanner einschalten", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            device.setColorMode(color_mode);

            ImageFile image = new ImageFile();
            string imageExtension = "";

            this.Invoke(new MethodInvoker(delegate ()
            {

                image = device.ScanImage(WIA.FormatID.wiaFormatJPEG);
                imageExtension = ".jpeg";

                //switch (comboBox1.SelectedIndex)
                //{
                //    case 0:
                //        image = device.ScanImage(WIA.FormatID.wiaFormatPNG);
                //        imageExtension = ".png";
                //        break;
                //    case 1:
                //        image = device.ScanImage(WIA.FormatID.wiaFormatJPEG);
                //        imageExtension = ".jpeg";
                //        break;
                //    case 2:
                //        image = device.ScanImage(WIA.FormatID.wiaFormatBMP);
                //        imageExtension = ".bmp";
                //        break;
                //    case 3:
                //        image = device.ScanImage(WIA.FormatID.wiaFormatGIF);
                //        imageExtension = ".gif";
                //        break;
                //    case 4:
                //        image = device.ScanImage(WIA.FormatID.wiaFormatTIFF);
                //        imageExtension = ".tiff";
                //        break;
                //}
            }));

            //pictureBox1.Image = null;

            // Save the image
            document_image_path = Path.Combine(output_path, image_filename + imageExtension);

            if (File.Exists(document_image_path))
            {
                File.Delete(document_image_path);
            }
            
            image.SaveFile(document_image_path);

            pictureBox1.Image = new Bitmap(document_image_path);
        }


        private void btnEmail_Click(object sender, EventArgs e)
        {
            Boolean mail_send = false;
            InfoDialog info = new InfoDialog();
            InfoDialog infoMailSend = new InfoDialog();

            if ( String.IsNullOrEmpty(document_image_path))
            {
                info.InfoText("Bitte Dokument scannen");
                info.showInfoDialog(true);
                //MessageBox.Show("Bitte Dokument scannen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WebTest.sendmail_outlook mail = new WebTest.sendmail_outlook();

            info.InfoText("Sende Email an " + DestMailAddress);
            info.Show();
            mail_send = mail.sentOutlookMail(DestMailAddress, "Scanned Document", "Angefügtes Dokument beachten", document_image_path);

            Thread.Sleep(3000);
            info.Close();

            if (mail_send == true)
            {
                infoMailSend.InfoText("Email wurde gesendet\n\nEine Kopie befindet sich in GESENDETE ELEMENTE in Outlook");
                infoMailSend.showInfoDialog(true);
                infoMailSend.Close();
            }
            else
            {
                MessageBox.Show("Mail konnte nicht gesendet werden", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void btnScan_Click(object sender, EventArgs e)
        {
            if (ListScanners() == false)
            {
                timer1.Enabled = true;
                InfoDialog info = new InfoDialog();
                info.InfoText("Es wurde kein Scanner gefunden. \n\nDrucker einschalten und mit Computer verbinden");
                info.showInfoDialog(true);
                info.Close();
                return;
            }

            dt = DateTime.Now;
            string time = dt.ToString("yy-MM-dd HH-mm-ss");
            image_filename = "scan2wia " + time;
            Task.Factory.StartNew(StartScanning).ContinueWith(result => TriggerScan());
        }

        private void btnListDevices_Click(object sender, EventArgs e)
        {
            if (ListScanners() == false)
            {
                timer1.Enabled = true;
                InfoDialog info = new InfoDialog();
                info.InfoText("Es wurde kein Scanner gefunden. \n\nDrucker einschalten und mit Computer verbinden");
                info.showInfoDialog(true);
                info.Close();
            }
        }

        private void btnSetMailAddress_Click(object sender, EventArgs e)
        {
            EmailAdresse mailAdress = new EmailAdresse();
            DialogResult result = new DialogResult();

            mailAdress.setAddress(DestMailAddress);
            result = mailAdress.showDialogBox();

            if(result == DialogResult.OK)
            {
                DestMailAddress = mailAdress.getAddress();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbScanColored_CheckedChanged(object sender, EventArgs e)
        {
            if (cbScanColored.Checked == true)
            { color_mode = 1; }
            else
            { color_mode = 4; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ListScanners() == true)
            { 
                timer1.Enabled = false;
                InfoDialog info = new InfoDialog();
                info.InfoText ("Scanner wurde gefunden. Jetzt Dokument scannen.");
                info.showInfoDialog(true);
                info.Close();
            }
        }
    }
}
