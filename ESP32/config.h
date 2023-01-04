const char* ssid     = "ssid";
const char* password = "password";
const char* hostname = "ESP32_1";

IPAddress ip(192, 168, 1, 201);
IPAddress gateway(192, 168, 1, 1);
IPAddress subnet(255, 255, 255, 0);

const char* MQTT_BROKER_ADRESS = "192.168.1.xxx";
const uint16_t MQTT_PORT = 1883;
const char* MQTT_CLIENT_NAME = "ESP32Client_2";