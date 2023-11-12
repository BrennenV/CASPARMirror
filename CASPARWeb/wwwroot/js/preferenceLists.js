var dataTable;

$(document).ready(function () {
    loadddl();
    loadCheckBoxes();
    //loadList();

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
                    }).css({
                        'outline': '1px solid #593196',
                        'border-radius': '25%' // Add this line
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
    $.ajax({
        url: "/api/timeBlockList",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var container1 = $("#timeBlocks1");
            var container2 = $("#timeBlocks2");
            container1.empty();
            container2.empty();
            $.each(data.data, function (index, timeBlock) {
                if (!timeBlock.isArchived) {
                    var div = $('<div>').addClass('form-check');

                    var input = $('<input>').addClass('form-check-input').attr({
                        type: 'checkbox',
                        id: 'check' + (index + 1),
                        name: 'option' + (index + 1),
                        value: timeBlock.timeBlockValue
                    }).css({
                        'outline': '1px solid #593196',
                        'border-radius': '25%' // Add this line
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
                    }).css({
                        'outline': '1px solid #593196',
                        'border-radius': '25%' // Add this line
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
                    }).css({
                        'outline': '1px solid #593196',
                        'border-radius': '25%' // Add this line
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

function loadList() {
    dataTable = $('#DT_PreferenceDetails').DataTable({
        "ajax": {
            "url": "/api/preferenceDetail",
            "type": "GET",
            "datatype": "json",
            "dataSrc": function (json) {
                var selectedSemester = Number($('#ddlSemesterInstance').val());
                var filteredData = $.grep(json.data, function (row) {
                    return Number(row.wishlist) === selectedSemester;
                });
                return filteredData;
            },
            "error": function (xhr, error, thrown) {
                alert('Ajax error:' + xhr.responseText);
            }
        },
        "columns": [
            { "data": "wishlistCourse.preferenceRank", "width": "5%" },
            {
                "data": "wishlistCourse",
                "render": function (data) {
                    return `${data.course.academicProgram.programCode} ${data.course.courseNumber} ${data.course.courseTitle}`
                },
                "width": "25%"
            },
            {
                "data": "id",
                "render": function (data, type, row, meta) {
                    return `<div class="text-center">
                                <a href="/Instr/Wishlists/Update?id=${data}&semesterInstanceId=${row.wishlistCourse.wishList.semesterInstanceId}" class="btn btn-outline-primary mb-1 rounded" style="cursor:pointer; width: 100px;">
                                    <i class="bi bi-pencil-square"></i></a>
                                <a href="/Instr/Wishlists/Delete?id=${data}&semesterInstanceId=${row.wishlistCourse.wishList.semesterInstanceId}" class="btn btn-outline-danger mb-1 rounded" style="cursor:pointer; width: 100px;">
                                    <i class="bi bi-trash"></i></a>   
                            </div>`;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%",
        "order": [[0, "asc"]]
    });
}