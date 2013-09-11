using UnityEngine;
using System.Collections;
using System.Net.Mail;

public class SendEmail : MonoBehaviour
{
    public void RunSendEmail(string msg, string mysubject, string address)
    {
        MailMessage message = new MailMessage("dk781020@gmail.com", address);//MailMessage(寄信者, 收信者)
        message.IsBodyHtml = true;
        message.BodyEncoding = System.Text.Encoding.UTF8;//E-mail編碼
        message.Subject = mysubject;//E-mail主旨
        message.Body = msg;//E-mail內容

        SmtpClient smtpClient = new SmtpClient("127.0.0.1", 25);//設定E-mail Server和port
        smtpClient.Send(message);
    }

    // Use this for initialization
    void Start()
    {
        this.RunSendEmail("測試內容", "測試主旨標題", "xyz@yahoo.com.tw");//呼叫send_email函式測試
    }

    // Update is called once per frame
    void Update()
    {

    }
}
