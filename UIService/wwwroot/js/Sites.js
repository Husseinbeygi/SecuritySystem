////$(document).bind("contextmenu", function (e) {
////    return false;
////});



function downloadFile(mimeType, base64String, fileName) {
    console.log("Hi!");
    var fileDataUrl = "data:" + mimeType + ";base64," + base64String;
    fetch(base64String)
        .then(response => response.blob())
        .then(blob => {

            //create a link
            var link = window.document.createElement("a");
            link.href = window.URL.createObjectURL(blob, { type: mimeType });
            link.download = fileName;

            //add, click and remove
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        });
}