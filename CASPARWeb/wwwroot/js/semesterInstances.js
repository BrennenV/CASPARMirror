var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#DT_SemesterInstances').DataTable({
        "ajax": {
            "url": "/api/semesterInstance",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "semesterInstanceName", "width": "24%" },
            {"data": "startDate", "render": function (data, type, row) {
                    if (type === 'display') {
                        // Format the date using JavaScript's Date object
                        const date = new Date(data);
                        return date.toLocaleDateString();
                    }
                    return data;
                }, "width": "23%" },
            {"data": "endDate", "render": function (data, type, row) {
                    if (type === 'display') {
                        // Format the date using JavaScript's Date object
                        const date = new Date(data);
                        return date.toLocaleDateString();
                    }
                    return data;
                }, "width": "23%" },
            {"data": "id", "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Admin/SemesterInstances/Upsert?id=${data}" type="button" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; width: 100px;">
                        <i class="bi bi-pencil-square"></i> Edit </a>
                    <a href="/Admin/SemesterInstances/Delete?id=${data}" class="btn btn-outline-danger mt-1 rounded" style="cursor:pointer; style="cursor: pointer; width: 100px; ">
                        <i class="bi bi-trash" ></i> Delete </a>
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
