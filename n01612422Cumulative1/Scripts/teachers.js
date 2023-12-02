function onSubmitNewTeacher() {
    console.log("onSubmitNewTeacher");
    return validateInputs()
}

function validateInputs() {
    // Initiative: Use JavaScript and Client Side Validation to ensure that the user doesn’t accidentally
    // submit a form with missing information(such as a teacher name)
    // Get input values after trimming whitespace to make sure user cannot input only empty spaces.
    var fname = document.getElementById('fname').value.trim();
    var lname = document.getElementById('lname').value.trim();
    var employeeNum = document.getElementById('employeeNum').value.trim();
    var hireDate = document.getElementById('hireDate').value.trim();
    var salary = document.getElementById('salary').value.trim();

    // Validate input
    // Check if fields are empty
    if (fname === "") {
        alert("Please enter a valid First Name");
        return false; // prevent form submission
    }

    if (lname === "") {
        alert("Please enter a valid Last Name");
        return false; // prevent form submission
    }

    if (employeeNum === "") {
        alert("Please enter a valid Employee Number");
        return false; // prevent form submission
    }

    if (hireDate === "") {
        alert("Please enter a valid Hire Date");
        return false; // prevent form submission
    }

    if (salary === "" || salary <= 0) {
        alert("Please enter a valid Salary");
        return false; // prevent form submission
    }

    // If all validations pass, allow form submission
    return true;
}

// Initiative: Use JavaScript and AJAX to send an XHR request for adding a teacher (See C# For
// Web Development Pt10 in Module 8)
function onSubmitAjax() {

    if (validateInputs === false) {
        alert("Please check your input and resubmit.");
        return;
    }
    var createTeacherbtn = document.getElementById("createTeacherBtn");
    var fname = document.getElementById('fname').value.trim();
    var lname = document.getElementById('lname').value.trim();
    var employeeNum = document.getElementById('employeeNum').value.trim();
    var hireDate = document.getElementById('hireDate').value.trim();
    var salary = document.getElementById('salary').value.trim();

    var TeacherData = {
        fname,
        lname,
        employeeNum,
        hireDate,
        salary
    };

    var URL = window.location.protocol + "//" + window.location.host + "/api/TeacherData/AddTeacher"
    

    console.log("Ajax URL for Add Teacher",URL);

    var rq = new XMLHttpRequest();
    rq.open("POST", URL, true);
    rq.setRequestHeader("Content-Type", "application/json");
    rq.onreadystatechange = function () {
        //ready state should be 4 AND status should be 200
        if (rq.readyState == 4) {

            if (rq.status == 200) {
                //request is successful and the request is finished

                //nothing to render, the method returns nothing.
                const msg = rq.responseText;
                alert(msg);
                createTeacherbtn.disabled = false;
            } else {
                alert("Error sending ajax request");
                createTeacherbtn.disabled = false;
            }

        } 

    }
    //POST information sent through the .send() method
    rq.send(JSON.stringify(TeacherData));
    createTeacherbtn.disabled = true;
}

// Initiative: Use JavaScript and AJAX to send an XHR request for adding a teacher (See C# For
// Web Development Pt10 in Module 8)
function onDeleteAjax(id) {
    if (id == null || id < 0) {
        alert("Teacher id cannot be null or less than 0.");
        return;
    }

    var ajaxDelBtn = document.getElementById('ajaxDelBtn');

    var URL = window.location.protocol + "//" + window.location.host + "/api/TeacherData/DeleteTeacher/" + id;


    console.log("Ajax URL for Delete Teacher", URL);

    var rq = new XMLHttpRequest();
    rq.open("POST", URL, true);
    rq.setRequestHeader("Content-Type", "application/json");
    rq.onreadystatechange = function () {
        //ready state should be 4 AND status should be 200
        if (rq.readyState == 4) {

            if (rq.status == 200) {
                //request is successful and the request is finished

                //nothing to render, the method returns nothing.
                const msg = rq.responseText;
                alert(msg);
                window.location.href = "/Teacher/List";
                ajaxDelBtn.disabled = false;
            } else {
                alert("Error sending ajax request");
                ajaxDelBtn.disabled = false;
            }

        }

    }
    //POST information sent through the .send() method
    rq.send(null);
    ajaxDelBtn.disabled = true;
}