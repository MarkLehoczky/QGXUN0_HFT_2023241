const url = "http://localhost:43016/";

let endpoint = "";
let objectID = "";
let objectHighlight = "";
let itemformat = _ => { };


function setup(target, id, highlight, format) {
    endpoint = target;
    objectID = id;
    objectHighlight = highlight;
    itemformat = format;
}


function createInit() {
    let form = getForm(['create', 'form']);
    emptyForm(form)
    try { customCreateInit(); }
    catch (error) { console.warn(error); }
    finally { form.style.display = 'flex'; }
}

function readInit() {
    let form = getForm(['read', 'selector']);
    for (let child of form) {
        if (child.id.includes('select')) {
            loadSelection(child);
        }
    }
    try { customReadInit(); }
    catch (error) { console.warn(error); }
    finally { form.style.display = 'flex'; }
}

function updateInit() {
    let form = getForm(['update', 'selector']);
    for (let child of form) {
        if (child.id.includes('select')) {
            loadSelection(child);
        }
    }
    try { customUpdateInit(); }
    catch (error) { console.warn(error); }
    finally { form.style.display = 'flex'; }
}

function deleteInit() {
    let form = getForm(['delete', 'selector']);
    for (let child of form) {
        if (child.id.includes('select')) {
            loadSelection(child);
        }
    }
    try { customDeleteInit(); }
    catch (error) { console.warn(error); }
    finally { form.style.display = 'flex'; }
}

function readAllInit() {
    form = getForm(['readall']);
    try { customReadAllInit(); }
    catch (error) { console.warn(error); }
    finally { readAllAction(form); }
}


function readSelected(info) {
    let id = JSON.parse(JSON.stringify(info[0].value));
    readAction(id);
}

function updateSelected(info) {
    let id = JSON.parse(JSON.stringify(info[0].value));

    fetch(url + endpoint + '/' + id, HTTPRequestFormat('GET'))
        .then(response => response.json())
        .then(response => fillForm(getForm(['update', 'form']), JSON.parse(JSON.stringify(response))))
        .then(form => form.style.display = 'flex');
}

function deleteSelected(info) {
    let id = JSON.parse(JSON.stringify(info[0].value));
    deleteAction(id);
}


function createAction(info) {
    let input = formJson(info);
    console.log(input);
    createSend(input);
}

function readAction(info) {
    unloadAllForm();
    readSend(info);
}

function updateAction(info) {
    let input = formJson(info);
    console.log(input);
    updateSend(input);
}

function deleteAction(info) {
    deleteSend(info);
}

function readAllAction(info) {
    let list = null;

    for (let child of info) {
        if (child.id.toLowerCase().includes('list')) {
            list = child;
        }
    }

    readAllSend(list);
    info.style.display = 'flex';
}


function createSend(info) {
    fetch(url + endpoint, HTTPRequestFormat('POST', info))
        .then(_ => unloadAllForm());
}

function readSend(info) {
    fetch(url + endpoint + '/' + info, HTTPRequestFormat('GET'))
        .then(response => response.json())
        .then(item => {
            let form = getForm(['read', 'form']);
            fillForm(form, itemformat(item))
            form.style.display = 'flex'
        });
}

function updateSend(info) {
    fetch(url + endpoint, HTTPRequestFormat('PUT', info))
        .then(_ => unloadAllForm());
}

function deleteSend(info) {
    fetch(url + endpoint + '/' + info, HTTPRequestFormat('DELETE'))
        .then(_ => unloadAllForm());
}

function readAllSend(info) {
    fetch(url + endpoint)
        .then(response => response.json())
        .then(response => response.forEach(item => info.value += item[objectHighlight] + '\n'));
}



function unloadAllForm() {
    for (let form of document.forms) {
        form.style.display = "none";
    }
}

function getForm(ids) {
    unloadAllForm();

    for (let form of document.forms) {
        if (ids.every(i => form.id.includes(i))) {
            return form;
        }
    }
}

function emptyForm(form) {
    for (let child of form) {
        if (child.type != 'submit') {
            child.value = "";
        }
    }

    return form;
}

function fillForm(form, values) {
    for (let val in values) {
        for (let child of form) {
            if (child.name.toLowerCase() == val.toLowerCase()) {
                child.value = values[val];
            }
        }
    }

    return form;
}

function formJson(form) {
    let jsonInput = {}

    for (let i of form) {
        let value = JSON.parse(JSON.stringify(i.value));

        if (i.type != 'submit') {
            if (value == '') {
                if (i.className.includes('nullable')) {
                    jsonInput[i.name] = null;
                }
                else if (i.type == 'number' || i.type == 'select-one') {
                    jsonInput[i.name] = -1;
                }
                else {
                    jsonInput[i.name] = '';
                }
            }
            else {
                jsonInput[i.name] = value;
            }
        }
    }

    return jsonInput;
}


function loadSelection(selectObject) {
    selectObject.innerHTML = '<option selected disabled hidden value="">None</option>';
    fetch(url + endpoint)
        .then(response => response.json())
        .then(response => response.forEach(item => selectObject.innerHTML += `<option value="${item[objectID]}">${item[objectHighlight]}</option>`));
}

function loadSelection(selectObject, selectedID) {
    selectObject.innerHTML = `<option ${selectedID == undefined || selectedID == null || selectedID == '' ? 'selected' : ''} disabled hidden value="">None</option>`;
    fetch(url + endpoint)
        .then(response => response.json())
        .then(response => response.forEach(item => selectObject.innerHTML += `<option ${item[objectID] == selectedID ? 'selected' : ''} value="${item[objectID]}">${item[objectHighlight]}</option>`));
}


function HTTPRequestFormat(httpMethod) {
    return {
        method: httpMethod.toUpperCase(),
        headers: { 'Content-Type': 'application/json' },
        body: null
    }
}

function HTTPRequestFormat(httpMethod, httpBody) {
    return {
        method: httpMethod.toUpperCase(),
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(httpBody)
    }
}
