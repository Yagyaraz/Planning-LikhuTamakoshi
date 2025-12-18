// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Message(msg) {
    if (msg !== "") {
        var contantData = '';
        if (msg === true) {
            contantData = 'तपाइको डाटा सुरक्षित भयो!!';
        } else if (msg === "error") {
            contantData = 'तपाइको डाटा सुरक्षित भएन!!';
        } else if (msg === false) {
            contantData = 'तपाइको डाटा सुरक्षित भएन!!';
        } else {
            contantData = msg;
        }

        if (contantData != '') {
            $.confirm({
                columnClass: 'col-md-5',
                title: '',
                content: contantData,
                type: 'blue',
                typeAnimated: true,
                autoClose: 'closeBtn|5000',
                buttons: {
                    closeBtn: {
                        text: 'Close',
                        btnClass: 'btn-green',
                        action: function () {
                           //window.location.reload(true);
                        }
                    }
                }
            });
        }
    }
}
function Message1(msg) {
    if (msg !== "") {
        var contantData = msg;
        if (contantData != '') {
            $.confirm({
                columnClass: 'col-md-5',
                title: '',
                content: contantData,
                type: 'blue',
                typeAnimated: true,
                autoClose: 'closeBtn|3000',
                buttons: {
                    closeBtn: {
                        text: 'Close',
                        btnClass: 'btn-green',
                    }
                }
            });
        }
    }
}
function ApproveOrNot(a, id) {
    debugger;
    $.confirm({
        title: 'Confirm!',
        content: 'Are you sure !',
        buttons: {
            Approve: {
                text: 'Approve',
                btnClass: 'btn-green',
                keys: ['enter', 'shift'],
                action: function () {
                    debugger;
                    $.ajax({
                        url: ApproveFor(),
                        type: 'get',
                        data: { id: id, Isapprove: true },
                        success: function (result) {
                            $.alert('Approve गरिएको छ!');
                            $(a).closest('span').remoe();
                        },
                        error: function () {
                            $.alert('error in calling ajax !!');
                        }
                    });
                }
            },
            cancel: function () {
                //close
            }
        }
    });
}
