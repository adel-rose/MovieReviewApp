async function submitForm() {
    const genre = {
        name: document.getElementById('Name').value
    };

    const response = await fetch('https://your-api-url.com/genres', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(genre)
    });

    if (response.ok) {
        window.location.href = '/Genre/Index'; // redirect on success
    } else {
        // show error
    }
}