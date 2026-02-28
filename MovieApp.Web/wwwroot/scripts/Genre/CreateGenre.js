async function submitForm() {
    const genre = {
        Genre: document.getElementById('Name').value
    };

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
}