async function submitForm() {
    const genre = {
        Genre: document.getElementById('Name').value
    };

    // using a promise, e.g fetch, that already returns a promise
    // here the data contained in the promise is extracted if .then() was executed, with the use of async await
    const response = await fetch('http://localhost:5180/api/genre/create', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(genre)
    });

    if (response.ok) {
        window.location.href = '/genre/list';
    } else {
        document.getElementById('errorSection').innerText = "Could not create Genre, contact an admin.";
    }


    // using XMLHttp request that does not have default promise behavior, with a callback to handle response
    // post(genre, function(error, response) {
    //     if(error) {
    //         document.getElementById('errorSection').innerText = "Could not create Genre, contact an admin.";
    //         return;
    //     }
    //
    //     if(response.status == 200 || response.status == 201) {
    //         window.location.href = '/genre/list';
    //     } else {
    //         document.getElementById('errorSection').innerText = "Could not create Genre, contact an admin.";
    //     }
    // });

   
}

function post(genre, callback) {
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://localhost:5180/api/genre/create');
    xhr.setRequestHeader('Content-Type', 'application/json');

    xhr.onload = function() {
        callback(null, xhr);
    };

    xhr.onerror = function() {
        callback(new Error('Request failed'), null);
    };

    xhr.send(JSON.stringify(genre));
}

