var dataTable;

$(document).ready(function () {
    loadddl();
    loadList();

    $('#ddlSemesterInstance').change(function () {
        setSemesterInstanceIdForQueryString();
        dataTable.ajax.reload();
    });
});

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
                    return Number(row.preferenceListDetail.preferenceList.semesterInstanceId) === selectedSemester;
                });
                return filteredData;
            },
            "error": function (xhr, error, thrown) {
                alert('Ajax error:' + xhr.responseText);
            }
        },
        "columns": [
            {
                "data": "preferenceListDetail",
                "render": function (data) {
                    return `${data.course.academicProgram.programCode} ${data.course.courseNumber} ${data.course.courseTitle}`
                },
                "width": "25%"
            },
            { "data": "preferenceListDetail.preferenceRank", "width": "5%"},
            { "data": "modality.modalityName", "width": "15%" },
            { "data": "campus.campusName", "width": "15%" },
            { "data": "daysOfWeek.daysOfWeekTitle", "width": "10%" },
            { "data": "timeBlock.timeBlockValue", "width": "10%" },
            {
                "data": "id",
                "render": function (data, type, row, meta) {
                    return `<div class="text-center">
                                <a href="/Instr/WishlistsOLD/Update?id=${data}&semesterInstanceId=${row.preferenceListDetail.preferenceList.semesterInstanceId}" class="btn btn-outline-primary mb-1 rounded" style="cursor:pointer; width: 100px;">
                                    <i class="bi bi-pencil-square"></i> Edit </a>
                                <a href="/Instr/WishlistsOLD/Delete?id=${data}&semesterInstanceId=${row.preferenceListDetail.preferenceList.semesterInstanceId}" class="btn btn-outline-danger mb-1 rounded" style="cursor:pointer; width: 100px;">
                                    <i class="bi bi-trash"></i> Delete </a>   
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