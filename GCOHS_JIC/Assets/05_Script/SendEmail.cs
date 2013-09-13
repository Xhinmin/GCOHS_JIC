using UnityEngine;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

/// <summary>
/// 寄信Maill的功能
/// </summary>
public class SendEmail : MonoBehaviour
{
    public void RunSendEmail(string msg = "測試內容", string mysubject = "測試主旨標題", string address = "dk781020@hotmail.com")
    {
        //Mail 內容設定(未完成)
        MailMessage message = new MailMessage("dk781020@hotmail.com", address);//MailMessage(寄信者, 收信者)
        message.IsBodyHtml = true;

        message.SubjectEncoding = Encoding.UTF8;//標題編碼
        message.BodyEncoding = Encoding.UTF8;//內容編碼

        message.Subject = mysubject;//E-mail主旨
        message.Body = msg;//E-mail內容

        Attachment attachment = new Attachment("ScreenCapture.png");//<-這是附件部分~先用附件的物件把路徑指定進去~
        message.Attachments.Add(attachment);//<-郵件訊息中加入附件

        //mail server 內容設定
        SmtpClient smtpClient;
        smtpClient = new SmtpClient("smtp.gmail.com", 587);//gmail smtp設定
        smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential("hahamiror@gmail.com", "hahamiror123");//gmail 帳密
        smtpClient.EnableSsl = true;//打開ssl

        //設定安全機制(必須設定否則無法發送)
        ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

        //完成寄信後的callback function
        smtpClient.SendCompleted += this.smtp_SendCompleted;

        //寄送mail
        smtpClient.SendAsync(message, "Send");//寄送
    }

    //完成寄信後的callback function
    void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        print(e.Error);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.RunSendEmail();
        }
    }
}
