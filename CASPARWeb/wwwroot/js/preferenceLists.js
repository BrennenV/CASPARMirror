var dataTable;

$(document).ready(function () {
    loadddl();
    loadCheckBoxes();

    $('#ddlSemesterInstance').change(function () {
        setSemesterInstanceIdForQueryString();
        dataTable.ajax.reload();
    });
});

function loadCheckBoxes() {
    $.ajax({
        url: "/api/modality",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var container = $("#modalities");
            container.empty();
            $.each(data.data, function (index, modality) {
                if (!modality.isArchived) {
                    var div = $('<div>').addClass('form-check');

                    var input = $('<input>').addClass('form-check-input').attr({
                        type: 'checkbox',
                        id: 'check' + (index + 1),
                        name: 'option' + (index + 1),
                        value: modality.modalityName
                    });

                    var label = $('<label>').addClass('form-check-label').attr('for', input.attr('id')).text(modality.modalityName);

                    div.append(input, label);
                    container.append(div);
                }
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });

    // Load the time blocks
    loadTimeBlocks();

    // Load the days of the week
    loadDaysOfWeek();

    // Load the campuses
    loadCampuses();
}

function loadTimeBlocks() {
    $.ajax({
        url: "/api/timeBlockList",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var container1 = $("#timeBlocks1");
            var container2 = $("#timeBlocks2");
            container1.empty();
            container2.empty();

            // Sort the data from least to greatest
            data.data.sort(function (a, b) {
                var aTimes = a.timeBlockValue.split(' - ');
                var bTimes = b.timeBlockValue.split(' - ');
                var aStart = new Date('1970/01/01 ' + aTimes[0]);
                var aEnd = new Date('1970/01/01 ' + aTimes[1]);
                var bStart = new Date('1970/01/01 ' + bTimes[0]);
                var bEnd = new Date('1970/01/01 ' + bTimes[1]);

                // Compare start times first, then end times if start times are equal
                return aStart - bStart || aEnd - bEnd;
            });


            $.each(data.data, function (index, timeBlock) {
                if (!timeBlock.isArchived) {
                    var div = $('<div>').addClass('form-check');

                    var input = $('<input>').addClass('form-check-input').attr({
                        type: 'checkbox',
                        id: 'check' + (index + 1),
                        name: 'option' + (index + 1),
                        value: timeBlock.timeBlockValue
                    });

                    var label = $('<label>').addClass('form-check-label').attr('for', input.attr('id')).text(timeBlock.timeBlockValue);

                    div.append(input, label);

                    // Add to the first container if index is even, second container if index is odd
                    if (index % 2 === 0) {
                        container1.append(div);
                    } else {
                        container2.append(div);
                    }
                }
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}

function loadDaysOfWeek() {
    $.ajax({
        url: "/api/daysOfWeek",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var container = $("#daysOfWeek");
            container.empty();
            $.each(data.data, function (index, day) {
                if (!day.isArchived) {
                    var div = $('<div>').addClass('form-check');

                    var input = $('<input>').addClass('form-check-input').attr({
                        type: 'checkbox',
                        id: 'check' + (index + 1),
                        name: 'option' + (index + 1),
                        value: day.daysOfWeekValue
                    });

                    var label = $('<label>').addClass('form-check-label').attr('for', input.attr('id')).text(day.daysOfWeekValue);

                    div.append(input, label);
                    container.append(div);
                }
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}

function loadCampuses() {
    $.ajax({
        url: "/api/campusList",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var container = $("#campuses");
            container.empty();
            $.each(data.data, function (index, campus) {
                if (!campus.isArchived) {
                    var div = $('<div>').addClass('form-check');

                    var input = $('<input>').addClass('form-check-input').attr({
                        type: 'checkbox',
                        id: 'check' + (index + 1),
                        name: 'option' + (index + 1),
                        value: campus.campusName
                    });

                    var label = $('<label>').addClass('form-check-label').attr('for', input.attr('id')).text(campus.campusName);

                    div.append(input, label);
                    container.append(div);
                }
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}



function loadddl() {
    $.ajax({
        url: "/api/semesterInstance",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var dropdown = $("#ddlSemesterInstance");
            dropdown.empty();
            $.each(data.data, function (index, item) {
                if (item.id == $("#selectedSemesterId").text()) {
                    dropdown.append($("<option />").val(item.id).text(item.semesterInstanceName).attr("selected", "selected"));
                }
                else {
                    dropdown.append($("<option />").val(item.id).text(item.semesterInstanceName));
                }
            });

            //This is to add the semester instance to the query string when the add preference button is clicked
            setSemesterInstanceIdForQueryString();
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}

function setSemesterInstanceIdForQueryString() {
    var dropdown = $("#ddlSemesterInstance");
    var btn = $("#addPreferenceBtn");

    var oldHref = btn.attr("href");
    var newHref = oldHref.slice(0, oldHref.indexOf("=") + 1) + dropdown.val().toString();

    btn.attr("href", newHref);
}

function loadTemplate() {
    
}