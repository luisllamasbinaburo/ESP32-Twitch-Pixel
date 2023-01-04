namespace ESP32_Twitch_Pixel
{
    const int GRID_SIZE_X = 16;
    const int GRID_SIZE_Y = 16;

    public class BotTwitch
    {
        MqttClient clientMqtt;
        TwitchClient client;

        public BotTwitch()
        {
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            var webSocketClient = new WebSocketClient(clientOptions);

            var credentials = new ConnectionCredentials("you_twitch_username", "you_oauth");
            client = new TwitchClient(webSocketClient);
            client.Initialize(credentials, "channel_name");

            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            client.Connect();

            clientMqtt = new MqttClient("broker_ip");
            var clientId = Guid.NewGuid().ToString();
            clientMqtt.Connect(clientId);
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            if (e.Command.CommandText == "pixel" && e.Command.ArgumentsAsList.Count == 3)
            {
                string x = e.Command.ArgumentsAsList[0];
                string y = e.Command.ArgumentsAsList[1];
                string color = e.Command.ArgumentsAsList[2];

                bool valid_x = int.TryParse(x, out int x_position);
                bool valid_y = int.TryParse(y, out int y_position);
                bool valid_color = ColorMapper.TryParse(color, out string color_hex);

                if (valid_x && valid_y && valid_color)
                {
                   if (x_position >= 0 && x_position < GRID_SIZE_X &&
                        y_position >= 0 && y_position < GRID_SIZE_Y)
                    {
                        SendByMqtt_AddPixel(x_position, y_position, color_hex);
                    }
                }                
            }
            else if (e.Command.CommandText == "clear")
            {
                SendByMqtt_Clear();
            }
            else if (e.Command.CommandText == "help")
            {
                client.SendMessage(e.Command.ChatMessage.Channel, "DarkestRed - Red - LightRed - Orange - Yellow - Paleyellow - DarkGreen - Green - LightGreen - DarkTeal - Teal - LightTeal - DarkBlue - Blue - LightBlue - Indigo - Periwinkle - Lavender - DarkPurple - Purple - Palepurple - Magenta - Pink - LightPink - DarkBrown - Brown - Beige - Black - Darkgray - Gray - LightGray - White");
            }
        }

        private void SendByMqtt_Clear()
        {
            clientMqtt.Publish("twitch/pixel/clear", Encoding.UTF8.GetBytes("clear"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        private void SendByMqtt_AddPixel(int x_position, int y_position, string color)
        {
            dynamic product = new ExpandoObject();
            product.x = x_position;
            product.y = y_position;
            product.color = color;
            product.rgb565 = ColorUtils.Hex_to_RGB565(color);

            string json = JsonConvert.SerializeObject(product);
            clientMqtt.Publish("twitch/pixel/set", Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }
    }
}
