var dataTable;
$(document).ready(function () {
    loadList();
});
function loadList() {
    const urlParams = new URLSearchParams(window.location.search);
    const id = urlParams.get('id');
    dataTable = $('#DT_PreferenceListDetailModalities').DataTable({
        "ajax": {
            "url": `/api/preferenceListDetailModality?id=${id}`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //should not be capital
            { "data": "modality.modalityName", "width": "20%" },
            { "data": "campus.campusName", "width": "20%" },
            { "data": "daysOfWeek.daysOfWeekTitle", "width": "20%" },
            { "data": "timeBlock.timeBlockValue", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Instructor/PreferenceListDetailModalities/Upsert?id=${data}" class="btn btn-outline-primary mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
                        <i class="bi bi-pencil-square"></i> Edit </a>
                    <a href="/Instructor/PreferenceListDetailModalities/Delete?id=${data}" class="btn btn-outline-danger mt-1 rounded" style="cursor:pointer; style="cursor:pointer; width: 100px;">
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