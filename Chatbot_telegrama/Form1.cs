using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Chatbot_telegrama
{
    public partial class Form1 : Form
    {
        ITelegramBotClient botClient;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            botClient = new TelegramBotClient("2116338351:AAGoMTbdDQG2ANvjBOnhKiDHtZ5IENQS3bs");
            botClient.OnMessage += BotClient_OnMessage;
            var me = botClient.GetMeAsync().Result;
            rtbResult.Text += $"Hello, Boss ! I am user {me.Id}\nMy name is {me.FirstName}.";
            botClient.StartReceiving();
            button1.Enabled = false;
        }

        private async void BotClient_OnMessage(object sender, MessageEventArgs e)
        {
            var id = e.Message.From.Id;
            var test = e.Message.Text.ToString();
            label1.BeginInvoke(new Action(() =>
            {
                rtbResult.Text += $"\nReceived a text message in chat {e.Message.Chat.Id}.";
            }));

            label1.BeginInvoke(new Action(() =>
            {
                rtbResult.Text += $"\nYou said Open: {test} ";
            }));

            switch (test)
            {
                case "Hello":
                    await botClient.SendTextMessageAsync(id,
                         "\n<b>Bot-Group14</b>\n" +
                         "\nHello my friend" + "\nWhat can I do for you?",      //tôi có thể giúp gì cho bạn?
                         Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);
                    break;
                case "Hi":
                    await botClient.SendTextMessageAsync(id,
                         "\n<b>Bot-Group14</b>\n" +
                         "\nHello my friend" + "\nWhat can I do for you?",      //tôi có thể giúp gì cho bạn?
                         Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);
                    break;

                case "How are you?":
                    await botClient.SendTextMessageAsync(id,
                         "\n<b>Bot-Group14</b>\n" +
                         "\nIm good" +
                         "\nAnd you?",
                         Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);
                    break;

                case "I'm fine":
                    await botClient.SendTextMessageAsync(id,
                         "\n<b>Bot-Group14</b>\n" +
                         "\nGreat!",
                         Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);
                    break;
                case "I'm Ok":
                    await botClient.SendTextMessageAsync(id,
                         "\n<b>Bot-Group14</b>\n" +
                         "\nGreat!",
                         Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);
                    break;
                case "I'm tired":
                    await botClient.SendTextMessageAsync(id,
                         "\n<b>Bot-Group14</b>\n" +
                         "\nAre you unwell?!",
                         Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);
                    break;


                case "/notepad":

                    await botClient.SendTextMessageAsync(id,
                        "\n<b>Bot-Group14</b>\n" +
                        "\nOk! You need me to open Notepad.",
                       Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);

                    Process.Start("notepad");

                    break;

                case "/calc":

                    await botClient.SendTextMessageAsync(id,
                        "\n<b>Bot-Group14</b>\n" +
                        "\nOk! You need me to open Calculator",
                       Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);

                    Process.Start("calc");

                    break;

                case "/what_time_is_it":

                    label1.BeginInvoke(new Action(() =>
                    {
                        rtbResult.Text += $"\n Vietnam ‎(UTC+7)‎: {DateTime.Now.ToString()} ";
                    }));
                    await botClient.SendTextMessageAsync(id,
                        "\n<b>Bot-Group14</b>\n" +
                         DateTime.Now.ToString(),
                       Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);

                    break;

                case "/capturescreen":

                    await botClient.SendTextMessageAsync(id,
                        "\n<b>Bot-Group14</b>\n" +
                        "\nOk! I will take a screenshot",
                       Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);

                    CaptureMyScreen();
                    using (FileStream fs = System.IO.File.OpenRead("Capture.jpg"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(fs, "Capture.jpg");
                        var file = await botClient.SendDocumentAsync(e.Message.Chat, inputOnlineFile);
                    }

                    break;


                default:
                    await botClient.SendTextMessageAsync(id,
                        "\n<b>Bot-Group14</b>\n" +
                        "\nFunctions:" +
                        "\nCalculator: /calc " +
                        "\nNotepad: /notepad " +
                        "\nDate & Time: /what_time_is_it " +
                        "\nScreen capture:/capturescreen ",
                       Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);
                    break;
            }   
           
        }  
        
                private void CaptureMyScreen()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                bitmap.Save("Capture.jpg", ImageFormat.Jpeg);
            }
        } 
    }


}