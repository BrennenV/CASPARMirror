var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    const urlParams = new URLSearchParams(window.location.search);
    const id = urlParams.get('id');
    dataTable = $('#DT_PreferenceListDetails').DataTable({
        "ajax": {
            "url": `/api/preferenceListDetail?id=${id}`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "preferenceRank", "width": "10%" },
            { "data": "course.courseTitle", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Instructor/PreferenceListDetails/Upsert?id=${data}" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-pencil-square"></i> Edit </a>
                    <a href="/Instructor/PreferenceListDetails/Delete?id=${data}" class="btn btn-outline-danger mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-trash"></i> Delete </a>
                    <a href="/Instructor/PreferenceListDetailModalities/Index?id=${data}" class="btn btn-outline-info mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="far fa-trash-alt"></i> Modalities </a>
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