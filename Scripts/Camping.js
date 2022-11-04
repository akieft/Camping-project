
var GET = [];
window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (a, name, value) { GET[name] = value; });

var dateToSet = "";

if (!("date" in GET && GET["date"] != "")) {
    var d = new Date();
    dateToSet = d.getFullYear() + "-" + d.getMonth() + "-" + d.getDay();
    console.log("Didn't find GET date, setting to " + dateToSet);
} else {
    dateToSet = GET["date"];
}

console.log("Setting date to " + dateToSet);
document.querySelector("#start-date-input").value = dateToSet;

function onStartDateChange(obj, event) {
    window.location = "/Reservation/Create?date=" + obj.value;
}

