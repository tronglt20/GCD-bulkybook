var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/Categories/GetAll"
        },
        "columns": [
            { "data": "name", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Categories/Upsert/${data}" style="cursur:pointer" class="btn btn-success text-white">
                                    <i class="fas fa-edit"></i>
                                </a>    
                                <a href="/Admin/Categories/Upsert/id" style="cursur:pointer" class="btn btn-danger text-white">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>`;
                },
                "width":"40%"
            }
        ]
    })
}