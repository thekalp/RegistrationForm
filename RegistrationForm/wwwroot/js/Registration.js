var GlobalId = 0;

$(document).ready(function () {
    LoadData();

    $('#RegForm').validate({ // initialize the plugin
        rules: {
            Name: {
                required: true
            },
            Phone: {
                required: true,
                minlength: 10
            },
            Email: {
                required: true,
                email: true
            }
        }
    });
});
function bindState() {

    $("#ddlState").append($("<option></option>").val(""))
    $.ajax({
        url: '/Registration/GetState',
        dataType: "json",
        type: 'GET',
        success: function (data) {
             
            if (data.aaData.length > 0) {
                $.each(data.aaData, function (data, value) {
                    $("#ddlState").append($("<option></option>").val(value.id).html(value.stateName));
                })
            }
        },
        error: function (err) {
            toastr.error('An error has occured!!!');
        }
    });

}

function bindCity(id) {

     
    $('#ddlcity').empty();
    $("#ddlcity").append($("<option></option>").val(""))
    $.ajax({
        url: '/Registration/GetCity?id=' + id,
        dataType: "json",
        type: 'GET',
        success: function (data) {
             
            if (data.aaData.length > 0) {
                $.each(data.aaData, function (data, value) {
                    $("#ddlcity").append($("<option></option>").val(value.id).html(value.cityName));
                })
            }
        },
        error: function (err) {
            toastr.error('An error has occured!!!');
        }
    });

}

$(document).on('change', '#ddlState', function ($event) {
    bindCity($('#ddlState').val());
});

function LoadData() {
    $('#CountyTable').DataTable({
        paging: true,
        ordering: true,
        processing: true,
        serverSide: false,
        responsive: true,
        bDestroy: true,
        "dom":
            "<'row'" +
            "<'col-sm-12 d-flex align-items-center justify-content-end'f>" +
            ">" +

            "<'table-responsive'tr>" +

            "<'row'" +
            "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'li>" +
            "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
            ">",
        "ajax": {
            "url": '/Registration/Get',
            "type": "GET",
            "datatype": "json",
            "dataSrc": function (json) {
                // You can also modify `json.data` if required
                 
                return json.aaData;
            },
        },
        "columns": [
            {
                "data": "name",
                "autoWidth": true,
                "searchable": true,
                "sortable": true,

            },
            {
                "data": "email",
                "autoWidth": true,
                "searchable": true,
                "sortable": true,

            },
            {
                "data": "phone",
                "autoWidth": true,
                "searchable": true,
                "sortable": true,

            },
            {
                "data": "address",
                "autoWidth": true,
                "searchable": true,
                "sortable": true,

            },
            {
                data: "id",
                searchable: false,
                sortable: false,
                render: function (data, type, row) {
                    return '<td class="text-end"><a  onclick="EditById(\'' + row.id + '\')" class="btn btn-sm btn-white text-success me-2"><i class="fa fa-edit" style="color:green"></i> Edit</a><a onclick="DeleteById(\'' + row.id + '\')"  class="btn btn-sm btn-white text-danger"><i class="fa fa-trash" style="color:red"></i>Delete</a></td>';
                }
            }
        ]
    });
}

$(document).on('click', '#btnNewCounty', function () {
    $("#AddRegistration #btnSubmit").text('Save');
    $("#AddRegistration .countyTitle").text('Add New Registration');
    $('#RegForm').validate().resetForm();

    $('#ddlcity').empty();
    $('#ddlState').empty();

    bindCity(0);
    bindState();
    $("#Name").val(null);
    $("#Address").val(null);
    $("#Phone").val(null);
    $("#Email").val(null);
    $("#Id").val(0);

    $("#AddRegistration").modal("show");
});

$(document).on('click', '#btnSubmit', function () {
    SaveData();
});

function SaveData() {
     
    if ($("#RegForm").valid() == true) {

        if ($("#RegForm #btnSubmit").text() == "Save") {
            var formData = new FormData();
            formData.append("Id", 0)
            formData.append("Name", $("#Name").val())
            formData.append("Address", $("#Address").val())
            formData.append("Phone", $("#Phone").val())
            formData.append("Email", $("#Email").val())
            formData.append("StateId", $("#ddlState").val())
            formData.append("CityId", $("#ddlcity").val())
            $.ajax({
                url: "/Registration/Add",
                type: "post",
                data: formData,
                contentType: false,
                processData: false,
                dataType: "json",
                success: function (data) {
                     
                    if (data.success == "1") {
                        LoadData();
                        $("#AddRegistration").modal('hide');
                        alert(data.message);
                    }
                    else if (data.warning) {
                        alert(data.message);
                    }
                    else {
                        alert("Error occured");
                        $("#AddRegistration").modal('hide');
                    }
                },
                error: function (err) {

                    alert('An error has occured!!!');
                }
            });
        }
        else {
            var formData = new FormData();
            formData.append("Id", $("#Id").val())
            formData.append("Name", $("#Name").val())
            formData.append("Address", $("#Address").val())
            formData.append("Phone", $("#Phone").val())
            formData.append("Email", $("#Email").val())
            formData.append("StateId", $("#ddlState").val())
            formData.append("CityId", $("#ddlcity").val())
            $.ajax({
                url: "/Registration/Update",
                type: "post",
                data: formData,
                contentType: false,
                processData: false,
                dataType: "json",
                success: function (data) {
                     
                    if (data.success == "1") {
                        alert(data.message);
                        LoadData();

                    }
                    else if (data.warning) {
                        alert(data.message);
                    }
                    else {
                        alert("Error occured");
                    }
                    $("#AddRegistration").modal('hide');
                },
                error: function (err) {
                    alert('An error has occured!!!');
                }
            });
        }
    }
}

function EditById(id) {

    $('#RegForm').validate().resetForm();
    $('#ddlcity').empty();
    $('#ddlState').empty();
    $("#Id").val(id);
    var formData = new FormData();
    formData.append('Id', id);
    $.ajax({
        url: '/Registration/Get',
        dataType: "json",
        type: 'GET',
        data: { id: id },
        success: function (data) {
            debugger


            bindState();
            setTimeout(function () {
                $("#ddlState").val(data.aaData[0].stateId).trigger('change');
            }, 500);
          /*  bindCity(0);*/
            setTimeout(function () {
                $("#ddlcity").val(data.aaData[0].cityId);
            }, 800);
           
            $("#AddRegistration").modal("show");
            $("#Id").val(data.aaData[0].id);
            $("#Name").val(data.aaData[0].name);
            $("#Address").val(data.aaData[0].address);
            $("#Phone").val(data.aaData[0].phone);
            $("#Email").val(data.aaData[0].email);
            $("#AddRegistration #btnSubmit").text('Update');
            $("#AddRegistration .RegTitle").text('Edit Registration');
          
        },
        error: function (err) {
            alert('An error has occured!!!');
        }
    });
}


function DeleteById(id) {
    GlobalId = id;
    $("#ConfirmDelete").modal("show");
}



$(document).on('click', '#btnDeleteCounty', function () {
    $.ajax({
        url: '/Registration/Delete/?id=' + GlobalId,
        type: 'GET',
        success: function (data) {
             
            if (data.success.value == "1") {
                LoadData();
                alert("Data has been deleted successfully!!");
                $("#ConfirmDelete").modal('hide');
            }
            else {
                alert("An error has occured!!!");
                $("#ConfirmDelete").modal('hide');
            }
        },
        error: function (err) {

            alert('An error has occured!!!');
        }
    });
})