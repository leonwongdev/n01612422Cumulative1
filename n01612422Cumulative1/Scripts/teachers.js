function onSubmitNewTeacher() {
    console.log("onSubmitNewTeacher");
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