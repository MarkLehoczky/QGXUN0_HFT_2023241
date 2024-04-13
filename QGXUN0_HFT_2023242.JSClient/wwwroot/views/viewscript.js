function setupSignalR(connections, endpoint, rowformat) {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:43016/api")
        .configureLogging(signalR.LogLevel.Information)
        .build();
		
    for (let conn of connections) {
        connection.on(conn, (user, message) => getdata(endpoint, rowformat));
    }

    connection.onclose(async () => {
        await start();
    });

    getdata(endpoint, rowformat);
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 1000);
    }
};

async function getdata(endpoint, rowformat) {
    await fetch('http://localhost:43016/' + endpoint)
        .then(response =>
            response.json())
        .then(response => {
            document.getElementById('items').innerHTML = "";
            return response;
        })
        .then(response =>
            response.forEach(item =>
                document.getElementById('items').innerHTML += rowformat(item))
    );
}