



function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            codeLatLng(position.coords.latitude, position.coords.longitude);
        });
    } else {
        alert("browser cannot find location");
    }
}

function showPosition(position) {



}










