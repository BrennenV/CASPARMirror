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
                "width": "70%"
            },
            { "data": "quantity", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Administrator/Templates/Upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width: 100px;">
                        <i class="far fa-edit"></i> Edit </a>
                    <a href="/Administrator/Templates/Delete?id=${data}" class="btn btn-danger text-white" style="cursor:pointer; width: 100px;">
                        <i class="far fa-trash-alt"></i> Delete </a>
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
