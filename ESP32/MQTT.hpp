#include <M5StickCPlus.h>

WiFiClient espClient;
PubSubClient mqttClient(espClient);

void ClearScreen()
{
	tftSprite.fillScreen(TFT_BLACK);
	tftSprite.pushSprite(0, 0);

	ledSprite.fillScreen(TFT_BLACK);
	sendToLed(ledSprite);
}

void DrawPixel(int32_t x, int32_t y, uint32_t color)
{
	tftSprite.fillRect(x * 8 + 6, y * 8 + 6, 8, 8, color);
	tftSprite.pushSprite(0, 0);

	ledSprite.drawPixel(x, y, color);
	sendToLed(ledSprite);
}

String GetPayloadContent(char* data, size_t len)
{
	String content = "";
	for (size_t i = 0; i < len; i++)
	{
		content.concat(data[i]);
	}
	return content;
}

void SuscribeMqtt()
{
	mqttClient.subscribe("twitch/pixel/clear");
	mqttClient.subscribe("twitch/pixel/set");
}

String content = "";
void OnMqttReceived(char* topic, byte* payload, unsigned int length)
{
	String topicStr = topic;
	content = GetPayloadContent((char*)payload, length);

	if (topicStr == "twitch/pixel/clear")
	{
		ClearScreen();
	}
	if (topicStr == "twitch/pixel/set")
	{
		StaticJsonDocument<200> doc;
		DeserializationError error = deserializeJson(doc, content);
		if (error) return;

		uint8_t x = doc["x"];
		uint8_t y = doc["y"];
		uint32_t color = doc["rgb565"];
		DrawPixel(x, y, color);
	}
}