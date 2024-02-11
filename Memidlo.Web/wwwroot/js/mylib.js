
const buttonSearchElement = document.getElementById('buttonSearch');
const searchTagElement = document.getElementById('searchTag');

buttonSearchElement.addEventListener('click', function () {
    if (searchTagElement.value == "") {
        alert("Nie podano żadnego tagu");
    } else {
        window.open("/tags/" + searchTagElement.value);
    }
})
