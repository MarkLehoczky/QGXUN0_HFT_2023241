/*
let endpoint = ""
let objectID = ""
let objectHighlight = ""
let itemformat = null

function setup(target, id, highlight, format) {
    endpoint = target
    objectID = id
    objectHighlight = highlight
    itemformat = format
}


function createInit() {
    let id = ['create', 'form']
    let form = loadForm(id)
    customCreateInit();
    form.style.display = 'flex'





    // Sets al form to invisible
    for (let form of document.forms) {
        form.style.display = "none"
    }

    // Selects the form and clears the values
    for (let form of document.forms) {
        if (form.id.includes('create') && form.id.includes('form')) {
            for (let child of form) {
                if (child.type != 'submit') {
                    child.value = ""
                }
            }

            return f;
        }
    }
}

function readInit() {
    let init = null

    for (let f of document.forms) {
        f.style.display = "none"
    }

    for (let f of document.forms) {
        if (f.id.toLowerCase().includes('read') && f.id.toLowerCase().includes('selector')) {
            for (let c of f) {
                if (c.type.toLowerCase().includes('select')) {
                    c.innerHTML = '<option selected disabled hidden value="">None</option>';
                    fetch("http://localhost:43016/" + endpoint)
                        .then(response => response.json())
                        .then(response => response.forEach(item => c.innerHTML += `<option value="${item[objectID]}">${item[objectHighlight]}</option>`))
                }
            }

            init = f
            break
        }
    }

    customDeleteInit();

    init.style.display = "flex"
}

function updateInit() {
    let init = null

    for (let f of document.forms) {
        f.style.display = "none"
    }

    for (let f of document.forms) {
        if (f.id.toLowerCase().includes('update') && f.id.toLowerCase().includes('selector')) {
            for (let c of f) {
                if (c.type.toLowerCase().includes('select')) {
                    c.innerHTML = '<option selected disabled hidden value="">None</option>';
                    fetch("http://localhost:43016/" + endpoint)
                        .then(response => response.json())
                        .then(response => response.forEach(item => c.innerHTML += `<option value="${item[objectID]}">${item[objectHighlight]}</option>`))
                }
            }

            init = f
            break
        }
    }

    customDeleteInit();

    init.style.display = "flex"
}

function deleteInit() {
    let init = null

    for (let f of document.forms) {
        f.style.display = "none"
    }

    for (let f of document.forms) {
        if (f.id.toLowerCase().includes('delete') && f.id.toLowerCase().includes('selector')) {
            for (let c of f) {
                if (c.type.toLowerCase().includes('select')) {
                    c.innerHTML = '<option selected disabled hidden value="">None</option>';
                    fetch("http://localhost:43016/" + endpoint)
                        .then(response => response.json())
                        .then(response => response.forEach(item => c.innerHTML += `<option value="${item[objectID]}">${item[objectHighlight]}</option>`))
                }
            }

            init = f
            break
        }
    }

    customDeleteInit();

    init.style.display = "flex"
}

function readAllInit() {
    readAllAction(null)
}


function readSelected(event) {
    readAction(event)
}

function updateSelected(event) {
    let id = JSON.parse(JSON.stringify(event[0].value));

    for (let f of document.forms) {
        f.style.display = "none"
    }

    fetch("http://localhost:43016/" + endpoint + '/' + id)
        .then(response => response.json())
        .then(response => JSON.parse(JSON.stringify(response)))
        .then(item => {
            for (let f of document.forms) {
                if (f.id.toLowerCase().includes('update') && f.id.toLowerCase().includes('form')) {
                    for (let c of f) {
                        if (c.type != 'submit') {
                            c.value = item[c.name]
                        }
                    }

                    return f
                }
            }
        })
        .then(form => {
            customUpdateInit(id)
            return form
        })
        .then(form => form.style.display = "flex")
}

function deleteSelected(event) {
    deleteAction(event)
}


function createAction(event) {
    let jsonInput = {}

    for (let i of event) {
        let val = JSON.parse(JSON.stringify(i.value))

        if (i.type !== 'submit') {
            if (i.type === 'text') {
                if (val == '') {
                    jsonInput[i.name] = !i.className.includes('nullable') ? "" : null
                }
                else {
                    jsonInput[i.name] = val
                }
            }
            else if (i.type === 'number') {
                if (val == '') {
                    jsonInput[i.name] = !i.className.includes('nullable') ? "0" : null
                }
                else {
                    jsonInput[i.name] = val
                }
            }
            else if (i.type === 'select-one') {
                if (val == '') {
                    jsonInput[i.name] = !i.className.includes('nullable') ? "0" : null
                }
                else {
                    jsonInput[i.name] = val
                }
            }
        }
    }

    console.log('Create: ' + jsonInput)

    fetch("http://localhost:43016/" + endpoint, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(jsonInput)
    })
        .then(_ => {
            for (let f of document.forms) {
                f.style.display = "none"
            }
        })
}

function readAction(event) {
    let id = JSON.parse(JSON.stringify(event[0].value));

    for (let f of document.forms) {
        f.style.display = "none"
    }

    fetch("http://localhost:43016/" + endpoint + '/' + id)
        .then(response => response.json())
        .then(response => JSON.parse(JSON.stringify(response)))
        .then(item => {
            document.getElementById('single').innerHTML = ''
            for (let k in item) {
            }

            for (let k in itemformat(item)) {
                document.getElementById('single').innerHTML += `<p class="description">${k}</p>`
                document.getElementById('single').innerHTML += `<p class="value">${item[k]}</p>`
            }
        })
        .then(_ => {
            for (let f of document.forms) {
                if (f.id.toLowerCase().includes("single")) {
                    f.style.display = "flex"
                    return
                }
            }
        })
}

function updateAction(event) {
    let jsonInput = jsonTemplate

    for (let i of event) {
        let val = JSON.parse(JSON.stringify(i.value))

        if (i.type !== 'submit') {
            if (i.type === 'text') {
                if (val == '') {
                    jsonInput[i.name] = !i.className.includes('nullable') ? "" : null
                }
                else {
                    jsonInput[i.name] = val
                }
            }
            else if (i.type === 'number') {
                if (val == '') {
                    jsonInput[i.name] = !i.className.includes('nullable') ? "0" : null
                }
                else {
                    jsonInput[i.name] = val
                }
            }
            else if (i.type === 'select-one') {
                if (val == '') {
                    jsonInput[i.name] = !i.className.includes('nullable') ? "0" : null
                }
                else {
                    jsonInput[i.name] = val
                }
            }
        }
    }

    console.log(jsonInput)

    fetch("http://localhost:43016/" + endpoint, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(jsonInput)
    })
        .then(_ => {
            for (let f of document.forms) {
                f.style.display = "none"
            }
        })
}

function deleteAction(event) {
    let id = JSON.parse(JSON.stringify(event[0].value));

    fetch("http://localhost:43016/" + endpoint + '/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: null
    })
        .then(_ => {
            for (let f of document.forms) {
                f.style.display = "none"
            }
        })
}

function readAllAction(event) {
    for (let f of document.forms) {
        f.style.display = "none"
    }

    fetch("http://localhost:43016/" + endpoint)
        .then(response => response.json())
        .then(response => JSON.parse(JSON.stringify(response)))
        .then(items => {
            document.getElementById("multiple").innerHTML = ""
            items.forEach(item => {

                document.getElementById('multiple').innerHTML += tableformat(item)
            })
        })
        .then(_ => {
            for (let f of document.forms) {
                if (f.id.toLowerCase().includes("list")) {
                    f.style.display = "flex"
                    return
                }
            }
        })
}
*/



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
    let ids = ['create', 'form'];
    let form = getForm(ids);
    emptyForm(form)
    try { customCreateInit(); }
    catch (error) { console.warn(error); }
    finally { form.style.display = 'flex'; }
}

function readInit() {
    let ids = ['read', 'selector'];
    let form = getForm(ids);
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
    let ids = ['update', 'selector'];
    let form = getForm(ids);
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
    let ids = ['delete', 'selector'];
    let form = getForm(ids);
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
        .then(response => {
            return fillForm(getForm(['update', 'form']), JSON.parse(JSON.stringify(response)))
        })
        .then(form => {
            form.style.display = 'flex'
        });
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
        //.then(response => JSON.parse(JSON.stringify(response)))
        //.then(item => { /* TODO */ })
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
