var marker = null;

function initMap() {

    var infowindow = new google.maps.InfoWindow();

    var map = new google.maps.Map(document.getElementById("map"), {
        zoom: 16,
        center: { lat: 43.316872, lng: 21.894501 },
        disableDefaultUI: true,
        draggable: true
    });

    map.setOptions({ minZoom: 8, maxZoom: 16 });

    marker = new google.maps.Marker({ map: map });

    google.maps.event.addListener(map, "click", function (event) {
        var latitude = event.latLng.lat();
        var longitude = event.latLng.lng();

        marker.setPosition(event.latLng);

        map.panTo(new google.maps.LatLng(latitude, longitude));
    });

    marker.addListener("click", function () {
        infowindow.open(map, marker);

    });

    buttonsForm = '<input id="chooseButton" type="file" value="Izaberi slike" multiple>'
        + '<button class="btn btn-secondary" id="submitButton" onclick="SubmitButtonClick()">Dodaj lokaciju</button>';

    infowindow.setContent(buttonsForm);
    infowindow.open(map, marker);
}

function SubmitButtonClick() {
    var formData = new FormData();

    console.log(marker.position.lat());

    formData.append("lat", marker.position.lat());
    formData.append("lng", marker.position.lng());

    for (let i = 0; i < $("#chooseButton")[0].files.length; i++) {
        formData.append("lokacijaImages", $("#chooseButton")[0].files[i]);
    }

    $.ajax({
        url: 'Mapa/DodajLokaciju',
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData
    })
        .done(function(){
            alert('Uspesno ste dodali lokaciju');
        })
}