using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace WebTest
{
    public class sendmail_outlook
    {

        public Boolean sentOutlookMail(string mailadress, string subject, string body, string file_name)
        {
            Boolean result = false;
            DateTime dt = DateTime.Now;
            string date = dt.ToString("dd.MM.yyyy hh:mm");

            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                // Set the properties of the mail item.
                mailItem.Subject = subject;
                mailItem.To = mailadress;
                mailItem.Body = dt + " " + body;
                mailItem.Attachments.Add(file_name, Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);

                // Send the email.
                mailItem.Send();
                Thread.Sleep(2000);
                result = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Email konnte nicht gesendet werden","Fehler",MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            return (result);
        }

    }
}
