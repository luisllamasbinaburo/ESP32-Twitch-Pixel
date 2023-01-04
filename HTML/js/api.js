function onConnect() {
    var options = {
        qos: 0,
        onSuccess: onSubSuccess,
        onFailure: onSubFailure
    };
    client.subscribe('twitch/pixel/clear', options);
    client.subscribe('twitch/pixel/set', options);
}

function onFailure(message) {
    console.log(message)
}

function onConnectionLost(responseObject) {
    if (responseObject.errorCode !== 0) {
        console.log("onConnectionLost:" + responseObject.errorMessage);
    }
}

function onMessageArrived(message) {   
    let json = JSON.parse(message.payloadString);
    let command = json.command;

    if(command === "clear")
    {
        clearCanvas();
    }
    else if(command === "set")
    {
        let x = json.x;
        let y = json.y;
        let color = json.color;
        drawPixel(x, y, color);
    }
}

function onSubFailure(message) {
    console.log(message)
}

function onSubSuccess(message) {
    console.log(message)
}