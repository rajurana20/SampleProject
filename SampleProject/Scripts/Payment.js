

function setCostById(CId) {
    var cost = CalculateCostById(CId);
    $('#TotalCost').val(cost);
}





function CalculateCostById(CId) {
    $.ajax({
        url: "/Payments/getCostById/" + CId,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#TotalCost').val(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}




