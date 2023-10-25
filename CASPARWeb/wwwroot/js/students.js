var dataTable;
var ddlSemesterInstance;

$(document).ready(function () {
    loadddl();
    loadList();

    $('#ddlSemesterInstance').change(function () {
        dataTable.ajax.reload();
    });
});

function loadddl() {
    $.ajax({
        url: "/api/semesterInstance",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var $dropdown = $("#ddlSemesterInstance");
            $dropdown.empty();
            $.each(data.data, function (index, item) {
                $dropdown.append($("<option />").val(item.id).text(item.semesterInstanceName));
            });
        },
        error: function (xhr, error, thrown) {
            alert('Ajax error:' + xhr.responseText);
        }
    });
}

function loadList() {
    dataTable = $('#DT_Students').DataTable({
        "ajax": {
            "url": "/api/student",
            "type": "GET",
            "datatype": "json",
            "dataSrc": function (json) {
                var selectedSemester = Number($('#ddlSemesterInstance').val());
                var filteredData = $.grep(json.data, function (row) {
                    return Number(row.wishlistDetail.wishlist.semesterInstance.id) === selectedSemester;
                });
                return filteredData;
            },
            "error": function (xhr, error, thrown) {
                alert('Ajax error:' + xhr.responseText);
            }
        },
        "columns": [
            { "data": "wishlistDetail.course.courseTitle", "width": "30%" },
            { "data": "modality.modalityName", "width": "15%" },
            { "data": "campus.campusName", "width": "15%" },
            { "data": "timeOfDay.partOfDay", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Students/Upsert?id=${data}" class="btn btn-outline-primary mb-1 rounded" style="cursor:pointer; width: 100px;">
                                    <i class="bi bi-pencil-square"></i> Edit </a>
                                <a href="/Students/Delete?id=${data}" class="btn btn-outline-danger mb-1 rounded" style="cursor:pointer; width: 100px;">
                                    <i class="bi bi-trash"></i> Delete </a>   
                            </div>`;
                }, "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%",
        "order": [[0, "asc"]]
    });
}