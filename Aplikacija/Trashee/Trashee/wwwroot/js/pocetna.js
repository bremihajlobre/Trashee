/*popUp na stranici Pocetna*/

function openFormNotification() {
    document.getElementById("popUpNotification").style.display = "block";
    document.getElementById("createdNotification").style.height = "200px";
}

function closeFormNotification() {
    document.getElementById("popUpNotification").style.display = "none";
    document.getElementById("createdNotification").style.height = "450px";
}