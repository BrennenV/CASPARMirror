var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_ReleaseTimes').DataTable({
        "ajax": {
            "url": "/api/releaseTimes",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "instructor.instructorName", "width": "20%" },
            { "data": "semester.semesterName", "width": "20%" },
            { "data": "releaseTimeAmount", "width": "5%" },
            { "data": "releaseTimeNotes", "width": "35%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a href=# class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;"">
                        <i class="bi bi-pencil-square"></i> Approve </a>
                    <a href=# class="btn btn-outline-danger mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-trash"></i> Deny </a>
                    </div>`;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%"
    });
}
