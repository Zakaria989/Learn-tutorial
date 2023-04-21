// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function showDeleteButton(editButton) {
    var deleteButton = editButton.parentNode.nextElementSibling.firstChild;
    deleteButton.style.display = deleteButton.style.display === 'none' ? 'inline-block' : 'none';
}

var editMode = false;

function toggleEditMode(editbutton) {
    var tr = editbutton.parentNode.parentNode; // get the tr element containing the Edit button
    editMode = !editMode;
    var elements = tr.getElementsByClassName("editable"); // select only the cells within that tr element
    var saveButton = tr.querySelector("save");
    var editButton = tr.getElequerySelectormentById("edit");

    for (var i = 0; i < elements.length; i++) {
        var element = elements[i];
        if (editMode) {
            element.innerHTML = '<input value="' + element.innerHTML + '">';
            editButton.style.display = "none";
            saveButton.style.display = 'inline-block';
                        editbutton.innerHTML = "Save";
        } else {
            element.innerHTML = element.childNodes[0].value;
            saveButton.style.display = 'none';
            editButton.style.display = "inline-block";
                        editbutton.innerHTML = "Edit";
        }
    }
}













//function editValues(editButton) {
//    var weight = editButton.parentNode.parentNode.querySelector("#weight");
//    var bodyfat = editButton.parentNode.parentNode.querySelector("#bodyfat");
//    var date = editButton.parentNode.parentNode.querySelector("#date");

//    if (editButton.textContent === "Edit") {
//        // Change button text to "Save"
//        editButton.textContent =  Save";
//        editOrViewMode = true;
//        // Create input elements for weight, bodyfat, and date
//        var weightInput = document.createElement("input");
//        var dateInput = document.createElement("input");
//        var bodyfatInput = document.createElement("input");

//        // Store the original values in data attributes
//        weight.dataset.originalValue = weight.textContent;
//        bodyfat.dataset.originalValue = bodyfat.textContent;
//        date.dataset.originalValue = date.textContent;

//        weightInput.type = "number";
//        weightInput.name = "weight";

//        weightInput.value = weight.textContent;
//        dateInput.value = date.textContent;
//        bodyfatInput.value = bodyfat.textContent;

//        // Replace weight, bodyfat, and date td elements with input elements
//        weight.replaceWith(weightInput);
//        bodyfat.replaceWith(bodyfatInput);
//        date.replaceWith(dateInput);

//    } else {
//        // Change button text to "Edit"
//        editButton.textContent = "Edit";
//        editOrViewMode = false;
//        // Hide input elements and show td elements
//        weight.querySelector("input").hidden = true;
//        weight.textContent = weight.querySelector("input").value || weight.dataset.originalValue;

//        bodyfat.querySelector("input").hidden = true;
//        bodyfat.textContent = bodyfat.querySelector("input").value || bodyfat.dataset.originalValue;

//        date.querySelector("input").hidden = true;
//        date.textContent = date.querySelector("input").value || date.dataset.originalValue;

//        // Create new td elements with the updated values
//        var newWeight = document.createElement("td");
//        newWeight.id = "weight";
//        newWeight.textContent = weight.textContent;

//        var newBodyfat = document.createElement("td");
//        newBodyfat.id = "bodyfat";
//        newBodyfat.textContent = bodyfat.textContent;

//        var newDate = document.createElement("td");
//        newDate.id = "date";
//        newDate.textContent = date.textContent;

//        // Replace input elements with td elements
//        weight.parentNode.replaceChild(newWeight, weight);
//        bodyfat.parentNode.replaceChild(newBodyfat, bodyfat);
//        date.parentNode.replaceChild(newDate, date);

//    }
//}


//function saveValues(saveButton) {
//    var row = saveButton.parentNode.parentNode;
//    var weight = row.querySelector("#weight input").value;
//    var bodyfat = row.querySelector("#bodyfat input").value;
//    var date = row.querySelector("#date input").value;

//    // Update the row with the new values
//    row.querySelector("#weight").textContent = weight;
//    row.querySelector("#bodyfat").textContent = bodyfat;
//    row.querySelector("#date").textContent = date;

//    // Change the Save button text to "Edit"
                    
//}




function incorrectDateAlert() {
    alert("Dato er ikke i riktige format")
}