class Connector {
    constructor(connectionPoints, endpoint, objectMainProperty, formatObject) {
        this.endpoint = endpoint;
        this.objectMainProperty = objectMainProperty;
        this.formatObject = formatObject;

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:43016/api")
            .configureLogging(signalR.LogLevel.Information)
            .build();

        connectionPoints.forEach(point => this.connection.on(point, () => this.update()));
        this.connection.onclose(async () => await this.#connect());

        this.#connect();
        this.update();
    }

    async update() {
        await fetch(`http://localhost:43016/${this.endpoint}`)
            .then(response => response.json())
            .then(items => {
                let list = document.getElementById('list');
                let formattedItems = items.map(i => this.formatObject(i));
                let columns = Object.keys(formattedItems[0]);

                list.innerHTML = `<thead><tr>${columns.map(c => `<th>${c}</th>`).join('')}</tr></thead>`;
                list.innerHTML += `<tbody>${formattedItems.map(i => `<tr>${columns.map(c => `<td ${this.objectMainProperty == c ? 'class="bold"' : 'class="center"'}>${i[c]}</td>`).join('')}</tr>`).join('')}</tbody>`;
            });
    }

    async #connect() {
        try {
            await this.connection.start();
            console.log("SignalR Connected.");
        } catch (error) {
            console.error(error);
            setTimeout(this.#connect(), 1000);
        }
    }
}