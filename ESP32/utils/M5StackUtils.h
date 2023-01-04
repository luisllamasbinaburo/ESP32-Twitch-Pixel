#pragma once

// Connects to WIFI
void    WIFI_Connect() {
	int i = 0;
	// Connect to WiFi network
	Serial.println();
	Serial.println();
	Serial.printf("Connecting to WIFI: %s\n", SSID.c_str());

	WiFi.mode(WIFI_STA);
	WiFi.begin(SSID, PASS);

	while (WiFi.status() != WL_CONNECTED) {
		delay(100);
		Serial.print(".");
		i++;
		if (i > 15) {
			i = 0;
			Serial.printf("\nConnecting to WIFI: %s\n", SSID.c_str());
		}
	}

	Serial.println("");
	Serial.println("WiFi connected");

	// Start the server
	server.begin();

	// Print the IP address
	Serial.print("Use this URL : ");
	Serial.print("http://");
	Serial.print(WiFi.localIP());
	Serial.println("/");
}