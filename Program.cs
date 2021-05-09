using System;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Collections.Generic;

namespace FinderPhoneBot
{

    class Program
    {
        static TelegramBotClient BotFinder;

        static void Main(string[] args)
        {
            BotFinder = new TelegramBotClient("1600475929:AAEiDPlpsRjD2KXNERVyKX6kvFrUQF_CloI");

            BotFinder.OnMessage += BotOnMessageReceived;
            BotFinder.OnCallbackQuery += BotOnCallbackQueryReceived;

            User me = BotFinder.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);
            BotFinder.StartReceiving();

            Console.ReadLine();
            BotFinder.StopReceiving();  
        }

        

        private static async void BotOnCallbackQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} Ви натиснули на клавішу {buttonText}");

            if (buttonText == "Задня та передня камери")
            {
                _ = await BotFinder.SendTextMessageAsync(e.CallbackQuery.From.Id, "Задня та передня камери");
            }
            else if (buttonText == "Оперативна пам'ять")
            {

                _ = await BotFinder.SendTextMessageAsync(e.CallbackQuery.From.Id, "2 ");
            }
            else if (buttonText == "Вбудована пам'ять")
            {       
               _ = await BotFinder.SendTextMessageAsync(e.CallbackQuery.From.Id, "Код:" );            
            }
            else if (buttonText == "Ємність батареї")
            {

                _ = await BotFinder.SendTextMessageAsync(e.CallbackQuery.From.Id, "2 ");
            }
            else if (buttonText == "За ціною(До 10 000 грн.)")
            {

                _ = await BotFinder.SendTextMessageAsync(e.CallbackQuery.From.Id, "2 ");
            }
            else if (buttonText == "За ціною(Дорожче 10 000 грн.)")
            {

                _ = await BotFinder.SendTextMessageAsync(e.CallbackQuery.From.Id, "2 ");
            }

            try
            {
                await BotFinder.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Вы нажали кнопку {buttonText}");
            }
            catch { }
        }

        private static async void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Message message = e.Message;

            switch (message.Text)

            {
                case "/start":
                    string text = @"
/characteristic - вивід тем ";
                    await BotFinder.SendTextMessageAsync(message.From.Id, text);
                    break;
                case "/characteristic":
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                      {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(@"Задня та передня камери")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(@"Оперативна пам'ять")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(@"Вбудована пам'ять")
                        }
                        ,
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(@"Ємність батареї")
                        }
                        ,
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(@"За ціною(До 10 000 грн.)"),
                            InlineKeyboardButton.WithCallbackData(@"За ціною(Дорожче 10 000 грн.)")
                        }
                      }) ;
                    await BotFinder.SendTextMessageAsync(message.From.Id, "Виберіть характеристику", replyMarkup: inlineKeyboard);

                    break;
                default:
                    string no = "Введіть правильно команду!";
                    await BotFinder.SendTextMessageAsync(message.From.Id, no);
                    break;
            }
        }      
    }
}
