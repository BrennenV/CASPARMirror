var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    const urlParams = new URLSearchParams(window.location.search);
    const id = urlParams.get('id');
    dataTable = $('#DT_Templates').DataTable({
        "ajax": {
            "url": `/api/template?id=${id}`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            {
                "data": null,
                "render": function (data, type, row) {
                    return row.course.courseNumber + " - " + row.course.courseTitle
                },
                "width": "60%"
            },
            { "data": "quantity", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Administrator/Templates/Upsert?id=${data}" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-pencil-square"></i> Edit </a>
                    <a href="/Administrator/Templates/Delete?id=${data}" class="btn btn-outline-danger mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-trash"></i> Delete </a>
                    </div>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%"
    });
}
