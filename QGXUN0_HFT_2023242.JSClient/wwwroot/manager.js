class Manager {
    url = "http://localhost:43016/";
    endpoint = "";
    objectID = "";
    objectMainProperty = "";
    objectFormat = (_) => { console.warn("Object formatting is not set") };
    displayForm = document.getElementById("display");
    selectorForm = document.getElementById("selector");
    genericForm = document.getElementById("generic");

    constructor(endpoint, objectID, objectMainProperty, objectFormat) {
        this.endpoint = endpoint;
        this.objectID = objectID;
        this.objectMainProperty = objectMainProperty;
        this.objectFormat = objectFormat;
    }


    createInit() {
        this._hideForms();
        this._resetDisplay();

        this.displayForm.onsubmit = () => this.createAction();

        try { customCreateInit(); }
        catch (error) { console.warn(error); }
    }

    readInit() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.readSelected();

        try { customReadInit(); }
        catch (error) { console.warn(error); }
    }

    updateInit() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.updateSelected();

        try { customUpdateInit(); }
        catch (error) { console.warn(error); }
    }

    deleteInit() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.deleteSelected();

        try { customDeleteInit(); }
        catch (error) { console.warn(error); }
    }

    readAllInit() {
        this._hideForms();
        this._resetGeneric();

        this.readAllAction();

        try { customReadAllInit(); }
        catch (error) { console.warn(error); }
    }


    readSelected() {
        this._hideForms();
        this._resetDisplay();
        fetch(this.url + this.endpoint + '/' + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillDisplay(item))
            .then(_ => this._readOnlyDisplay());
    }

    updateSelected() {
        this._hideForms();
        this._resetDisplay();

        this.displayForm.onsubmit = () => this.updateAction();

        fetch(this.url + this.endpoint + '/' + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillDisplay(item));
    }

    deleteSelected() {
        this._hideForms();
        this.deleteSend(this.selectorForm[0].value);
    }


    createAction() {
        let json = this._jsonDisplay();
        console.log('Create');
        console.log(json);
        this._hideForms();
        this.createSend(json);
    }

    readAction() {
    }

    updateAction() {
        let json = this._jsonDisplay();
        console.log('Update');
        console.log(json);
        this._hideForms();
        this.updateSend(json);
    }

    deleteAction() {
    }

    readAllAction() {
        this.readAllSend(null);
    }


    createSend(info) {
        fetch(this.url + this.endpoint, this._HTTPRequestFormat('POST', info))
    }

    readSend(info) {
    }

    updateSend(info) {
        fetch(this.url + this.endpoint, this._HTTPRequestFormat('PUT', info))
    }

    deleteSend(info) {
        fetch(this.url + this.endpoint + '/' + info, this._HTTPRequestFormat('DELETE'))
    }

    readAllSend(info) {
        fetch(this.url + this.endpoint, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(Array.from(items).map(c => c[this.objectMainProperty]).join('\n')));
    }



    _hideForms() {
        this.displayForm.style.display = 'none';
        this.selectorForm.style.display = 'none';
        this.genericForm.style.display = 'none';
    }

    _resetDisplay() {
        Array.from(this.displayForm.elements).forEach(child => {
            child.readOnly = false;
            child.value = child.type == 'submit' ? child.type : '';
        });
        this.displayForm.style.display = 'flex';
        this.displayForm.lastElementChild.hidden = false;
    }

    _readOnlyDisplay() {
        Array.from(this.displayForm.elements).forEach(child => {
            child.readOnly = true;
        });
        this.displayForm.lastElementChild.hidden = true;
    }

    _fillDisplay(values) {
        for (let val in this.objectFormat(values)) {
            for (let child of this.displayForm) {
                if (child.name.toLowerCase() == val.toLowerCase()) {
                    child.value = this.objectFormat(values)[val];
                }
            }
        }
    }

    _jsonDisplay() {
        let json = {};

        Array.from(this.displayForm.elements).filter(child => child.disabled == false && child.type != 'submit').forEach(child => {
            json[child.name] = child.value != ''
                ? JSON.parse(JSON.stringify(child.value))
                : !child.className.includes('nullable')
                    ? child.type == 'number' || child.type == 'select-one'
                        ? -1
                        : ''
                    : null;
        });

        return json;
    }


    _resetSelector() {
        this.selectorForm.lastElementChild.hidden = false;
        this.selectorForm[0].innerHTML = '<option selected disabled hidden value=""></option>';
        fetch(this.url + this.endpoint)
            .then(response => response.json())
            .then(response => response.forEach(item => this.selectorForm[0].innerHTML += `<option value="${item[this.objectID]}">${item[this.objectMainProperty]}</option>`))
            .then(_ => this.selectorForm.style.display = 'flex');
    }

    _fillSelector(values, valueName, mainPropertyName) {
        this.selectorForm[0].innerHTML = '<option selected disabled hidden value=""></option>';

        values.forEach(item => this.selectorForm[0].innerHTML += `<option value="${item[valueName]}">${item[mainPropertyName]}</option>`);
        this.selectorForm.style.display = 'flex';
    }

    _readOnlySelector() {
        this.selectorForm.lastElementChild.hidden = true;
    }


    _resetGeneric() {
        this.genericForm.firstElementChild.value =  '';
        this.genericForm.style.display = 'flex'
    }

    _fillGeneric(value) {
        this.genericForm.firstElementChild.value = value;
    }


    _HTTPRequestFormat(httpMethod) {
        return {
            method: httpMethod.toUpperCase(),
            headers: { 'Content-Type': 'application/json' },
            body: null
        }
    }

    _HTTPRequestFormat(httpMethod, httpBody) {
        return {
            method: httpMethod.toUpperCase(),
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(httpBody)
        }
    }
}