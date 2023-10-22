var dataTable;
var ddlSemesterInstance;

$(document).ready(function () {
    loadddl();
    loadList();

    $('#ddlSemesterInstance').change(function () {
        console.log("Dropdown value changed to: ", $(this).val());
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
                console.log(item);
                console.log('id:', item.id);
                console.log('semesterInstanceName:', item.semesterInstanceName);
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
                console.log("Selected semester: ", selectedSemester);
                var filteredData = $.grep(json.data, function (row) {
                    console.log("Selected semester comparison: ", row.wishlistDetail.wishlist.semesterInstance.id);
                    return Number(row.wishlistDetail.wishlist.semesterInstance.id) === selectedSemester;
                });
                console.log("Filtered data: ", filteredData);
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
            { "data": "wishlistPartOfDay", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Students/Index?id=${data}" class="btn btn-outline-primary mb-1 rounded" style="cursor:pointer; width: 100px;">
                                    <i class="far fa-edit"></i> Edit </a>
                                <a href="/Students/Delete?id=${data}" class="btn btn-danger mb-1 rounded text-white" style="cursor:pointer; width: 100px;">
                                    <i class="far fa-trash-alt"></i> Delete </a>   
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