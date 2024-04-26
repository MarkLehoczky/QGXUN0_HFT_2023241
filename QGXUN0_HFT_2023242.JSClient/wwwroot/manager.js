class Manager {
    url = "http://localhost:43016/";
    endpoint = "";
    objectID = "";
    objectMainProperty = "";
    formatObject = (_) => { console.warn("Object formatting is not set") };
    displayForm = document.getElementById("display");
    selectorForm = document.getElementById("selector");
    genericForm = document.getElementById("generic");

    constructor(endpoint, objectID, objectMainProperty, objectFormat) {
        this.endpoint = endpoint;
        this.objectID = objectID;
        this.objectMainProperty = objectMainProperty;
        this.formatObject = objectFormat;
    }


    create() {
        this._hideForms();
        this._resetDisplay();

        this.displayForm.onsubmit = () => this.#createSend();

        try { customCreate(); }
        catch (error) { console.warn(error); }
    }

    read() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#readSelected();

        try { customRead(); }
        catch (error) { console.warn(error); }
    }

    update() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#updateSelected();

        try { customUpdate(); }
        catch (error) { console.warn(error); }
    }

    delete() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#deleteSelected();

        try { customDelete(); }
        catch (error) { console.warn(error); }
    }

    readAll() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        try { customReadAll(); }
        catch (error) { console.warn(error); }

        fetch(this.url + this.endpoint, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(Array.from(items).map(c => c[this.objectMainProperty]).join('\n')));
    }


    #createSend() {
        let json = this._jsonDisplay();
        console.log('Create');
        console.log(json);
        this._hideForms();

        fetch(this.url + this.endpoint, this._HTTPRequestFormat('POST', json))
    }

    #readSelected() {
        this._hideForms();
        this._resetDisplay();
        fetch(this.url + this.endpoint + '/' + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillDisplay(item))
            .then(_ => this._readOnlyDisplay());
    }

    #updateSelected() {
        this._hideForms();
        this._resetDisplay();

        this.displayForm.onsubmit = () => this.#updateSend();

        fetch(this.url + this.endpoint + '/' + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillDisplay(item));
    }

    #updateSend() {
        let json = this._jsonDisplay();
        console.log('Update');
        console.log(json);
        this._hideForms();

        fetch(this.url + this.endpoint, this._HTTPRequestFormat('PUT', json))
    }

    #deleteSelected() {
        console.log('Delete ID: ' + this.selectorForm[0].value);
        this._hideForms();

        fetch(this.url + this.endpoint + '/' + this.selectorForm[0].value, this._HTTPRequestFormat('DELETE'))
    }


    _hideForms() {
        this.displayForm.style.display = 'none';
        this.selectorForm.style.display = 'none';
        this.genericForm.style.display = 'none';
    }


    _resetDisplay() {
        Array.from(this.displayForm.elements).forEach(child => {
            child.readOnly = false;
            child.value = child.type == 'submit' ? child.value : '';
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
        for (let val in this.formatObject(values)) {
            for (let child of this.displayForm) {
                if (child.name.toLowerCase() == val.toLowerCase()) {
                    child.value = this.formatObject(values)[val];
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


    async _resetSelector() {
        this.selectorForm.lastElementChild.hidden = false;
        this.selectorForm[0].multiple = false;
        this.selectorForm[0].innerHTML = '<option selected disabled hidden value=""></option>';
        await fetch(this.url + this.endpoint)
            .then(response => response.json())
            .then(response => response.forEach(item => this.selectorForm[0].innerHTML += `<option value="${item[this.objectID]}">${item[this.objectMainProperty]}</option>`))
            .then(_ => this.selectorForm.style.display = 'flex');
    }

    _resetEmptySelector() {
        this.selectorForm.lastElementChild.hidden = false;
        this.selectorForm[0].multiple = false;
        this.selectorForm[0].innerHTML = '<option selected disabled hidden value=""></option>';
        this.selectorForm.style.display = 'flex';
    }

    _fillSelector(values) {
        this.selectorForm[0].innerHTML = '<option selected disabled hidden value=""></option>';

        Array.from(values).forEach(item => {
            this.selectorForm[0].innerHTML += `<option value="${item[1]}">${item[0]}</option>`
        });
    }


    _resetGeneric() {
        this.genericForm.lastElementChild.hidden = false;
        this.genericForm.firstElementChild.readOnly = false;
        this.genericForm.firstElementChild.value = '';
        this.genericForm.style.display = 'flex'
    }

    _readOnlyGeneric() {
        this.genericForm.lastElementChild.hidden = true;
        this.genericForm.firstElementChild.readOnly = true;
    }

    _fillGeneric(value) {
        this.genericForm.firstElementChild.value = value;
    }


    _HTTPRequestFormat(httpMethod, httpBody) {
        return {
            method: httpMethod.toUpperCase(),
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(httpBody)
        }
    }
}



class AuthorManager extends Manager {
    constructor(endpoint, objectID, objectMainProperty, objectFormat) {
        super(endpoint, objectID, objectMainProperty, objectFormat);
    }


    highestRated() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/HighestRated", this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillGeneric(JSON.stringify(this.formatObject(item.value), null, 2)));
    }

    lowestRated() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/LowestRated", this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillGeneric(JSON.stringify(this.formatObject(item.value), null, 2)));
    }

    series() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#seriesSelected();
    }

    selectBook() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#selectBook_AuthorSelected();
    }


    #seriesSelected() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/" + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(author => fetch(this.url + this.endpoint + "/Series", this._HTTPRequestFormat('POST', author))
                .then(response => response.json())
                .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => i.CollectionName), null, 2))));
    }

    #selectBook_AuthorSelected() {
        let authorID = this.selectorForm[0].value;

        this._hideForms();
        this._resetEmptySelector();
        this._fillSelector([
            ['Most Expensive', 0],
            ['Least Expensive', 1],
            ['Highest Rated', 2],
            ['Lowest Rated', 3]
        ]);

        this.selectorForm.onsubmit = () => this.#selectBook_AuthorAndFilterSelected(authorID);
    }

    #selectBook_AuthorAndFilterSelected(authorID) {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/" + authorID, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(author => fetch(this.url + this.endpoint + "/SelectBook", this._HTTPRequestFormat('POST', { "Item1": author, "Item2": parseInt(this.selectorForm[0].value) }))
                .then(response => response.json())
                .then(item => this._fillGeneric(JSON.stringify(item.Title, null, 2))));
    }
}



class BookManager extends Manager {
    constructor(endpoint, objectID, objectMainProperty, objectFormat) {
        super(endpoint, objectID, objectMainProperty, objectFormat);
    }


    addAuthors() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#addAuthors_BookSelected();
    }

    removeAuthors() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#removeAuthors_BookSelected();
    }

    inYear() {
        this._hideForms();
        this._resetGeneric();

        this.genericForm.onsubmit = () => this.#inYearSelected();
    }

    betweenYears() {
        this._hideForms();
        this._resetGeneric();

        this.genericForm.onsubmit = () => this.#betweenYearsSelected();
    }

    titleContains() {
        this._hideForms();
        this._resetGeneric();

        this.genericForm.onsubmit = () => this.#titleContainsSelected();
    }

    select() {
        this._hideForms();
        this._resetEmptySelector();
        this._fillSelector([
            ['Most Expensive', 0],
            ['Least Expensive', 1],
            ['Highest Rated', 2],
            ['Lowest Rated', 3]
        ]);

        this.selectorForm.onsubmit = () => this.#selectSelected();
    }


    #addAuthors_BookSelected() {
        let bookID = this.selectorForm[0].value;

        this._hideForms();
        this._resetEmptySelector();
        this.selectorForm[0].multiple = true;

        fetch(this.url + 'Author', this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillSelector(Array.from(items).map(i => [i.AuthorName, i.AuthorID])));

        this.selectorForm.onsubmit = () => this.#addAuthors_BookAndAuthorsSelected(bookID);
    }

    #addAuthors_BookAndAuthorsSelected(bookID) {
        this._hideForms();

        fetch(this.url + this.endpoint + "/" + bookID, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(book =>
                Promise.all(
                    Array.from(this.selectorForm[0].selectedOptions).map(v =>
                        fetch(this.url + `Author/${v.value}`, this._HTTPRequestFormat('GET'))
                            .then(r => r.json())))
                    .then(authors =>
                        fetch(this.url + this.endpoint + "/AddAuthors/array", this._HTTPRequestFormat('PUT',
                            {
                                "Item1": book,
                                "Item2": authors
                            }
                        ))));
    }

    #removeAuthors_BookSelected() {
        let bookID = this.selectorForm[0].value;

        this._hideForms();
        this._resetEmptySelector();
        this.selectorForm[0].multiple = true;

        fetch(this.url + this.endpoint + "/" + bookID, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillSelector(Array.from(item["authors"]).map(i => [i.AuthorName, i.AuthorID])));

        this.selectorForm.onsubmit = () => this.#removeAuthors_BookAndAuthorsSelected(bookID);
    }

    #removeAuthors_BookAndAuthorsSelected(bookID) {
        this._hideForms();

        fetch(this.url + this.endpoint + "/" + bookID, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(book =>
                Promise.all(
                    Array.from(this.selectorForm[0].selectedOptions).map(v =>
                        fetch(this.url + `Author/${v.value}`, this._HTTPRequestFormat('GET'))
                            .then(r => r.json())))
                    .then(authors =>
                        fetch(this.url + this.endpoint + "/RemoveAuthors/array", this._HTTPRequestFormat('PUT',
                            {
                                "Item1": book,
                                "Item2": authors
                            }
                        ))));
    }

    #inYearSelected() {
        let value = this.genericForm.firstElementChild.value;
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + `/InYear?year=${value}`, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => this.formatObject(i)), null, 2)));
    }

    #betweenYearsSelected() {
        let values = this.genericForm.firstElementChild.value.split('\n');
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + `/BetweenYears?min=${values[0]}&max=${values[1]}`, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => this.formatObject(i)), null, 2)));
    }

    #titleContainsSelected() {
        let value = this.genericForm.firstElementChild.value.split('\n')
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/TitleContains/array", this._HTTPRequestFormat('POST', value))
            .then(response => response.json())
            .then(titles => this._fillGeneric(JSON.stringify(titles, null, 2)));
    }

    #selectSelected() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/Select", this._HTTPRequestFormat('POST', parseInt(this.selectorForm[0].value)))
            .then(response => response.json())
            .then(item => this._fillGeneric(JSON.stringify(this.formatObject(item), null, 2)));
    }
}



class CollectionManager extends Manager {
    constructor(endpoint, objectID, objectMainProperty, objectFormat) {
        super(endpoint, objectID, objectMainProperty, objectFormat);
    }


    addBooks() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#addBooks_CollectionSelected();
    }

    removeBooks() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#removeBooks_CollectionSelected();
    }

    clearBooks() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#clearBooks_CollectionSelected();
    }

    series() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/Series", this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => this.formatObject(i)), null, 2)));
    }

    nonSeries() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/NonSeries", this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => this.formatObject(i)), null, 2)));
    }

    inYear() {
        this._hideForms();
        this._resetGeneric();

        this.genericForm.onsubmit = () => this.#inYearSelected();
    }

    betweenYears() {
        this._hideForms();
        this._resetGeneric();

        this.genericForm.onsubmit = () => this.#betweenYearsSelected();
    }

    price() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#priceSelected();
    }

    rating() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#ratingSelected();
    }

    select() {
        this._hideForms();
        this._resetEmptySelector();
        this._fillSelector([
            ['Most Expensive', 0],
            ['Least Expensive', 1],
            ['Highest Rated', 2],
            ['Lowest Rated', 3]
        ]);

        this.selectorForm.onsubmit = () => this.#select_BookFilterSelected();

    }

    selectBook() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#selectBook_CollectionSelected();
    }


    #addBooks_CollectionSelected() {
        let collectionID = this.selectorForm[0].value;

        this._hideForms();
        this._resetEmptySelector();
        this.selectorForm[0].multiple = true;

        fetch(this.url + 'Book', this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillSelector(Array.from(items).map(i => [i.Title, i.BookID])));

        this.selectorForm.onsubmit = () => this.#addBooks_CollectionAndBooksSelected(collectionID);
    }

    #addBooks_CollectionAndBooksSelected(collectionID) {
        this._hideForms();

        fetch(this.url + this.endpoint + "/" + collectionID, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(collection =>
                Promise.all(
                    Array.from(this.selectorForm[0].selectedOptions).map(v =>
                        fetch(this.url + `Book/${v.value}`, this._HTTPRequestFormat('GET'))
                            .then(r => r.json())))
                    .then(books =>
                        fetch(this.url + this.endpoint + "/AddBooks/array", this._HTTPRequestFormat('PUT',
                            {
                                "Item1": collection,
                                "Item2": books
                            }
                        ))));
    }

    #removeBooks_CollectionSelected() {
        let collectionID = this.selectorForm[0].value;

        this._hideForms();
        this._resetEmptySelector();
        this.selectorForm[0].multiple = true;

        fetch(this.url + this.endpoint + "/" + collectionID, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillSelector(Array.from(item["books"]).map(i => [i.Title, i.BookID])));

        this.selectorForm.onsubmit = () => this.#removeBooks_CollectionAndBooksSelected(collectionID);
    }

    #removeBooks_CollectionAndBooksSelected(collectionID) {
        this._hideForms();

        fetch(this.url + this.endpoint + "/" + collectionID, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(collection =>
                Promise.all(
                    Array.from(this.selectorForm[0].selectedOptions).map(v =>
                        fetch(this.url + `Book/${v.value}`, this._HTTPRequestFormat('GET'))
                            .then(r => r.json())))
                    .then(books =>
                        fetch(this.url + this.endpoint + "/RemoveBooks/array", this._HTTPRequestFormat('PUT',
                            {
                                "Item1": collection,
                                "Item2": books
                            }
                        ))));
    }

    #clearBooks_CollectionSelected() {
        this._hideForms();

        fetch(this.url + this.endpoint + "/" + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => fetch(this.url + this.endpoint + "/ClearBooks", this._HTTPRequestFormat('PUT', item)));
    }

    #inYearSelected() {
        let value = this.genericForm.firstElementChild.value;
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + `/InYear?year=${value}`, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => this.formatObject(i)), null, 2)));
    }

    #betweenYearsSelected() {
        let values = this.genericForm.firstElementChild.value.split('\n');
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + `/BetweenYears?min=${values[0]}&max=${values[1]}`, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => this.formatObject(i)), null, 2)));
    }

    #priceSelected() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/" + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item =>
                fetch(this.url + this.endpoint + "/Price", this._HTTPRequestFormat('POST', item))
                    .then(response => response.json())
                    .then(v => this._fillGeneric(v)));
    }

    #ratingSelected() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/" + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item =>
                fetch(this.url + this.endpoint + "/Rating", this._HTTPRequestFormat('POST', item))
                    .then(response => response.json())
                    .then(v => this._fillGeneric(v)));
    }

    #select_BookFilterSelected() {
        let bookFilter = this.selectorForm[0].value;
        this._hideForms();
        this._resetEmptySelector();
        this._fillSelector([
            ['Collection', 0],
            ['Series', 1],
            ['Non series', 2]
        ]);

        this.selectorForm.onsubmit = () => this.#select_BookFilterAndCollectionFilterSelected(bookFilter);
    }

    #select_BookFilterAndCollectionFilterSelected(bookFilter) {
        let collectionFilter = this.selectorForm[0].value;
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/Select", this._HTTPRequestFormat('POST',
            {
                "Item1": parseInt(bookFilter),
                "Item2": parseInt(collectionFilter)
            }))
            .then(response => response.json())
            .then(item => this._fillGeneric(JSON.stringify(this.formatObject(item.value), null, 2)));
    }

    #selectBook_CollectionSelected() {
        let collectionID = this.selectorForm[0].value;
        this._hideForms();
        this._resetEmptySelector();
        this._fillSelector([
            ['Most Expensive', 0],
            ['Least Expensive', 1],
            ['Highest Rated', 2],
            ['Lowest Rated', 3]
        ]);

        this.selectorForm.onsubmit = () => this.#selectBook_CollectionAndBookFilterSelected(collectionID);
    }

    #selectBook_CollectionAndBookFilterSelected(collectionID) {
        let bookFilter = this.selectorForm[0].value;
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();
        fetch(this.url + this.endpoint + "/" + collectionID, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(collection =>
                fetch(this.url + this.endpoint + "/SelectBook", this._HTTPRequestFormat('POST',
                    {
                        "Item1": collection,
                        "Item2": parseInt(bookFilter)
                    }))
                    .then(response => response.json())
                    .then(item => this._fillGeneric(JSON.stringify(item, null, 2))));
    }
}



class PublisherManager extends Manager {
    constructor(endpoint, objectID, objectMainProperty, objectFormat) {
        super(endpoint, objectID, objectMainProperty, objectFormat);
    }


    series() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/Series", this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => this.formatObject(i)), null, 2)));
    }

    onlySeries() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/OnlySeries", this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => this.formatObject(i)), null, 2)));
    }

    highestRated() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/HighestRated", this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillGeneric(JSON.stringify(this.formatObject(item.value), null, 2)));
    }

    lowestRated() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/LowestRated", this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(item => this._fillGeneric(JSON.stringify(this.formatObject(item.value), null, 2)));
    }

    rating() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#ratingSelected();
    }

    authors() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#authorsSelected();
    }

    permanentAuthors() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/PermanentAuthors", this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => i.AuthorName), null, 2)));
    }

    permanentAuthorsOfPublisher() {
        this._hideForms();
        this._resetSelector();

        this.selectorForm.onsubmit = () => this.#permanentAuthorsOfPublisher_PublisherSelected();
    }


    #ratingSelected() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/" + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(publisher => fetch(this.url + this.endpoint + "/Rating", this._HTTPRequestFormat('POST', publisher))
                .then(response => response.json())
                .then(item => this._fillGeneric(JSON.stringify(item, null, 2))));
    }

    #authorsSelected() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/" + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(publisher => fetch(this.url + this.endpoint + "/Authors", this._HTTPRequestFormat('POST', publisher))
                .then(response => response.json())
                .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => i.AuthorName), null, 2))));
    }

    #permanentAuthorsOfPublisher_PublisherSelected() {
        this._hideForms();
        this._resetGeneric();
        this._readOnlyGeneric();

        fetch(this.url + this.endpoint + "/" + this.selectorForm[0].value, this._HTTPRequestFormat('GET'))
            .then(response => response.json())
            .then(publisher => fetch(this.url + this.endpoint + "/PermanentAuthors", this._HTTPRequestFormat('POST', publisher))
                .then(response => response.json())
                .then(items => this._fillGeneric(JSON.stringify(Array.from(items).map(i => i.AuthorName), null, 2))));
    }
}